using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudController :MonoBehaviour
{
    [SerializeField] private CanvasGroup endGamePanel;
    [SerializeField] private CanvasGroup screenShakeFX;
    [SerializeField] private TMP_Text maxPillsText;
    [SerializeField] private TMP_Text collectedPillsText;
    [SerializeField] private Slider staminaSlider;



    public void SetMaxPills(int maxPills) => maxPillsText.text = maxPills.ToString( );
    public void SetCollectedPills(int collected) => collectedPillsText.text = collected.ToString( );
    public void SetStaminaValue(float ammout) => staminaSlider.value = ammout;
    public void SetMaxStamina(float maxValue)
    {
        staminaSlider.maxValue = maxValue;
        staminaSlider.transform.localScale = new Vector3(maxValue , 1f , 1f);


    }

    public void ShowEndGamePanel()
    {
        LeanTween.alphaCanvas(endGamePanel , 1f , 0.5f).setOnComplete(() => { endGamePanel.interactable = true; endGamePanel.blocksRaycasts = true; });

    }

    public void ShowScreenShakeFX(Action onComplete)
    {
        var seq = LeanTween.sequence( );
        seq.append(LeanTween.alphaCanvas(screenShakeFX , 1f , 0.5f).setLoopPingPong( ));
        seq.append(3f);
        seq.append(() => LeanTween.cancel(screenShakeFX.gameObject));
        seq.append(() => screenShakeFX.alpha = 0);
        seq.append(() => onComplete?.Invoke( ));

    }

}
