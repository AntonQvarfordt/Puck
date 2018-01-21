using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistanceDestroy))]
public class Piece : MonoBehaviour {

    public PieceNode[] Nodes;

    public Transform AttachPoint;
    public Piece DerivesFrom;
    //public Piece ConnectsTo;

    public PieceNode GetOpenNode ()
    {
        var openNodes = new List<PieceNode>();

        foreach (PieceNode node in Nodes)
        {
            if (node.IsOccupied)
                continue;

            openNodes.Add(node);
        }

        return openNodes[Random.Range(0, openNodes.Count - 1)];
    }

    private void OnDestroy ()
    {
        LevelDirector.Instance.WallPieces.Remove(this);
    }

}
