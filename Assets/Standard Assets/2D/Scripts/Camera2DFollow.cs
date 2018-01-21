using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;

        private Vector3 offset;
        [SerializeField]
        Vector3 _velocityRef;

        public float _damping;

        private Vector3 _targetPos;

        private void Awake()
        {
            offset = transform.position - target.position;
        }

        private void Update()
        {
            _targetPos = target.position;
            _targetPos.x = 0;

            Vector3 newPos = Vector3.SmoothDamp(transform.position, _targetPos + offset, ref _velocityRef, _damping);
            transform.position = newPos;
        }
    }
}
