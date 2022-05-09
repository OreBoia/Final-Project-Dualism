using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditDialogLineWindow : EditorWindow
{
    public int stringIndex;
    public string stringContent;
    public Sprite image;
    public int id;
    public DialogScriptableObject dialog;
    public bool confirm;

    private void OnGUI()
    {
        titleContent = new GUIContent("Edit Dialog Line");

        GUILayout.Label("Character Name");

        EditorGUILayout.TextField(id.ToString());


        GUILayout.Label("Content");
        stringContent = EditorGUILayout.TextArea(stringContent, GUILayout.ExpandHeight(true));

        GUILayout.Space(25f);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("OK"))
        {
            confirm = true;
            dialog.strings[stringIndex].sentence = stringContent;
            Close();
        }

        if (GUILayout.Button("Cancel"))
        {
            Close();
        }

    }
}
