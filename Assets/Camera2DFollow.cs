using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;

        private Vector3 positionOffset;

        private Vector3 rotationOffset;
        [SerializeField]
        Vector3 _velocityRef;

        public float _damping;

        private Vector3 _targetPos;

        private void Awake()
        {
            positionOffset = transform.position - target.position;
            rotationOffset = transform.localRotation.eulerAngles;
        }

        private void Update()
        {
            if (Player.Instance.Disabled)
                return;

            _targetPos = target.position;
            _targetPos.x = 0;

            Vector3 newPos = Vector3.SmoothDamp(transform.position, _targetPos + positionOffset, ref _velocityRef, _damping);
            transform.position = newPos;
        }
    }
}
