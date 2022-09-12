using System.Collections;
using System.Collections.Generic;
using Terapia;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager :MonoBehaviour
{
    [SerializeField] private CanvasGroup finishGamePanel;
    [SerializeField] private PlayerActions player;
    [SerializeField] private ChaserMonster monster;
    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }



    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit( );
        }
    }

    public void FinalizarGame()
    {
        if (player.Pills >= 3)
        {
            player.LockInput = true;
            monster.LockInput = true;
            LeanTween.alphaCanvas(finishGamePanel , 1f , 0.5f).setOnComplete(() => { finishGamePanel.interactable = true; finishGamePanel.blocksRaycasts = true; });
            
        }

    }
}
