using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager :MonoBehaviour
{
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }


    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame){
            Application.Quit();
        }
    }
}
