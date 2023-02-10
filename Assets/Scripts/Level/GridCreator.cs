using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloScripts;

public class GridCreator
{
    private Vector2 _gridSize;
    private Vector2 _tileLength;

    private float _spaceLength;
    private Tile _tilePrefab;
    private Vector2 _middlePoint;

    private List<Sprite> _gridSpriteList;

    public Vector2 GridSize { get => _gridSize; private set => _gridSize = value; }
    public Vector2 TileLength { get => _tileLength; private set => _tileLength = value; }
    public float SpaceLength { get => _spaceLength; private set => _spaceLength = value; }



    /// <summary>
    /// GridConst
    /// </summary>
    /// <param name="gridSize"></param>
    /// <param name="tilePrefab"></param>
    /// <param name="gridSpriteList">GridspriteList yukarýdan aþaðýya ve soldan saða yönlü sýralanmalýdýr(up to down and left to right)</param>
    /// <param name="startPoint"></param>
    public GridCreator(Vector2 gridSize, Tile tilePrefab, Vector2 tileLength, float spaceLength, List<Sprite> gridSpriteList, Vector2 middlePoint)
    {
        GridSize = gridSize;
        _tilePrefab = tilePrefab;
        TileLength = tileLength;
        SpaceLength = spaceLength;
        _gridSpriteList = gridSpriteList;
        _middlePoint = middlePoint;
    }

    public Dictionary<Vector2, Tile> CreateGrid()
    {
        Vector2 startPoint = GetGridStartPoint();
        Dictionary<Vector2,Tile> tileGrid = new Dictionary<Vector2,Tile>();

        for (int y = 0; y < GridSize.y; y++)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                Vector2 currentLocation = new Vector2(x, y);
                Tile currentTile = _tilePrefab.CreateGameObjectandPlaceIt(startPoint +
                    new Vector2((1 / 2 * TileLength.x) + (x * TileLength.x) + (x * SpaceLength),
                                (1 / 2 * TileLength.y) + (y * TileLength.y) + (y * SpaceLength)));
                currentTile.SetTile(currentLocation, SetTileSprite(currentLocation));
                tileGrid.Add(currentTile._location, currentTile);
            }
        }


        return tileGrid;
    }
    private Vector2 GetGridStartPoint()
    {
        Vector2 startPoint;
        float startingX = _middlePoint.x - ((((GridSize.x - 1) / 2) * TileLength.x) + ((GridSize.x - 1) / 2) * SpaceLength);
        float startingY = _middlePoint.y - ((((GridSize.y - 1) / 2) * TileLength.y) + ((GridSize.y - 1) / 2) * SpaceLength);
        startPoint = new Vector2(startingX, startingY);
        return startPoint;
    }

    public Sprite SetTileSprite(Vector2 location)
    {
        if (location.x == 0 && location.y == 0)
            return _gridSpriteList[6];
        else if (location.x == GridSize.x - 1 && location.y == 0)
            return _gridSpriteList[8];
        else if (location.x == 0 && location.y == GridSize.y - 1)
            return _gridSpriteList[0];
        else if (location.x == GridSize.x - 1 && location.y == GridSize.y - 1)
            return _gridSpriteList[2];
        else if (location.x == 0)
            return _gridSpriteList[3];
        else if (location.x == GridSize.x -1)
            return _gridSpriteList[5];
        else if (location.y == 0)
            return _gridSpriteList[7];
        else if (location.y == GridSize.y - 1)
            return _gridSpriteList[1];
        else
            return _gridSpriteList[4];
    }
    
}
