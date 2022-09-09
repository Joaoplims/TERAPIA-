using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class NoiseTrigger :MonoBehaviour
{
    [SerializeField] private int codSFX;

    [SerializeField] private float defaultSize = 1;
    [SerializeField] private float maxSize = 5;
    [Tooltip("Pense nisso como uma explosão, o expandTime seria o tempo da explosão até a dissipação")]
    [SerializeField] private float expandTime = 2f;

    private SphereCollider sphereCollider;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>( );
        sphereCollider.isTrigger = true;
        transform.localScale = Vector3.one * defaultSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Alo Montro");
            var monster = other.GetComponent<ChaserMonster>( );
            monster.ChangeState(ChaserMonster.MonsterStates.FollowingNoise);
            monster.NoiseTarget = transform;
        }
    }

    [ContextMenu("EmitNoise")]
    public void EmitNoise()
    {
        AudioManager.Instancia.PlaySfx(codSFX);
        LeanTween.scale(gameObject , Vector3.one * maxSize , expandTime).setOnComplete(() => transform.localScale = Vector3.one * defaultSize);
    }

}
