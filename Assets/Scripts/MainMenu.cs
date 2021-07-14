using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Loader.Load(Loader.Scene.CharacterSelect);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
