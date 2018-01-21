//
// Fingers Gestures
// (c) 2015 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DigitalRubyShared;

public class Swipe : MonoBehaviour
{
    public Player PlayerScript;

    [Tooltip("Set the required touches for the swipe.")]
    [Range(1, 10)]
    public int SwipeTouchCount = 1;

    [Tooltip("Controls how the swipe gesture ends. See SwipeGestureRecognizerSwipeMode enum for more details.")]
    public SwipeGestureRecognizerEndMode SwipeMode = SwipeGestureRecognizerEndMode.EndImmediately;

    private SwipeGestureRecognizer swipe;

    private void Start()
    {
        swipe = new SwipeGestureRecognizer();
        swipe.StateUpdated += Swipe_Updated;
        swipe.DirectionThreshold = 0;
        swipe.MinimumNumberOfTouchesToTrack = swipe.MaximumNumberOfTouchesToTrack = SwipeTouchCount;
        FingersScript.Instance.AddGesture(swipe);
        TapGestureRecognizer tap = new TapGestureRecognizer();
        //tap.StateUpdated += Tap_Updated;
        FingersScript.Instance.AddGesture(tap);
    }

    private void Update()
    {
        swipe.MinimumNumberOfTouchesToTrack = swipe.MaximumNumberOfTouchesToTrack = SwipeTouchCount;
        swipe.EndMode = SwipeMode;
    }

    //private void Tap_Updated(GestureRecognizer gesture)
    //{
    //    if (gesture.State == GestureRecognizerState.Ended)
    //    {

    //    }
    //}

    private void Swipe_Updated(GestureRecognizer gesture)
    {
        SwipeGestureRecognizer swipe = gesture as SwipeGestureRecognizer;
        if (swipe.State == GestureRecognizerState.Ended)
        {
            float angle = Mathf.Atan2(-swipe.DistanceY, swipe.DistanceX) * Mathf.Rad2Deg;
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(gesture.StartFocusX, gesture.StartFocusY, 0.0f));
            pos.z = 0.0f;
           
            //Debug.Log(angle);
            //Debug.Log(pos);

            var hMovement = Mathf.Abs(swipe.DeltaX);
            var vMovement = Mathf.Abs(swipe.DeltaY);

            if (hMovement > vMovement)
            {
                if (swipe.DeltaX > 0)
                {
                    RightSwipe();
                }
                else
                {
                    LeftSwipe();
                }
            }
            else
            {
                if (swipe.DeltaY > 0)
                {
                    UpSwipe();
                }
                else
                {
                    DownSwipe();
                }
            }
        }
    }

    private void LeftSwipe ()
    {
        Debug.Log("<<<<<<<<<");
        PlayerScript.SwipeLeft();
    }

    private void RightSwipe()
    {
        Debug.Log(">>>>>>>>>");
        PlayerScript.SwipeRight();
    }

    private void UpSwipe()
    {
        Debug.Log("^^^^^^^^");
        PlayerScript.SwipeUp();
    }

    private void DownSwipe()
    {
        Debug.Log("vvvvvvv");
    }
}
