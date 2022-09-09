using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake :MonoBehaviour
{
    public static CameraShake instance;
    private float jumpIter = 9.5f;

    private void Awake()
    {
        if(instance == null) instance = this;
    }
    public void Shake(float shakeTime)
    {
        float height = Mathf.PerlinNoise(jumpIter , 0f) * 10f;
        height = height * height * 0.3f;


        /**************
        * Camera Shake
        **************/

        float shakeAmt = height * 0.2f; // the degrees to shake the camera
        float shakePeriodTime = 0.15f; // The period of each shake
        //float dropOffTime = 1.6f; // How long it takes the shaking to settle down to nothing
        var dir = new Vector3(Random.Range(-1f , 1f) , Random.Range(-1f , 1f) , Random.Range(-1 , 1));
        LTDescr shakeTween = LeanTween.rotateAroundLocal(gameObject , Vector3.right , shakeAmt , shakePeriodTime)
        .setEase(LeanTweenType.easeShake) // this is a special ease that is good for shaking
        .setLoopClamp( )
        .setRepeat(-1);

        // Slow the camera shake down to zero
        LeanTween.value(gameObject , shakeAmt , 0f , shakeTime).setOnUpdate(
            (float val) =>
            {
                shakeTween.setTo(Vector3.right * val);
            }
        ).setEase(LeanTweenType.easeOutQuad);

    }
}
