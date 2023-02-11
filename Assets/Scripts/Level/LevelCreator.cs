using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ONEMLI: Bu Projede blocklar tetrimino olarak adlandýrýlmaktadýr.
/// 
/// Bu scriptte level oluþturma  ile ilgili tüm olaylar bulunmaktadýr
/// 1. Grid oluþturma : GridCreator classýnda grid oluþturma ile ilgili bütün olaylar bulunup LevelCreator classýnda Grid oluþturulmaktadýr
/// 2.TetriminoCreator: Leveldaki sürüklenecek tetriminolarý oluþturma ile ilgili olaylarý bulunan class.Levelcrator tarafýndan kullanýulmaktadýr.
/// 3.LevelEndCheck : OnLevelCompleted eventi ile saðlanan baðlantý  LevelendCheck ile her tetrimino gride býrakýldýðýnda kontrol ediliyor ve levelýn bitiþini saðlýyor
/// </summary>
public enum LevelDifficulty
{
    Easy,
    Medium,
    Hard
}

public delegate void LevelStateEvent();
public class LevelCreator : MonoBehaviour
{
    public static LevelCreator instance;
    [HideInInspector] public GridCreator gridCreator;
    [HideInInspector] public TetriminoCreator tetriminoCreator;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private List<Sprite> tileSprites;
    private LevelData data;

    [HideInInspector] public Dictionary<Vector2, Tile> tileGrid;

    public static event LevelStateEvent OnLevelCompleted;

    [SerializeField] private LevelData[] allLevelData;
    [SerializeField] private Tetrimino[] allTetriminoPrefabs;
    /// <summary>
    /// JSON dosyasý yaratmak için oluþturulan levellardan bir tanesinin ismini girin ve butona týklayýn
    /// </summary>
    [Header("JSON CREATION")]
    [Tooltip("JSON dosyasý yaratmak için oluþturulan levellardan bir tanesinin ismini girin ve butona týklayýn Assets klasöründe oluþacaktýr")]
    public string levelName;
    private void Awake()
    {
        if (instance == null)
            instance = this;

    }

    void Start()
    {
        RandomLevelSelect();

        gridCreator = new GridCreator(data.GridSize, tilePrefab, data.Tilelength, data.SpaceLength, tileSprites, data.MiddlePoint);
        tetriminoCreator = new TetriminoCreator(allTetriminoPrefabs, transform.position, data);
        tileGrid = gridCreator.CreateGrid();

        tetriminoCreator.CreateTetriminos();
        CreateJsonLevelFile("Level 1");
    }

    public void RandomLevelSelect()
    {
        data = allLevelData[UnityEngine.Random.Range(0, allLevelData.Length)];
    }
    private void OnEnable()
    {
        Tetrimino.onTetriminoInserted += CheckLevelEnd;
    }

    private void CheckLevelEnd()
    {
        if (CheckTetriminosCorrectPosition())
            OnLevelCompleted?.Invoke();
    }

    //Check is tetrimino zero correct location
    public bool CheckTetriminosCorrectPosition()
    {
        //All tetrimino must be correct location
        foreach (Tetrimino tetrimino in tetriminoCreator.CreatedTetriminos)
        {
            //Search zero tetriminopart
            for (int i = 0; i < tetrimino.TetriminoParts.Length; i++)
            {
                if (tetrimino.TetriminoParts[i].tetriminoPartLocation == Vector2.zero)
                {
                    //Find correct tetrimino answer 
                    for (int t = 0; t < data.LevelAnswer.Length; t++)
                    {
                        if (data.LevelAnswer[t].Id == tetrimino.Id)
                        {
                            if (data.LevelAnswer[t].Location != tetrimino.TetriminoParts[i].tetrominoTileLocation)
                                return false;
                            else
                                break;
                        }

                    }
                    break;
                }

            }
        }
        return true;
    }

    public void CreateJsonLevelFile(string levelName)
    {
      LevelData data = Resources.Load<LevelData>("ScriptableObjects/Levels/" + levelName);
        Debug.Log(data.name);
        JSONOperations.SaveData(data, levelName);
    }

    private void OnDisable()
    {
        Tetrimino.onTetriminoInserted -= CheckLevelEnd;
    }
}
[CustomEditor(typeof(LevelCreator))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelCreator myTarget = (LevelCreator)target;
        if (GUILayout.Button("CreateJsonFile"))
        {
            myTarget.CreateJsonLevelFile(myTarget.levelName);
        }
    }
}


