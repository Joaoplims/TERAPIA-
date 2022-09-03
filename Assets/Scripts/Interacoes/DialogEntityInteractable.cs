using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEntityInteractable :InteractableObject
{
    public DialogData dialogData;
    public override void Interact()
    {
        Debug.Log("<DialogEntityInteractable> Iniciando Dialogo");
        DialogBoxesUIController.instance.OpenGenericBoxDialog(dialogData);
    }
}
