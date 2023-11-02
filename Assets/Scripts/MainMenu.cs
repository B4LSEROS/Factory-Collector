using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Allows for the access of scenes
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //called when the play button is clicked.
    public void PlayGame()
    {
        //Loads the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();

    }
}
