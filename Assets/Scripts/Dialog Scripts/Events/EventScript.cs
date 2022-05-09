using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour
{
    public DialogScriptableObject dialogAsset;
    public GameObject setPoint;

    private static EventScript _instance;

    public static EventScript Instance
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
}
