using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ONEMLI: Bu Projede blocklar tetrimino olarak adlandýrýlmaktadýr.
/// 
/// Tetriminolar grid gibi parçalara bölünmüþtür.
/// Bu scriptte Tetriminolarýn davranýþ özellikleri bulunmaktadýr.
/// Tetriminolarýn playerýn yaptýðý olaylara karþý verdikleri cevaplar bu classtadýr
/// </summary>
public delegate void OnTetriminoDropped();
public class Tetrimino : MonoBehaviour
{
    [SerializeField] private int id;

    public TetriminoPart draggedPart;
    private TetriminoPart[] tetriminoParts;
    public int Id { get => id; private set => id = value; }
    public TetriminoPart[] TetriminoParts { get => tetriminoParts; private set => tetriminoParts = value; }

    public static event OnTetriminoDropped onTetriminoInserted;

    private void Awake()
    {
        TetriminoParts = transform.GetComponentsInChildren<TetriminoPart>();
    }
    public void DropTetriminoToSelectedTile(Tile tile)
    {

        Vector3 direction = tile.transform.position - draggedPart.partCollider.transform.position;
        TetriminoMovesToPosition(transform.position + direction, 0.5f);
        Debug.Log("TileFound " + tile._location + " " + direction);
        InsertTetriminoStateInsideGrid(tile);
        onTetriminoInserted?.Invoke();
    }
    public void TetriminoMovesToPosition(Vector3 position, float time)
    {
        transform.DOMove(position, time);
    }
    //Is Tetrimino placed inside of level?
    public bool CheckTetriminoInsideOfLevel(Tile placedTile)
    {
        Vector2 tilelocation = placedTile._location;
        Vector2 levelSize = LevelCreator.instance.gridCreator.GridSize;
        foreach (TetriminoPart part in TetriminoParts)
        {
            if (!part.IsTetriminoPartInsideOfLevel(tilelocation, levelSize, draggedPart)) return false;
        }
        return true;
    }
    public void InsertTetriminoStateInsideGrid(Tile placedTile)
    {
        Dictionary<Vector2, Tile> _tileGrid = LevelCreator.instance.tileGrid;
        foreach (TetriminoPart part in TetriminoParts)
        {
            Tile currentTile = _tileGrid[part.ConvertTetriminoToTileLocation(placedTile._location, draggedPart.tetriminoPartLocation)];
            part.tetrominoTileLocation = currentTile._location;
        }
    }
}
