using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloScripts;
/// <summary>
/// Tetrinimo yapan fonksiyonlarýn bulunduðu class
/// </summary>
public class TetriminoCreator
{
    private Tetrimino[] _tetriminos;
    private Vector2 _middlePoint;
    private LevelData _levelData;
    private List<Tetrimino> createdTetriminos = new List<Tetrimino>();
    public List<Tetrimino> CreatedTetriminos { get => createdTetriminos; private set => createdTetriminos = value; }

    public TetriminoCreator(Tetrimino[] tetriminos, Vector2 middlePoint, LevelData levelData)
    {
        _tetriminos = tetriminos;
        _middlePoint = middlePoint;
        _levelData = levelData;
    }
    public void CreateTetriminos()
    {

        for (int i = 0; i < _levelData.LevelAnswer.Length; i++)
        {
            for (int j = 0; j < _tetriminos.Length; j++)
            {
                if (_tetriminos[j].Id == _levelData.LevelAnswer[i].Id)
                {
                    Tetrimino createdTetrimino = _tetriminos[j].CreateGameObjectandPlaceIt(RandomizeSpawnPoint(1,2));
                    CreatedTetriminos.Add(createdTetrimino);
                }
            }
        }
    }
    private Vector2 RandomizeSpawnPoint(float x, float y)
    {
        return new Vector2(Random.Range(-x, +x) + _middlePoint.x, Random.Range(-y, +y) + _middlePoint.y);
    }


}
