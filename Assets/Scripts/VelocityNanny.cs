using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityNanny : MonoBehaviour
{

    public bool Active = true;
    private Rigidbody2D _rBody;

    public float SmoothingX;
    public float SmoothingY = 1;

    private float _targetVelocityX = 0f;
    private float _targetVelocityY = 10f;

    private float velocityRefX;
    private float velocityRefY;

    private bool _disableConfigured;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (!Active)
            return;

        if (Player.Instance.Disabled && !_disableConfigured)
        {
            DisableConfigure();
        }

        var newVelocity = _rBody.velocity;

        newVelocity.x = Mathf.SmoothDamp(newVelocity.x, _targetVelocityX, ref velocityRefX, SmoothingX);

        if (_rBody.velocity.y > 10f && !_disableConfigured)
        {
            newVelocity.y = Mathf.SmoothDamp(_rBody.velocity.y, _targetVelocityY, ref velocityRefY, SmoothingY);
        }
        else if (_disableConfigured)
        {
            newVelocity.y = Mathf.SmoothDamp(_rBody.velocity.y, _targetVelocityY, ref velocityRefY, SmoothingY);
        }

        _rBody.velocity = newVelocity;
        //if (_rBody.velocity.x > 0)
        //{
        //    var newVelocity2 = _rBody.velocity;
        //    newVelocity2.x -= 0.1f;

        //    if (_rBody.velocity.x - newVelocity2.x < 0)
        //        return;

        //    _rBody.velocity = newVelocity2;
        //}
    }

    private void DisableConfigure()
    {
        Debug.Log("Disable Configuring");

        _targetVelocityY = 0f;
        SmoothingX = 0.1f;
        SmoothingY = 0.1f;
        _disableConfigured = true;
    }

}
