using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager :MonoBehaviour
{
    public static AudioManager Instancia;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource ambientSource;

    [SerializeField] private List<AudioClip> sfxClips;
    [SerializeField] private List<AudioClip> ambientClips;
    // Start is called before the first frame update
    void Start()
    {
        if (Instancia == null)
        {
            Instancia = this;
        }
    }

    public void PlaySfx(int codSfx)
    {
        sfxSource.PlayOneShot(sfxClips[codSfx]);
    }

    public void PlayAmbientSound(int codAmbient){
        ambientSource.Stop();
        ambientSource.clip = ambientClips[codAmbient];
        ambientSource.Play();
    }


}
