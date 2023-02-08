using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloScripts;

public class GridCreator 
{
    private Vector2 _gridSize;
    private Tile _tilePrefab;
    private List<Sprite> _gridSpriteList;

    /// <summary>
    /// GridConst
    /// </summary>
    /// <param name="gridSize"></param>
    /// <param name="tilePrefab"></param>
    /// <param name="gridSpriteList">GridspriteList yukarýdan aþaðýya ve soldan saða yönlü sýralanmalýdýr(up to down and left to right)</param>
    /// <param name="startPoint"></param>
    public GridCreator(Vector2 gridSize, Tile tilePrefab , List<Sprite> gridSpriteList, Vector2 startPoint)
    {
        _gridSize = gridSize;
        _tilePrefab = tilePrefab;
        _gridSpriteList = gridSpriteList;
    }

    public List<Tile> CreateGrid()
    {
        List<Tile> tileGrid = new List<Tile>();

        for (int y = 0; y < _gridSize.y; y++)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {

            }
        }


        return tileGrid;
    }

}
