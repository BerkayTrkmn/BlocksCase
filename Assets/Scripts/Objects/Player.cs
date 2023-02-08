using HelloScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{

    private TouchManager touchManager;

    private Tetrimino draggedTetrimino;
    private void OnEnable()
    {
        touchManager =GetComponent<TouchManager>();
        touchManager.onTouchBegan += OnTouchBegan;
        touchManager.onTouchMoved += OnTouchMoved;
        touchManager.onTouchEnded += OnTouchEnded;
    }

    private void OnTouchBegan(TouchInput touch)
    {
        RaycastHit2D hit = Physics2D.Raycast(touch.WorldPosition, Vector2.left, 15f);
        if (hit.collider.gameObject.GetComponentInParent<Tetrimino>() != null)
        { draggedTetrimino = hit.collider.gameObject.GetComponentInParent<Tetrimino>();
            draggedTetrimino.draggedCollider = hit.collider;
        }
    }

    private void OnTouchMoved(TouchInput touch)
    {
        if (draggedTetrimino != null)
        {
            draggedTetrimino.transform.position += new Vector3(touch.DeltaWorldPosition.x, touch.DeltaWorldPosition.y, 0f);
        }
    }

    private void OnTouchEnded(TouchInput touch)
    {
        draggedTetrimino.draggedCollider.enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(touch.WorldPosition, Vector2.left, 15f);
        if (hit.collider.gameObject.GetComponentInParent<Tile>() != null)
            Debug.Log("Tile found");

        draggedTetrimino.draggedCollider.enabled = true;
        draggedTetrimino.draggedCollider = null;
    }
    private void OnDisable()
    {
        touchManager.onTouchBegan -= OnTouchBegan;
        touchManager.onTouchMoved -= OnTouchMoved;
        touchManager.onTouchEnded -= OnTouchEnded;
    }

}
