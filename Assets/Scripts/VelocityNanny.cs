using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityNanny : MonoBehaviour
{

    public bool Active = true;
    private Rigidbody2D _rBody;

    public float SmoothingX;
    public float SmoothingY = 1;

    private Vector2 _targetVelocityX = Vector3.zero;
    private float _targetVelocityY = 20f;

    private Vector3 velocityRefX;
    private Vector3 velocityRefY;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (!Active)
            return;

        var newVelocityX = Vector3.SmoothDamp(_rBody.velocity, _targetVelocityX, ref velocityRefX, SmoothingX);

        var newVelocityY = _rBody.velocity;

        if (_rBody.velocity.y > 20)
        {
            newVelocityY = Vector3.SmoothDamp(_rBody.velocity, new Vector2(_targetVelocityX.x, _targetVelocityY), ref velocityRefY, SmoothingY);
        }

        newVelocityX.y = _rBody.velocity.y;

        _rBody.velocity = new Vector2(newVelocityX.x, newVelocityY.y);
        //if (_rBody.velocity.x > 0)
        //{
        //    var newVelocity2 = _rBody.velocity;
        //    newVelocity2.x -= 0.1f;

        //    if (_rBody.velocity.x - newVelocity2.x < 0)
        //        return;

        //    _rBody.velocity = newVelocity2;
        //}
    }

}
