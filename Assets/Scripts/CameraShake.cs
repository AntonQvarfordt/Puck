///Daniel Moore (Firedan1176) - Firedan1176.webs.com/
///26 Dec 2015
///
///Shakes camera parent object

using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    public bool debugMode = false;//Test-run/Call ShakeCamera() on start

    private float shakeAmount;//The amount to shake this frame.
    private float shakeDuration;//The duration this frame.

    //Readonly values...
    float shakePercentage;//A percentage (0-1) representing the amount of shake to be applied when setting rotation.
    float startAmount;//The initial shake amount (to determine percentage), set when ShakeCamera is called.
    float startDuration;//The initial shake duration, set when ShakeCamera is called.

    bool isRunning = false; //Is the coroutine running right now?

    public bool smooth;//Smooth rotation?
    public float smoothAmount = 5f;//Amount to smooth

    void Start()
    {

        if (debugMode) ShakeCamera();
    }


    void ShakeCamera()
    {

        startAmount = shakeAmount;//Set default (start) values
        startDuration = shakeDuration;//Set default (start) values

        if (!isRunning) StartCoroutine(Shake());//Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
    }

    public void ShakeCamera(float amount, float duration)
    {

        shakeAmount += amount;
        startAmount = shakeAmount;
        shakeDuration += duration;
        startDuration = shakeDuration;

        if (!isRunning) StartCoroutine(Shake());
    }


    IEnumerator Shake()
    {
        isRunning = true;

        while (shakeDuration > 0.01f)
        {
        

            Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;
            rotationAmount.z = 0;

            shakePercentage = shakeDuration / startDuration;

            shakeAmount = startAmount * shakePercentage;
            shakeDuration -= Time.deltaTime;


            if (smooth)
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
            else
                transform.localRotation = Quaternion.Euler(rotationAmount);//Set the local rotation the be the rotation amount.

            yield return new WaitForEndOfFrame();
        }
        transform.localRotation = Quaternion.identity;//Set the local rotation to 0 when done, just to get rid of any fudging stuff.
        isRunning = false;
    }

}