using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Whiskers : MonoBehaviour
{


    public List<Collider> IgnoreList = new List<Collider>();
    public float ExplodeForce = 400;

    public bool _allowShakeCam = true;

    private void OnTriggerEnter(Collider other)
    {
        if (IgnoreList.Contains(other))
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer("ShatterMesh"))
        {


            IgnoreList.Add(other);
            //Debug.Log("HitPiece");
            var rBody = other.GetComponent<Rigidbody>();
            rBody.isKinematic = false;
            AddForce(new Vector3(0, 3, -1) * ExplodeForce, rBody, 4);
            ShakeCam();

        }
    }


    private void ShakeCam()
    {
        if (!_allowShakeCam)
            return;

        _allowShakeCam = false;

        Player.Instance.CameraShakeScript.ShakeCamera(1, 0.1f);

        Invoke("AllowShakeCam", 1);
    }

    private void AllowShakeCam()
    {
        _allowShakeCam = true;
    }

    public void AddForce(Vector3 force, Rigidbody rBody, int overIterations = 1, int timeStep = 0)
    {
        StartCoroutine(AddForceCoroutine(force, rBody, overIterations, timeStep));
    }

    private IEnumerator AddForceCoroutine(Vector3 force, Rigidbody rBody, int overIterations = 1, int timeStep = 0)
    {
        force = force / overIterations;

        for (int i = 0; i < overIterations; i++)
        {
            //Debug.Log(force);
            rBody.AddExplosionForce(ExplodeForce, transform.position, 20);
            //rBody.AddTorque(force.x);

            if (timeStep == 0)
                yield return new WaitForFixedUpdate();
            else
                yield return new WaitForSeconds(timeStep);
        }
    }

}
