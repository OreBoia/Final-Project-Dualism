using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class DialogScript : MonoBehaviour
{
    PlayerInput playerInput;

    public string actionMapNameSwitch;

    [SerializeField]
    InputActionMap inputActions;
    [SerializeField]
    string scheme;
    [SerializeField]
    bool autoswitch;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        scheme = playerInput.currentControlScheme;
    }

    public void OnEventInteraction()
    {
        Debug.Log("SWITCH TO DC");

        if(DialogController.Instance.dialogAsset != null)
        {
            ChangeActionMap("DialogControl");

            OnNextSentence();
        } 
    }

    private void OnNextSentence()
    {
        DialogController.Instance.NextSentence();
    }

    private void OnSkipSentence()
    {
        DialogController.Instance.SkipSentence();
    }

    public void ChangeActionMap(string mapName)
    {
        playerInput.SwitchCurrentActionMap(mapName);
    }
}

