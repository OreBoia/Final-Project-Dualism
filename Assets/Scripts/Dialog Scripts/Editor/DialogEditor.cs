using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class DialogEditor : EditorWindow
{
    [MenuItem("Tools/Dialog Editor")]

    private static void OpenWindow()
    {
        GetWindow<DialogEditor>();
    }

    private Vector2 scrollPosition;
    private Vector2 scrollPositionLine;
    private int selectedDialogIndex;
    private string[] dialogsAssetsFound;
    private string[] dialogsAssetsFoundLabel;

    string imageName = "null";

    private void OnGUI()
    {
        titleContent = new GUIContent("Dialog Editor");

        if (GUILayout.Button("New Dialog"))
        {
            NewDialog();
        }

        GetAllDialogs();

        if (dialogsAssetsFound.Length == 0)
        {
            EditorGUILayout.HelpBox("No Languages Found", MessageType.Error);
            return;
        }

        selectedDialogIndex = EditorGUILayout.Popup("Dialogs",
            selectedDialogIndex, dialogsAssetsFoundLabel);

        GUILayout.Label(dialogsAssetsFound[selectedDialogIndex]);

        DialogScriptableObject dialog = AssetDatabase.LoadAssetAtPath<DialogScriptableObject>(dialogsAssetsFound[selectedDialogIndex]);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        GUILayout.Label("DIALOG LIST");
        GUILayout.Space(5f);

        //DISPLAY STRINGS
        for (int i = 0; i < dialog.strings.Count; i++)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(dialog.strings[i].id.ToString(),
                imageName != null ? imageName : "null");

            GUILayout.Space(15f);

            GUILayout.Label(dialog.strings[i].sentence);

            //EDIT STRING
            if (GUILayout.Button("e", EditorStyles.miniButtonLeft, GUILayout.Width(25f)))
            {
                EditDialogLineWindow editDialogLineWindow = EditorWindow.GetWindow<EditDialogLineWindow>();

                editDialogLineWindow.stringIndex = i;
                editDialogLineWindow.stringContent = dialog.strings[i].sentence;
                editDialogLineWindow.id = dialog.strings[i].id;
                editDialogLineWindow.dialog = dialog;
            }

            //REMOVE STRING
            if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(25f)))
            {
                if (EditorUtility.DisplayDialog("Confirm",
                    $"Do you really want to remove the string {dialog.strings[i].sentence}?",
                    "YES",
                    "NO"))
                {
                    RemoveString(dialog.strings[i].sentence);
                    break;
                }

            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
    }

    private void GetAllDialogs()
    {
        dialogsAssetsFound = AssetDatabase.FindAssets("t: DialogScriptableObject");
        dialogsAssetsFoundLabel = new string[dialogsAssetsFound.Length];

        for (int i = 0; i < dialogsAssetsFound.Length; i++)
        {
            dialogsAssetsFound[i] =
                AssetDatabase.GUIDToAssetPath(dialogsAssetsFound[i]);
            dialogsAssetsFoundLabel[i] =
                Path.GetFileName(dialogsAssetsFound[i]);

            //Debug.Log($"Trovato: {dialogsAssetsFound[i]}");
        }
    }

    private void NewDialog()
    {
        string path = EditorUtility.SaveFilePanelInProject("New Dialog Asset",
            "NewDialog",
            "asset",
            "New Dialog Saved!");

        if (!string.IsNullOrEmpty(path))
        {
            DialogScriptableObject newDialog = ScriptableObject.CreateInstance<DialogScriptableObject>();

            AssetDatabase.CreateAsset(newDialog, path);

            EditorUtility.SetDirty(newDialog);
        }
    }

    private void RemoveString(string line)
    {
        foreach (string languagePath in dialogsAssetsFound)
        {
            DialogScriptableObject language = AssetDatabase.LoadAssetAtPath<DialogScriptableObject>(languagePath);
            language.strings.RemoveAll(s => s.sentence == line);

            EditorUtility.SetDirty(language);
        }
    }
}