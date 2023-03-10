using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject levelCompletePanel;



    private void OnEnable()
    {
        LevelCreator.OnLevelCompleted += OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {
        levelCompletePanel.SetActive(true);
    }
    private void OnDisable()
    {
        LevelCreator.OnLevelCompleted -= OnLevelCompleted;
    }
}
