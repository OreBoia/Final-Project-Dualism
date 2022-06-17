using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //INSTANCE
    private static PlayerScript _instance;

    public static PlayerScript Instance
    {
        get
        {
            return _instance;
        }
    }
}
