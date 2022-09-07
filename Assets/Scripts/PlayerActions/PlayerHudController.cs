using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudController :MonoBehaviour
{
    [SerializeField] private CanvasGroup endGamePanel;
    [SerializeField] private TMP_Text maxPillsText;
    [SerializeField] private TMP_Text collectedPillsText;
    [SerializeField] private Slider staminaSlider;



    public void SetMaxPills(int maxPills) => maxPillsText.text = maxPills.ToString( );
    public void SetCollectedPills(int collected) => collectedPillsText.text = collected.ToString( );
    public void SetStaminaValue(float ammout) => staminaSlider.value = ammout;
    public void SetMaxStamina(float maxValue)
    {
        staminaSlider.maxValue = maxValue;
        staminaSlider.transform.localScale = new Vector3(maxValue, 1f,1f);
        

    }

    public void ShowEndGamePanel()
    {
        LeanTween.alphaCanvas(endGamePanel, 1f, 0.5f);

    }

}