using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TilePartType
{
    Up,
    Left,
    Down,
    Right
}
[System.Serializable]
public struct TilePart
{
    public TilePartType Type;
    public bool IsOccupied;
}
[System.Serializable]
public class Tile : MonoBehaviour
{
    private Vector2 _location;
    [SerializeField]private Dictionary<TilePartType, bool> isTilePartsOccupied;

    public Dictionary<TilePartType, bool> IsTilePartsOccupied { get => isTilePartsOccupied; set => isTilePartsOccupied = value; }

    private void SetTile(Vector2 location)
    {
        _location = location;
        SetAllPartOfTile();
    }
    private void SetAllPartOfTile()
    {
        string[] PieceTypeNames = System.Enum.GetNames(typeof(TilePartType));
        for (int i = 0; i < PieceTypeNames.Length; i++)
        {
            IsTilePartsOccupied.Add((TilePartType)i, false);
        }
    }
    public bool CheckTileIsOccupied()
    {
        string[] PieceTypeNames = System.Enum.GetNames(typeof(TilePartType));
        for (int i = 0; i < PieceTypeNames.Length; i++)
        {
            if (!IsTilePartsOccupied[(TilePartType)i]) return false;
        }
        return true;
    }
    public void SetTilePart(TilePartType type, bool isOccupied)
    {
        isTilePartsOccupied[type] = isOccupied;
    }
}


