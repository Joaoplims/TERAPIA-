using System.Collections;
using System.Collections.Generic;
using Terapia;
using UnityEngine;
using UnityEngine.Events;

public class Pill : MonoBehaviour
{
    public UnityEvent OnUse;

    [SerializeField] private DebuffTypes debuff;
    [SerializeField] private int codSfx;



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.Instancia.PlaySfx(codSfx);
            other.GetComponent<PlayerActions>().SetDebuff(debuff);
            OnUse?.Invoke();
            Destroy(gameObject);
        }
    }




    public enum DebuffTypes{
        LooseBreath = 0,
        InvertControll = 1,
        ScreenShake = 2
    }

}
