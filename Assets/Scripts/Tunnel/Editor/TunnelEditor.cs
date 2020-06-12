using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tunnel))]
public class TunnelEditor : Editor
{
    Tunnel tunnel;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawSettingsEditor(tunnel.settings, tunnel.OnSettingsChanged);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated)
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            Editor editor = CreateEditor(settings);
            editor.OnInspectorGUI();

            if (check.changed)
            {
                if (onSettingsUpdated != null)
                {
                    onSettingsUpdated();
                }
            }
        }

    }

    void OnEnable()
    {
        tunnel = (Tunnel)target;
    }
}
