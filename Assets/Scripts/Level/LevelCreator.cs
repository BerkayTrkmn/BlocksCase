using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public enum LevelDataUse
{
    Inspector,
    Json,
    ScriptableObject
}
public class LevelCreator : MonoBehaviour
{
    private GridCreator gridCreator;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private List<Sprite> tileSprites;
    LevelData data;

    [SerializeField] private Vector2 gridSize;
   
    [SerializeField] private Vector2 tilelength;
    [SerializeField] private float spaceLength;
    [SerializeField] private Vector2 middlePoint;

    void Start()
    {
       data = new LevelData() { GridSize = gridSize, Tilelength = tilelength, SpaceLength = spaceLength, MiddlePoint = middlePoint };
        gridCreator = new GridCreator(data.GridSize, tilePrefab, data.Tilelength, data.SpaceLength, tileSprites, data.MiddlePoint);
        gridCreator.CreateGrid();
        
     
    }

   
}
public class LevelData
{
     public Vector2 GridSize;
     public Vector2 Tilelength;
     public float SpaceLength;
     public Vector2 MiddlePoint;
}
