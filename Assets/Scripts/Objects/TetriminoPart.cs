using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ONEMLI: Bu Projede blocklar tetrimino olarak adlandýrýlmaktadýr.
/// 
/// Tetriminolarýn bir parçasýnýn davranýþýnýn bulunduðu class
/// fonksyonlarý aþaðýdaki gibidir
/// </summary>
public class TetriminoPart : MonoBehaviour
{
    public Vector2 tetriminoPartLocation;
    public Vector2 tetrominoTileLocation = Vector2.one * -1 ;
    public Collider2D partCollider;

    private void Awake()
    {
        partCollider = GetComponent<Collider2D>();
    }
    public bool IsTetriminoPartInsideOfLevel(Vector2 tileLocation, Vector2 _levelsize,TetriminoPart draggedPart)
    {
        Vector2 currentLocation = ConvertTetriminoToTileLocation(tileLocation, draggedPart.tetriminoPartLocation);
        if (currentLocation.x >= 0 && currentLocation.x < _levelsize.x
         && currentLocation.y >= 0 && currentLocation.y < _levelsize.y)
            return true;
        else return false;

    }
    public Vector2 ConvertTetriminoToTileLocation(Vector2 tileLocation, Vector2 draggedTetriminoLocation)
    {
       return (tetriminoPartLocation - draggedTetriminoLocation) + tileLocation;
    }
}
