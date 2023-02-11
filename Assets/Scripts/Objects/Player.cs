using HelloScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// ONEMLI: Bu Projede blocklar tetrimino olarak adlandýrýlmaktadýr.
/// 
/// Bu scriptte playerýn yaptýðý hareketler(Drag, Touch vs) ile ilgili tüm olaylar bulunmaktadýr
/// 1. TouchManager: Playerýn týklamalarýný touch a çeviren benim scriptim.
/// 2.OnTouchBegan Actionu: Oyuncu týklamaya baþlayýnca týklanan yerde tetrimino var mý ve hangi tetrimino olduðunu belirleyen fonksiyon
/// 3.OnTouchMoved:Tetriminoyu hareket ettiren(Drag) fonksiyon Update fonksiyonu üzerinden çalýþýr.
/// 4.OnTouch Ended:Tetrimino nereye býrakýldýðýný belirleyip ona göre bir cevap dönen fonsiyon
/// </summary>
public class Player : MonoBehaviour
{

    private TouchManager touchManager;

    [SerializeField] private Tetrimino draggedTetrimino;
    private Vector3 draggedTetriminoStartPosition;
    int tileLayer;
    int tetriminoLayer;

    private void OnEnable()
    {
        tileLayer = LayerMask.GetMask("Tile");
        tetriminoLayer = LayerMask.GetMask("Tetrimino");
        touchManager = GetComponent<TouchManager>();
        touchManager.onTouchBegan += OnTouchBegan;
        touchManager.onTouchMoved += OnTouchMoved;
        touchManager.onTouchEnded += OnTouchEnded;
    }

    private void OnTouchBegan(TouchInput touch)
    {
        RaycastHit2D hit = Physics2D.Raycast(touch.WorldPosition, Vector2.zero, 15f, tetriminoLayer);
        if (hit.collider != null && hit.collider.gameObject.GetComponentInParent<Tetrimino>() != null)
        {
            Debug.Log(hit.collider.name);
            draggedTetrimino = hit.collider.gameObject.GetComponentInParent<Tetrimino>();
            draggedTetrimino.draggedPart = hit.collider.GetComponent<TetriminoPart>();
            draggedTetriminoStartPosition = draggedTetrimino.transform.position;

        }
    }

    private void OnTouchMoved(TouchInput touch)
    {
        if (draggedTetrimino != null)
            draggedTetrimino.transform.position += new Vector3(touch.DeltaWorldPosition.x, touch.DeltaWorldPosition.y, 0f);
    }

    private void OnTouchEnded(TouchInput touch)
    {

        if (draggedTetrimino == null) return;

        RaycastHit2D hit = Physics2D.Raycast(touch.WorldPosition, Vector2.zero, 15f, tileLayer);
        Debug.Log(tileLayer);

        //Check tile if not founded return start point
        if (hit.collider != null && hit.collider.transform.TryGetComponent<Tile>(out Tile _selectedTile))
        {
            //CheckTetrimino location
            if (draggedTetrimino.CheckTetriminoInsideOfLevel(_selectedTile))
                draggedTetrimino.DropTetriminoToSelectedTile(_selectedTile);
            else
                //Return to start position
                draggedTetrimino.TetriminoMovesToPosition(draggedTetriminoStartPosition, 0.2f);
        }
        else
            //Return to start position
            draggedTetrimino.TetriminoMovesToPosition(draggedTetriminoStartPosition, 0.2f);

        draggedTetrimino.draggedPart = null;
        draggedTetrimino = null;
    }


    private void OnDisable()
    {
        touchManager.onTouchBegan -= OnTouchBegan;
        touchManager.onTouchMoved -= OnTouchMoved;
        touchManager.onTouchEnded -= OnTouchEnded;
    }

}
