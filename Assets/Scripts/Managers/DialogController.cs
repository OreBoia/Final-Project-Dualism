using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;

public enum DialagoStatus { Init, Typing, EndOfSentence, EndOfDialog }
public enum DialagoType { Dialog, Monologue }

public class DialogController : MonoBehaviour
{
    
    private int selectedDialogIndex;
    private string[] dialogsAssetsFound;

    public TextMeshProUGUI dialogTextObj;
    private char[] charText;
    public int index = 0;
    public float typingSpeed;
    public int previusId;

    public GameObject setPointObj;

    //DIALOG ASSET
    public DialogScriptableObject dialogAsset;

    //DICTIONARY
    Dictionary<int, GameObject> speakerList =
    new Dictionary<int, GameObject>();

    

    Dictionary<int, GameObject> sortedSpeakerList = 
        new Dictionary<int, GameObject>();

    //COROUTINE 
    IEnumerator coroutine;

    //DIALOG STATUS
    public DialagoStatus dialogStatus = DialagoStatus.EndOfSentence;

    //INSTANCE
    private static DialogController _instance;

    public static DialogController Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        coroutine = Type();
    }

    private void Update()
    {
        //if (index == dialogAsset.strings.Count)
        //{
        //    dialagoStatus = DialagoStatus.EndOfDialog;
        //}

        if (dialogStatus == DialagoStatus.EndOfDialog)
        {
            speakerList.Clear();
        }
    }

    public IEnumerator Type()
    {
        dialogStatus = DialagoStatus.Typing;

        charText = dialogAsset.strings[index].sentence.ToCharArray();

        for (int i = 0; i < charText.Length; i++)
        {
            dialogTextObj.text += charText[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        dialogStatus = DialagoStatus.EndOfSentence;

        EndOfDialogCheck();
    }

    //ALLA FINE DELLA FRASE SALTA INIZIA QUELLA SUCCESSIVA TRAMITE INPUT (E o (A - CONTROLLER))
    public void NextSentence()
    {
        if (dialogAsset != null)
        {
            coroutine = Type();

            if (index == 0 && setPointObj != null)
            {
                SetPosition(setPointObj);
            }

            if (index < dialogAsset.strings.Count && dialogStatus != DialagoStatus.EndOfDialog && dialogStatus == DialagoStatus.EndOfSentence)
            {
                SetCanvasToActivate();

                SetTextMeshObj();

                StartCoroutine(coroutine);
            }
            else if (dialogStatus == DialagoStatus.EndOfDialog)
            {
                DeactivateCanvas(sortedSpeakerList[dialogAsset.strings[index].id]);
            }
        }
    }

    //SALTA ALLA LINEA DI DIALOGO SUCCESSIVA TRAMITE INPUT (Q o (O/B - TASTO DESTRO CONTROLLER))
    public void SkipSentence()
    {
        if (dialogAsset != null)
        {
            StopCoroutine(coroutine);

            if (dialogStatus != DialagoStatus.EndOfDialog)
            {
                SetCanvasToActivate();

                SetTextMeshObj();
            }

            if (dialogStatus == DialagoStatus.Typing)
            {
                dialogTextObj.text = dialogAsset.strings[index].sentence;

                dialogStatus = DialagoStatus.EndOfSentence;
            }
            else if (dialogStatus == DialagoStatus.EndOfSentence)
            {
                dialogTextObj.text = dialogAsset.strings[index].sentence;
            }

            EndOfDialogCheck();
        }
    }

    //AGGIUNGE GLI SPEAKER AL DICTIONARY E ORDINA PER KEY
    public void AddSpeaker()
    {
        NPCScripts[] speakers = GameObject.FindObjectsOfType<NPCScripts>();

        //ADD PLAYER BEFORE NPCS
        speakerList.Add(0, GameObject.FindObjectOfType<PlayerScript>().gameObject);

        foreach (NPCScripts s in speakers)
        {
            speakerList.Add(s.id, s.gameObject);
        }

        foreach (KeyValuePair<int, GameObject> entry in speakerList.OrderBy(x => x.Key))
        {
            sortedSpeakerList.Add(entry.Key, entry.Value);
        }

        //Previus ID first save
        if (dialogAsset != null)
        {
            previusId = dialogAsset.strings[index].id;
        }
    }

    //ATTIVA IL CANCAS DELLO SPEAKER E IMPOSTA IL COLORE DEL FRAME
    private void ActivateCanvas(GameObject gameObject, Color color) 
    {
        DialogBox canvas = gameObject.GetComponentInChildren<DialogBox>();

        canvas.canvas.SetActive(true);

        //Debug.Log(color);

        Image sp = canvas.gameObject.GetComponentInChildren<Image>();

        sp.color = color;
    }

    //DISATTIVA IL CANVAS DELLO SPEAKER
    private void DeactivateCanvas(GameObject gameObject) 
    {
        DialogBox canvas = gameObject.GetComponentInChildren<DialogBox>();

        canvas.canvas.SetActive(false);
    }

    //CERCA IL TextMeshObj DELLO SPEAKER E RESETTA IL TESTO
    private void SetTextMeshObj()
    {
        dialogTextObj =
            sortedSpeakerList[dialogAsset.strings[index].id].gameObject.GetComponentInChildren<TextMeshProUGUI>();

        dialogTextObj.text = "";
    }

    //SCEGLIE IL CANVAS DA DISATTIVARE E QUELLO DA ATTIVARE 
    private void SetCanvasToActivate()
    {
        if (previusId != dialogAsset.strings[index].id)
        {
            DeactivateCanvas(sortedSpeakerList[previusId]);
            previusId = dialogAsset.strings[index].id;
        }

        ActivateCanvas(sortedSpeakerList[dialogAsset.strings[index].id], dialogAsset.strings[index].color);
    }

    //CONTROLLA SE IL DIALOGO E' FINITO, ALTRIMENTI AUMENTA INDEX
    private void EndOfDialogCheck()
    {
        if (index < dialogAsset.strings.Count - 1)
        {
            index++;
        }
        else
        {
            dialogStatus = DialagoStatus.EndOfDialog;
        }
    }

    public void SetPosition(GameObject setPoint)
    {
        Transform playerTransform = GameObject.FindObjectOfType<PlayerScript>().gameObject.transform;

        playerTransform.position = setPoint.transform.position;
    }
}
