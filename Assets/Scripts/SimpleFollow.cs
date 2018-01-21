using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour {

    public Transform Target;
    private Vector3 _offset;

    public int xPos;

    private void Awake ()
    {
        _offset = Target.position - transform.position;
    }

	void Update () {

        var newPos = Target.position - _offset;
        newPos.x = xPos;
        transform.position = newPos;

	}
}
