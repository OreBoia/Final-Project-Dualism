using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus { Running, Paused, Dialog, GameOver }

public class GameController : MonoBehaviour
{

    public GameStatus gameStatus = GameStatus.Running;
   
}
