using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterBlock : MonoBehaviour
{
    public float EpxlosionFOrce = 20;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;
        var children = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody childR in children)
        {
            childR.isKinematic = false;
            childR.AddExplosionForce(EpxlosionFOrce, other.transform.position, 5f);
        }
    }
}
