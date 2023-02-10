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
public class Tile : MonoBehaviour
{
    public Vector2 _location;
    private SpriteRenderer spriteRenderer;
    [SerializeField]private Dictionary<TilePartType, bool> tilePartsOccupancy;

    public Dictionary<TilePartType, bool> TilePartsOccupancy { get => tilePartsOccupancy; set => tilePartsOccupancy = value; }

    public void SetTile(Vector2 location,Sprite _tileSprite)
    {
        if (tilePartsOccupancy == null) tilePartsOccupancy = new Dictionary<TilePartType, bool>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        _location = location;
        spriteRenderer.sprite = _tileSprite;
        SetAllPartOfTile();
    }
    private void SetAllPartOfTile()
    {
        string[] PieceTypeNames = System.Enum.GetNames(typeof(TilePartType));
        for (int i = 0; i < PieceTypeNames.Length; i++)
        {
            TilePartsOccupancy.Add((TilePartType)i, false);
        }
    }
    public bool CheckTileIsOccupied()
    {
        string[] PieceTypeNames = System.Enum.GetNames(typeof(TilePartType));
        for (int i = 0; i < PieceTypeNames.Length; i++)
        {
            if (!TilePartsOccupancy[(TilePartType)i]) return false;
        }
        return true;
    }
    public void SetTilePart(TilePartType type, bool isOccupied)
    {
        tilePartsOccupancy[type] = isOccupied;
    }
}


