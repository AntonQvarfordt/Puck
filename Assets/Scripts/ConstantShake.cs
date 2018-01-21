using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantShake : MonoBehaviour
{

    public bool debugMode = false;//Test-run/Call ShakeCamera() on start

    private float shakeAmount = 1;//The amount to shake this frame.

    //Readonly values...
    [SerializeField]
    float shakePercentage;//A percentage (0-1) representing the amount of shake to be applied when setting rotation.

    float startDuration;//The initial shake duration, set when ShakeCamera is called.

    public bool smooth;//Smooth rotation?
    public float smoothAmount = 5f;//Amount to smooth

    void Update()
    {
        Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;//A Vector3 to add to the Local Rotation
        rotationAmount.z = 0;//Don't change the Z; it looks funny.

        shakePercentage = Player.Instance.GetVelocity / 20;

        shakeAmount *= shakePercentage;//Set the amount of shake (% * startAmount).


        if (smooth)
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
        else
            transform.localRotation = Quaternion.Euler(rotationAmount);//Set the local rotation the be the rotation amount.    }

    }
} 
