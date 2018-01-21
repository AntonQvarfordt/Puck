using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : MonoBehaviour
{
    public GameObject[] Obstacles;

    public static LevelDirector Instance;

    public GameObject WallPrefab;
    public Transform WallsRoot;

    public List<Piece> WallPieces = new List<Piece>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        var shouldSpawnTile = CheckSpawnNextTile();

        if (shouldSpawnTile)
        {
            var wallP = AttachWallPiece();
            PopulateWithObstacles(wallP);
            WallPieces.Add(wallP);
        }
    }

    public Piece AttachWallPiece()
    {
        var wallPiece = Instantiate(WallPrefab);
        var wallScript = wallPiece.GetComponent<Piece>();
        wallPiece.transform.SetParent(WallsRoot);


        if (WallPieces.Count != 0)
        {
            wallScript.DerivesFrom = WallPieces[WallPieces.Count - 1];
            wallPiece.transform.position = wallScript.DerivesFrom.AttachPoint.position;

        }
        else
        {
            wallPiece.transform.position = new Vector3(0, 0, 0 );

        }

        return wallScript;
    }

    private bool CheckSpawnNextTile()
    {
        if (WallPieces.Count == 0)
            return true;

        if (Player.Instance.transform.position.y >= WallPieces[WallPieces.Count - 1].transform.position.y)
            return true;
        else
            return false;
    }

    public void PopulateWithObstacles (Piece wallPiece)
    {
        var obstacleAmount = Random.Range(0, 3);

        if (obstacleAmount > 1)
        {
            obstacleAmount = Random.Range(0, 3);
        }

        if (obstacleAmount == 3)
        {
            if (Random.Range(0, 2) == 0)
                obstacleAmount = 3;
            else
                obstacleAmount = Random.Range(0, 1);
        }

        var obstacles = new List<GameObject>();
        
        for (int i = 0; i < obstacleAmount; i++)
        {
            var obsInstance = Instantiate(Obstacles[Random.Range(0, Obstacles.Length)]);
            obstacles.Add(obsInstance);
        }

        foreach (GameObject obs in obstacles)
        {
   
            var node = wallPiece.GetOpenNode();
            if (node == null)
                return;
            //Debug.Log(obs.name);
            node.AttachObstacle(obs.GetComponent<Obstacle>());
        }
    }
}
