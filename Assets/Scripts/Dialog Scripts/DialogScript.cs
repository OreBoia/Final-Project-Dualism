using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class DialogScript : MonoBehaviour
{
    private void OnNextSentence()
    {
        DialogController.Instance.NextSentence();
    }

    private void OnSkipSentence()
    {
        DialogController.Instance.SkipSentence();
    }
}

