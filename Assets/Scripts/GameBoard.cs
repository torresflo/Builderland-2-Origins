using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour
{
    public GameObject tileExitPrefab;
    public GameObject tilePrefab;
    public GameObject tilePrefabMovable;
    public GameObject tileStairRightPrefabMovable;
    public GameObject tilePhysicPrefab;
    public GameObject tileStairRightPrefab;
    public GameObject tileStairLeftPrefab;
    public GameObject tilePrefabMovableGravity;
    public GameObject tileStairRightPrefabMovableGravity;

    protected static UnityEngine.Object[] tiles = null;

    protected int columns;
    protected int rows;
    protected int[][] board;
    protected List<GameObject> boardTiles;
    protected Transform boardHodler = null;
    protected GameObject background = null;

    protected void parser(string str, int[][] array)
    {
        string[] lines = str.Split('\n');

        for (int i = 0; i < rows; i++)
        {
            string[] values = lines[i].Split(',');

            for (int j = 0; j < columns; j++)
            {
                //Debug.Log("coucou " + i + ", " + j + ", " + Int32.Parse(values[j]));
                array[j][i] = Int32.Parse(values[j]);
            }
        }
    }

    protected void Initialize()
    {
        if (boardTiles == null)
            boardTiles = new List<GameObject>();
        else
        {
            foreach (GameObject g in boardTiles)
                Destroy(g);

            boardTiles.Clear();
        }

        if (boardHodler == null)
            boardHodler = new GameObject("Board").transform;
        if (background == null)
            background = GameObject.FindGameObjectWithTag("Background");
        
        board = null;
    }

    public void InitBoard(Level level)
    {
        Initialize();

        columns = level.columns;
        rows = level.rows;

        board = new int[columns][];
        int[][] physical_board = new int[columns][];

        for (int i = 0; i < columns; i++)
        {
            board[i] = new int[rows];
            physical_board[i] = new int[rows];
        }

        parser(level.getTiles(), board);
        parser(level.getPhysics(), physical_board);

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (board[i][j] == 0)
                    continue;

                //GameObject toInstantiate = tiles[board[i][j]];
                /*if (physical_board[i][j] == 0)
                {
                    tilePrefab.GetComponent<SpriteRenderer>().sprite = (Sprite)tiles[board[i][j]];
                    GameObject instance = Instantiate(tilePrefab, new Vector3(i - 7.5f, 7.5f - j, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHodler);
                }
                else
                {
                    tilePrefabPhysic.GetComponent<SpriteRenderer>().sprite = (Sprite)tiles[board[i][j]];
                    GameObject instance = Instantiate(tilePrefabPhysic, new Vector3(i - 7.5f, 7.5f - j, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHodler);
                }*/

                GameObject prefab = null;

                if (physical_board[i][j] == 1)
                {
                    prefab = tilePhysicPrefab;
                }
                else if (physical_board[i][j] == 2)
                {
                    prefab = tileStairRightPrefab;
                }
                else if (physical_board[i][j] == 3)
                {
                    prefab = tileStairLeftPrefab;
                }
                else if (physical_board[i][j] == 4)
                {
                    prefab = tileExitPrefab;
                }
                else if (physical_board[i][j] == 99)
                {
                    prefab = tilePrefabMovable;
                }
                else if (physical_board[i][j] == 100)
                {
                    prefab = tileStairRightPrefabMovable;
                }
                else if (physical_board[i][j] == 101)
                {
                    prefab = tilePrefabMovableGravity;
                }
                else if (physical_board[i][j] == 102)
                {
                    prefab = tileStairRightPrefabMovableGravity;
                }
                else
                {
                    prefab = tilePrefab;
                }

                prefab.GetComponent<SpriteRenderer>().sprite = (Sprite)tiles[board[i][j]];
                GameObject instance = Instantiate(prefab, new Vector3(i - 7.5f, 7.5f - j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHodler);
                boardTiles.Add(instance);

                /*if (physical_board[i][j] == 1)
                {
                    BoxCollider2D collider = instance.AddComponent<BoxCollider2D>();
                    collider.size = new Vector2(1, 1);
                }*/
            }
        }
        
        background.GetComponent<BackgroundScript>().SetSky(level.sky);

    }

    public static void LoadSprites()
    {
        if (tiles == null)
            tiles = Resources.LoadAll("Textures/tileset");
    }

    
}
