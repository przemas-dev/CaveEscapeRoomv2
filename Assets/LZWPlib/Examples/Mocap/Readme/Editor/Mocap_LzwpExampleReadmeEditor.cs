using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Mocap_LzwpExampleReadme))]
public class Mocap_LzwpExampleReadmeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Mocap example", new GUIStyle("BoldLabel") { fontSize = 18, padding = new RectOffset(0, 0, 10, 10), alignment = TextAnchor.MiddleCenter });

        if (GUILayout.Button(new GUIContent(" MANUAL", EditorGUIUtility.IconContent("_Help").image), GUILayout.Height(30)))
            Application.OpenURL(Lzwp.LzwpLibManualUrl + "#mocap-example");

        GUILayout.Space(20);
    }
}
