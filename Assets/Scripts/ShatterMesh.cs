using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterMesh : MonoBehaviour
{
    public float EpxlosionForce = 500;
    public List<Transform> Children = new List<Transform>();

    public void SetShatterReady()
    {
        Children.Clear();
        foreach (Transform t in transform)
        {
            if (t == transform)
                continue;

            Children.Add(t);
            var rBody = t.gameObject.AddComponent<Rigidbody>();
            var meshCollider = t.gameObject.AddComponent<MeshCollider>();
            meshCollider.convex = true;
            rBody.sleepThreshold = 50;

            rBody.Sleep();


        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Exploding Box Because Of " + other.collider.name);

        ReleaseKinematics(other.contacts[other.contacts.Length - 1].point, other);
    }

    public void ReleaseKinematics(Vector3 forcePosition, Collision2D other = null)
    {
        Destroy(GetComponent<BoxCollider>());

        var children = gameObject.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody childR in children)
        {
            if (childR == transform)
                continue;

            childR.isKinematic = false;
            if (other != null)
                childR.AddExplosionForce(EpxlosionForce, other.transform.position, 3);

        }

        Debug.Break();
    }
}
