using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class DialogScript : MonoBehaviour
{
    PlayerInput playerInput;

    [SerializeField]
    string inputActions;
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

    }

    private void OnEventInteraction()
    {
        Debug.Log("SWITCH TO DC");

        if(DialogController.Instance.dialogAsset != null)
        {
            playerInput.SwitchCurrentActionMap("DialogControl");
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
}

