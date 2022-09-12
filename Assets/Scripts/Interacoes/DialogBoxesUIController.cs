using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogBoxesUIController :MonoBehaviour
{
    public bool PressedContinue { get; set; }
    public static DialogBoxesUIController instance;
    [SerializeField] private RectTransform GenericBoxDialog;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void OpenGenericBoxDialog(DialogData data)
    {
        var tmpText = GenericBoxDialog.GetComponentInChildren<TMP_Text>( );
        var seq = LeanTween.sequence( );
        var titleName = GenericBoxDialog.transform.Find("BGEntityName_Image").GetChild(0).GetComponent<TMP_Text>( );
        titleName.text = data.DialogEntityName;
        seq.append(() => MoveUP_OpenAnimation(GenericBoxDialog));
        seq.append(0.1f);
        seq.append(() => StartCoroutine(AnimateListOfTextMsg(data.messages , tmpText)));

    }
    public void CloseGenericBoxDialog()
    {
        StopAllCoroutines( );
        PressedContinue = false;
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

        for (int i = 0; i < data.Count; i++)
        {
            text.fontSize = data[i].FontSize;
            text.color = data[i].FontColor;
            if (data[i].FontType != null)
                text.font = data[i].FontType;
            text.text = data[i].MessageText;
            if (data.Count > 1)
            {

                if (i == data.Count - 1)
                {
                    while (PressedContinue == false)
                    {

                        Debug.Log(PressedContinue);
                        yield return null;

                    }
                    PressedContinue = false;
                    CloseGenericBoxDialog( );

                }
                else
                {
                    while (PressedContinue == false)
                    {

                        Debug.Log(PressedContinue);
                        yield return null;

                    }
                }

                PressedContinue = false;
            }
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

    public void SetPressedContinue()
    {
        PressedContinue = true;
        Debug.Log("Alo? " + PressedContinue);
    }
}
