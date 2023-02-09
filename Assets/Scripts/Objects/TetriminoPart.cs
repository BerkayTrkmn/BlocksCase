using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetriminoPart : MonoBehaviour
{
    public Vector2 tetriminoPartLocation;
    public List<TilePartType> tetriminoCovarage;
    public Collider2D partCollider;

    private void Awake()
    {
        partCollider = GetComponent<Collider2D>();
    }
    public bool IsTetriminoPartInsideOfLevel(Vector2 placedTetriminoLocation, Vector2 _levelsize,TetriminoPart draggedPart)
    {
        Vector2 currentLocation =  (tetriminoPartLocation- draggedPart.tetriminoPartLocation) + placedTetriminoLocation;
        if (currentLocation.x >= 0 && currentLocation.x < _levelsize.x
         && currentLocation.y >= 0 && currentLocation.y < _levelsize.y)
            return true;
        else return false;

    }
}
