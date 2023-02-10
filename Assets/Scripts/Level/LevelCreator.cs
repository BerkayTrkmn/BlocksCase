using System;
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
public delegate void LevelStateEvent();
public class LevelCreator : MonoBehaviour
{
    public static LevelCreator instance;
    [HideInInspector]public GridCreator gridCreator;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private List<Sprite> tileSprites;
    LevelData data;

    [SerializeField] private Vector2 gridSize;
   
    [SerializeField] private Vector2 tilelength;
    [SerializeField] private float spaceLength;
    [SerializeField] private Vector2 middlePoint;

    [HideInInspector] public Dictionary<Vector2, Tile> tileGrid;

    public static event LevelStateEvent onLevelCompleted;

    private void Awake()
    {
        if(instance == null)
        instance = this;
    }

    void Start()
    {
       data = new LevelData() { GridSize = gridSize, Tilelength = tilelength, SpaceLength = spaceLength, MiddlePoint = middlePoint };
        gridCreator = new GridCreator(data.GridSize, tilePrefab, data.Tilelength, data.SpaceLength, tileSprites, data.MiddlePoint);
        tileGrid = gridCreator.CreateGrid();
        
     
    }
    private void OnEnable()
    {
        Tetrimino.onTetriminoInserted += CheckLevelEnd;
    }

    private void CheckLevelEnd()
    {
        if (IsLevelEnded())
            onLevelCompleted?.Invoke();
    }

    public bool IsLevelEnded()
    {
        foreach (var tile in tileGrid)
        {
            foreach (var tilePart in tile.Value.TilePartsOccupancy)
            {
                if (!tilePart.Value) return false;
            }
        }
        return true;
    }
    private void OnDisable()
    {
        Tetrimino.onTetriminoInserted -= CheckLevelEnd;
    }
}
public class LevelData
{
     public Vector2 GridSize;
     public Vector2 Tilelength;
     public float SpaceLength;
     public Vector2 MiddlePoint;
}
