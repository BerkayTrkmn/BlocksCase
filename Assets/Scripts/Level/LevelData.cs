using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Custom/Level", order = 0)]
public class LevelData : ScriptableObject
{
    public Vector2 GridSize;
    public Vector2 Tilelength;
    public float SpaceLength;
    public Vector2 MiddlePoint;
    public LevelDifficulty Difficulty;
    public TetriminoAnswer[] LevelAnswer;

}
[Serializable]
public struct TetriminoAnswer
{
    [SerializeField] public int Id;
    [SerializeField] public Vector2 Location;

}