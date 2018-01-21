using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseTrailColor : MonoBehaviour
{


    public Color MinColor;
    public Color MaxCOlor;


    public float MaxVelocity;

    public float PercentageValue;

    public MeshRenderer ApplyTo;

    public void Update()
    {
        PercentageValue = Player.Instance.GetVelocity / MaxVelocity;
        var newColor = Color.Lerp(MinColor, MaxCOlor, PercentageValue);

        ApplyTo.material.SetColor("_EmissionColor", newColor *1.5f);
    }
}
