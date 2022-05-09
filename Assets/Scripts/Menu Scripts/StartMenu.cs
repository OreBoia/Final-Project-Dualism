using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()//va alla scena dove inizia il gioco
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
