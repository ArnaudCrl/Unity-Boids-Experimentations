using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public ShapeSettings settings;
    public int numberOfSections = 20;

    private Transform cameraTransform;
    private const int sectionLength = 2;
    private List<GameObject> tunnelSections;
    private int numberOfAddedSections = 0;


    void Awake()
    {
        cameraTransform = Camera.main.transform;

        tunnelSections = new List<GameObject>();
        for (int i = 0; i < numberOfSections; i++)
        {
            GameObject section = new GameObject("Tunnel Section");
            section.AddComponent<TunnelSection>();

            tunnelSections.Add(section);
            tunnelSections[i].GetComponent<TunnelSection>().CreateSection(settings, i * sectionLength);
            tunnelSections[i].transform.parent = transform;
            tunnelSections[i].transform.position += new Vector3(0, 0, i * sectionLength);
        }
    }

    public void OnSettingsChanged()
    {
        foreach (GameObject section in tunnelSections)
        {
            section.GetComponent<TunnelSection>().UpdateSection(settings);
        }
    }

    void Update()
    {
        if(cameraTransform.position.z > tunnelSections[0].transform.position.z + sectionLength)
        {
            removeSectionAtBeggining();
            addSectionAtEnd();
        }
    }

    private void removeSectionAtBeggining()
    {
        Destroy(tunnelSections[0]);
        tunnelSections.RemoveAt(0);
    }

    private void addSectionAtEnd()
    {
        GameObject section = new GameObject("Tunnel Section");
        section.AddComponent<TunnelSection>();

        int zOffset = (numberOfSections + numberOfAddedSections) * sectionLength;
        tunnelSections.Add(section);
        tunnelSections[numberOfSections - 1].GetComponent<TunnelSection>().CreateSection(settings, zOffset);
        tunnelSections[numberOfSections - 1].transform.parent = transform;
        tunnelSections[numberOfSections - 1].transform.position += new Vector3(0, 0, zOffset);

        numberOfAddedSections++;
    }
}
