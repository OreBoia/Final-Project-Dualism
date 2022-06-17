using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseControllerScript : MonoBehaviour
{
    public GameObject[] chooseObjects;

    public int index;
    public int assetIndex;

    public Image cloudToChange;
    public Sprite[] newClouds;

    private void Start()
    {
        ChangeSelectionColorFrame(Color.blue);
    }

    private void Update()
    {
        DialogController.Instance.PrintSpeakers();

        if (DialogController.Instance.dialogStatus == DialagoStatus.EndOfDialog)
        {
            Debug.Log("NEW CLOUD");
        }
    }

    public void OnMovePositionRight()
    {
        ChangeSelectionColorFrame(Color.white);

        if (index < 2)
        {
            index++;
        }

        ChangeSelectionColorFrame(Color.blue);
    }

    public void OnMovePositionLeft()
    {
        ChangeSelectionColorFrame(Color.white);

        if (index > 0)
        {
            index--;
        }

        ChangeSelectionColorFrame(Color.blue);
    }

    public void OnSelectChoose()
    {
        Debug.Log($"Choose {index} - {chooseObjects[index]}");

        //Le scelte devono sparire dallo schermo
        ActiveDeatctiveChooseFrame();
        //Inizia dialogo 
        StartDialog();
        //Alla fine del dialogo:
        //  - nuova nuvola
        //  - nuove scelte
        //index++;
        

    }

    private void StartDialog()
    {
        DialogController.Instance.dialogAsset = chooseObjects[index].GetComponent<DialogAssetChoose>().dialog[assetIndex];
        DialogController.Instance.AddSpeaker();
        DialogController.Instance.setPointObj = null;

        DialogScript ds = GetComponent<DialogScript>();

        ds.OnEventInteraction();
    }

    private void ActiveDeatctiveChooseFrame()
    {
        
    }

    public void ChangeSelectionColorFrame(Color newColor)
    {
        chooseObjects[index].GetComponent<Image>().color = newColor;
    }

    public void EndOfDialogChanges()
    {
        if (assetIndex < 2)
        {
            assetIndex++;
            cloudToChange.sprite = newClouds[assetIndex];
        }      
    }
}
