using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour
{
    public PlacePointScript actualPosition;

    Rigidbody2D rb;

    public PlacePointScript rightPlacePoint;
    bool rightPosition;

    public string[] actualPosNameSplit;
    public string[] rightPosNameSplit;

    private void Start()
    {
        actualPosNameSplit = new string[2];
        rightPosNameSplit = new string[2];

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        PlacePointScript placePoint = collision.gameObject.GetComponent<PlacePointScript>();

        if (placePoint != null)
        {
            actualPosition = placePoint.gameObject.GetComponent<PlacePointScript>();

            actualPosNameSplit = actualPosition.name.Split('-');
            rightPosNameSplit = rightPlacePoint.gameObject.name.Split('-');

            if (actualPosition.name == rightPlacePoint.gameObject.name)
            {
                RightSequenceScript.Instance.SetRightPosTrue(Int32.Parse(actualPosNameSplit[1]) - 1);
            }
            else
            {
                RightSequenceScript.Instance.SetRightPosFalse(Int32.Parse(rightPosNameSplit[1]) - 1);
            }
        }
    }

    public void SwapCandlePosition(Vector2 nextPosition)
    {
        rb.MovePosition(nextPosition);
    }
}
