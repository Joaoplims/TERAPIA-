using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI :MonoBehaviour
{
    [SerializeField] private CanvasGroup intro;
    [SerializeField] private CanvasGroup mainButtons;

    private void Start()
    {
        LeanTween.alphaCanvas(mainButtons , 1f , 0.5f).setDelay(1.5f);
    }

    public void QuitGame()
    {
        Application.Quit( );
    }

    public void StartGame()
    {
        LeanTween.alphaCanvas(intro , 1f , 0.5f).setOnComplete(() => { intro.interactable = true; intro.blocksRaycasts = true; });
    }
    public void Continue() => SceneManager.LoadScene(1);
}
