using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowQ : MonoBehaviour {

    public float zPos;
    public Transform Target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 _offset;

    private void Awake()
    {
        _offset = transform.position - Target.position;
    }

    void Update()
    {
        Vector3 targetPosition = Target.TransformPoint(new Vector3(0, 5, -10));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
