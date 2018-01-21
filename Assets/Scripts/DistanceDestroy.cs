using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDestroy : MonoBehaviour {

    public Transform Target;
    public GameObject DestroyOverrideTarget;
    public float Distance;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        var dDestroy = GetComponent<DistanceDestroy>();
        dDestroy.Distance = 100;
        dDestroy.Target = Player.Instance.transform;
    }

    void Update () {

        if (Target == null)
            return;

        if (Vector2.Distance(transform.position, Target.position) > Distance)
        {
            DStroy();
        }
		
	}

    private void DStroy ( )
    {
        if (!DestroyOverrideTarget)
        {
            Destroy(gameObject);
            return;
        }
        Destroy(DestroyOverrideTarget);
        Destroy(this);
    }
}
