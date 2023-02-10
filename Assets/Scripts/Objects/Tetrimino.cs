using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnTetriminoDropped();
public class Tetrimino : MonoBehaviour
{
    [SerializeField] private int id;

    public TetriminoPart draggedPart;
    private TetriminoPart[] tetriminoParts;
    public int Id { get => id; private set => id = value; }

    public static event OnTetriminoDropped onTetriminoInserted;

    public bool isPlaced = false;

    private void Awake()
    {
        tetriminoParts = transform.GetComponentsInChildren<TetriminoPart>();
    }
    public void DropTetriminoToSelectedTile(Tile tile)
    {
        Vector3 direction = tile.transform.position - draggedPart.partCollider.transform.position;
        TetriminoMovesToPosition(transform.position + direction, 0.5f);
        Debug.Log("TileFound " + tile._location + " " + direction);
        InsertTetriminoStateInsideGrid(tile);
        isPlaced = true;
        onTetriminoInserted?.Invoke();
    }
    public void TetriminoMovesToPosition(Vector3 position, float time)
    {
        transform.DOMove(position, time);
    }
    public bool CheckTetriminoInsideOfLevel(Tile placedTile)
    {
        Vector2 tilelocation = placedTile._location;
        Vector2 levelSize = LevelCreator.instance.gridCreator.GridSize;
        foreach (TetriminoPart part in tetriminoParts)
        {
            if (!part.IsTetriminoPartInsideOfLevel(tilelocation, levelSize, draggedPart)) return false;
        }
        return true;
    }
    public void InsertTetriminoStateInsideGrid(Tile placedTile)
    {
        Dictionary<Vector2, Tile> _tileGrid = LevelCreator.instance.tileGrid;
        foreach (TetriminoPart part in tetriminoParts)
        {
            Tile currentTile = _tileGrid[part.ConvertTetriminoToTileLocation(placedTile._location, draggedPart.tetriminoPartLocation)];
            part.tetrominoTileLocation = currentTile._location;
            for (int i = 0; i < part.tetriminoCovarage.Count; i++)
            {
                currentTile.TilePartsOccupancy[part.tetriminoCovarage[i]] = true;
            }
        }
    }
    public void RemoveTetriminoStateInsideGrid()
    {
        Dictionary<Vector2, Tile> _tileGrid = LevelCreator.instance.tileGrid;
        foreach (TetriminoPart part in tetriminoParts)
        {
            Tile currentTile = _tileGrid[part.tetrominoTileLocation];
            part.tetrominoTileLocation = currentTile._location;
            for (int i = 0; i < part.tetriminoCovarage.Count; i++)
            {
                currentTile.TilePartsOccupancy[part.tetriminoCovarage[i]] = false;
            }
        }
    }
}
