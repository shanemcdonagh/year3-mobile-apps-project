using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
         FindObjectOfType<SceneController>().PlayGame();
    }
}
