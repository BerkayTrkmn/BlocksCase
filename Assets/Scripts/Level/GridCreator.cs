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

    /// <summary>
    /// GridConst
    /// </summary>
    /// <param name="gridSize"></param>
    /// <param name="tilePrefab"></param>
    /// <param name="gridSpriteList">GridspriteList yukarýdan aþaðýya ve soldan saða yönlü sýralanmalýdýr(up to down and left to right)</param>
    /// <param name="startPoint"></param>
    public GridCreator(Vector2 gridSize, Tile tilePrefab, Vector2 tileLength, float spaceLength, List<Sprite> gridSpriteList, Vector2 middlePoint)
    {
        _gridSize = gridSize;
        _tilePrefab = tilePrefab;
        _tileLength = tileLength;
        _spaceLength = spaceLength;
        _gridSpriteList = gridSpriteList;
        _middlePoint = middlePoint;
    }

    public List<Tile> CreateGrid()
    {
        Vector2 startPoint = GetGridStartPoint();
        List<Tile> tileGrid = new List<Tile>();

        for (int y = 0; y < _gridSize.y; y++)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                Vector2 currentLocation = new Vector2(x, y);
                Tile currentTile = _tilePrefab.CreateGameObjectandPlaceIt(startPoint +
                    new Vector2((1 / 2 * _tileLength.x) + (x * _tileLength.x) + (x * _spaceLength),
                                (1 / 2 * _tileLength.y) + (y * _tileLength.y) + (y * _spaceLength)));
                currentTile.SetTile(currentLocation, SetTileSprite(currentLocation));
            }
        }


        return tileGrid;
    }
    private Vector2 GetGridStartPoint()
    {
        Vector2 startPoint;
        float startingX = _middlePoint.x - ((((_gridSize.x - 1) / 2) * _tileLength.x) + ((_gridSize.x - 1) / 2) * _spaceLength);
        float startingY = _middlePoint.y - ((((_gridSize.y - 1) / 2) * _tileLength.y) + ((_gridSize.y - 1) / 2) * _spaceLength);
        startPoint = new Vector2(startingX, startingY);
        return startPoint;
    }

    public Sprite SetTileSprite(Vector2 location)
    {
        if (location.x == 0 && location.y == 0)
            return _gridSpriteList[6];
        else if (location.x == _gridSize.x - 1 && location.y == 0)
            return _gridSpriteList[8];
        else if (location.x == 0 && location.y == _gridSize.y - 1)
            return _gridSpriteList[0];
        else if (location.x == _gridSize.x - 1 && location.y == _gridSize.y - 1)
            return _gridSpriteList[2];
        else if (location.x == 0)
            return _gridSpriteList[3];
        else if (location.x == _gridSize.x -1)
            return _gridSpriteList[5];
        else if (location.y == 0)
            return _gridSpriteList[7];
        else if (location.y == _gridSize.y - 1)
            return _gridSpriteList[1];
        else
            return _gridSpriteList[4];
    }
    //public void CreateGrid(string word)
    //{
    //    float length = word.Length;

    //    float startingX = -(((length / 2) * keyLength) + ((length - 1) / 2) * spaceLength);
    //    keyList = new List<Key>();
    //    for (float i = 0; i < length; i++)
    //    {
    //        GameObject go = Instantiate(emptyKey.gameObject, transform);
    //        Vector3 startingScale = go.transform.localScale;
    //        go.transform.localScale = Vector3.zero;
    //        go.transform.DOScale(startingScale, 0.5f);
    //        //(transform.position.ChangeX(startingX + ((i * keyLength / 2) + (i * spaceLength))));
    //        go.transform.localPosition = new Vector3(startingX + ((1 / 2 * keyLength) + (i * keyLength) + (i * spaceLength)), 0, 0);

    //        keyList.Add(go.GetComponent<Key>());
    //    }
    //}
}
