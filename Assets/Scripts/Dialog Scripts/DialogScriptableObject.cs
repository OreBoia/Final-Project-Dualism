using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emotion { Idle, Sad, Angry, Happy }

public class DialogScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class DialogString
    {
        public string sentence;
        public int id;
        public Emotion emotion;
        public Color colorFrame;
        public Color colorText;
    }

    public List<DialogString> strings = new List<DialogString>();

}
