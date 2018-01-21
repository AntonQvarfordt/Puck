using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistanceDestroy))]
public class SmashWallObstacle : Obstacle
{
    public override void Init()
    {
        var dDestroy = GetComponent<DistanceDestroy>();
        dDestroy.Target = Player.Instance.transform;
    }
}
