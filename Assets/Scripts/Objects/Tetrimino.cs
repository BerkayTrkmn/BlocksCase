using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino : MonoBehaviour
{
    [SerializeField]private int id;
    [HideInInspector]public TetriminoPart draggedPart;
    private TetriminoPart[] tetriminoParts;
    public int Id { get => id; private set => id = value; }

    private void Awake()
    {
        tetriminoParts = transform.GetComponentsInChildren<TetriminoPart>();
    }
    public void DropTetriminoToSelectedTile(Tile tile)
    {
        Vector3 direction = tile.transform.position - draggedPart.partCollider.transform.position;
        TetriminoMovesToPosition(transform.position + direction, 0.5f);
        Debug.Log("TileFound " + tile._location + " " + direction);
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
           if(!part.IsTetriminoPartInsideOfLevel(tilelocation, levelSize,draggedPart)) return false;
        }
        return true;
    }
    
}
