using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogBoxesUIController :MonoBehaviour
{
    public static DialogBoxesUIController instance;
    [SerializeField] private RectTransform GenericBoxDialog;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void OpenGenericBoxDialog(DialogData data)
    {
        MoveUP_OpenAnimation(GenericBoxDialog);
        var tmpText = GenericBoxDialog.GetComponentInChildren<TMP_Text>( );
        StartCoroutine(AnimateListOfTextMsg(data.messages , tmpText));
    }
    public void CloseGenericBoxDialog()
    {
        MoveDown_CloseAnimation(GenericBoxDialog);
    }

    private void MoveUP_OpenAnimation(RectTransform animatedBox)
    {
        LeanTween.moveY(animatedBox , 40f , 0.5f);

    }

    private void MoveDown_CloseAnimation(RectTransform animatedBox)
    {
        LeanTween.moveY(animatedBox , -500f , 0.5f);

    }

    private IEnumerator AnimateListOfTextMsg(List<MessageData> data , TMP_Text text)
    {

        foreach (var item in data)
        {
            text.fontSize = item.FontSize;
            text.color = item.FontColor;
            if (item.FontType != null)
                text.font = item.FontType;
            yield return AnimateText(item.MessageText , text);

        }
    }
    private IEnumerator AnimateText(string msg , TMP_Text text)
    {

        string msgAux = "";
        for (int i = 0; i < msg.Length; i++)
        {
            msgAux += msg[i];
            text.text = msgAux;
            yield return new WaitForSeconds(0.1f);
        }



    }
}
