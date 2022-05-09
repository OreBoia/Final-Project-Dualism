using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventScript eventObj = collision.gameObject.GetComponent<EventScript>();

        if (eventObj != null)
        {
            //Debug.Log("COLLISION " + collision);
            DialogController.Instance.dialogAsset = eventObj.dialogAsset;
            DialogController.Instance.AddSpeaker();
            DialogController.Instance.setPointObj = eventObj.setPoint;
            //GAME STATUS = INDIALOG
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EventScript eventObj = collision.gameObject.GetComponent<EventScript>();

        Debug.Log("COLLISION " + collision);

        if (eventObj != null)
        {
            DialogController.Instance.dialogAsset = null;
            //aggiungere controllo quando non c'è il dialog asset
        }
    }
}
