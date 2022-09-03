using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogData :ScriptableObject
{
    public string DialogEntityName;
    public Sprite DialogEnityPicture;
    public List<MessageData> messages;

}

[System.Serializable]
public class MessageData
{
    [TextArea(3 , 10)]
    public string MessageText;
    public Color FontColor;
    public float FontSize = 14;
    public TMPro.TMP_FontAsset FontType;
}
