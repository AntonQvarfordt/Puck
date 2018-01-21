using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityStandardAssets._2D;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static Player Instance;

    public int BoostTime;

    public HeadColorController HeadColors;
    public Animator VisAnimation;

    public Vector2 HorizontalSwipeForce;
    public Vector2 VerticalSwipeForce;
    public float ForceMultiplier;
    public ParticleSystem BoostParticles;

    public float DragSmoothTime = 0.1f;

    public InputField VelocityDebug;
    public CameraShake CameraShakeScript;

    public float GetVelocity
    {
        get { return _velocityMagnitude; }
    }

    [SerializeField]
    private bool _allowBoost = true;
    [SerializeField]
    private bool _allowWallBoost = true;

    private Rigidbody2D _rigidbody;
    private float _velocityMagnitude;
    private float _defaultDrag;
    private float _refVelocity;

    private void Awake()
    {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody2D>();
        _defaultDrag = _rigidbody.drag;
        
    }

    private void Start()
    {
        _rigidbody.velocity = new Vector2(0, 10);
    }

    private void Update()
    {
        _velocityMagnitude = _rigidbody.velocity.magnitude;

        //if (_rigidbody.drag != _defaultDrag)
        //{
        //    _rigidbody.drag = Mathf.SmoothDamp(_rigidbody.drag, _defaultDrag, ref _refVelocity, DragSmoothTime);
        //}
    }

    //private void FixedUpdate()
    //{
    //    _rigidbody.velocity = new Vector2(Mathf.Clamp(_rigidbody.velocity.x, -40f, 40f), Mathf.Clamp(_rigidbody.velocity.y, 0f, 20));
    //}

    public void SwipeLeft()
    {
        if (!_allowBoost)
            return;

        AddForce(new Vector2(-HorizontalSwipeForce.x, 0) * ForceMultiplier * 4, 3);

        DisableBoost();
    }

    public void SwipeRight()
    {
        if (!_allowBoost)
            return;

        AddForce(new Vector2(HorizontalSwipeForce.x, 0) * ForceMultiplier * 4, 3);

        DisableBoost();

    }

    public void SwipeUp()
    {
        if (!_allowBoost)
            return;

        AddForce(new Vector2(VerticalSwipeForce.x, VerticalSwipeForce.y) * ForceMultiplier, 3);

        BoostParticles.Play();

        DisableBoost();
    }

    public void AddForce(Vector2 force, int overIterations = 10, int timeStep = 0)
    {
        StartCoroutine(AddForceCoroutine(force, overIterations, BoostTime / overIterations));
    }

    private IEnumerator AddForceCoroutine(Vector2 force, int overIterations = 1, int timeStep = 0)
    {
        if (_velocityMagnitude < 1)
        {
            overIterations += 2;
        }

        for (int i = 0; i < overIterations; i++)
        {
            if (i > 0)
                _rigidbody.AddForce(force / i);

            if (timeStep == 0)
                yield return new WaitForFixedUpdate();
            else
                yield return new WaitForSeconds(timeStep);
        }

        CameraShakeScript.ShakeCamera(0.6f, 0.3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (!_allowWallBoost)
                return;

            _allowWallBoost = false;

            WallBoost();


        }
    }

    private IEnumerator TimedUnlock(float unlockAfterSeconds, Action unlockAction)
    {
        yield return new WaitForSeconds(unlockAfterSeconds);
        unlockAction.Invoke();
    }

    private void WallBoost()
    {
        float horizontalBoost = 0;

        if (transform.position.x > 0)
        {
            horizontalBoost = -1;
        }
        else
        {
            horizontalBoost = 1;
        }

        Debug.Log("WallBoost");

        AddForce(new Vector2(horizontalBoost*1, VerticalSwipeForce.y) * ForceMultiplier * 0.25f, 3);

        StartCoroutine(TimedUnlock(1, () => _allowWallBoost = true));
    }

    public void DisableBoost ()
    {
        HeadColors.EmptyColors();
        _allowBoost = false;
        StartCoroutine(TimedUnlock(BoostTime, EnableBoost));
    }
         
    public void EnableBoost()
    {
        _allowBoost = true;
        HeadColors.IdleColors();
    }
}
