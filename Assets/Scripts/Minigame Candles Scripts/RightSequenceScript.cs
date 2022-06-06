using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSequenceScript : MonoBehaviour
{
    public bool[] rightPosCheck = { false, false, false, false, false, false };

    private static RightSequenceScript _instance;

    public static RightSequenceScript Instance
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

    public void SetRightPosTrue(int ind)
    {
        rightPosCheck[ind] = true;
        RightSequenceCheck();
    }

    public void SetRightPosFalse(int ind)
    {
        rightPosCheck[ind] = false;
    }

    public void RightSequenceCheck()
    {
        for (int i = 0; i < rightPosCheck.Length - 1; i++)
        {
            if (rightPosCheck[i] == false)
            {
                return;
            }
        }

        Debug.Log("WIN");
    }
}
