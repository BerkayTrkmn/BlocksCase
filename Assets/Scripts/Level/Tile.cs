using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ONEMLI: Bu Projede blocklar tetrimino olarak adland�r�lmaktad�r.
/// 
/// Grid i�in kullan�lan tile �zellikleri
/// </summary>

[System.Serializable]
public class Tile : MonoBehaviour
{
    public Vector2 _location;
    private SpriteRenderer spriteRenderer;


    public void SetTile(Vector2 location,Sprite _tileSprite)
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        _location = location;
        spriteRenderer.sprite = _tileSprite;
    }
   
   
   
}


