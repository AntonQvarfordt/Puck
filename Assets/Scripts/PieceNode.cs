using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceNode : MonoBehaviour {

    private Obstacle _attachedObstacle;

    private bool _occupied;

    public bool IsOccupied
    {
        get { return _occupied; }
    }

    public Obstacle GetAttachment
    {
        get { return _attachedObstacle; }
    }

    public void AttachObstacle (Obstacle obs)
    {
        _attachedObstacle = obs;

        obs.transform.position = transform.position;
        obs.Init();
    }



}
