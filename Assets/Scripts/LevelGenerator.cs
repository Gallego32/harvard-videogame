﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    public Vector2Int size;
    public Vector2Int offset;

    public int levelStyle;
    public List<TileBase> topperTiles;
    public List<TileBase> fillTiles;

    public List<TileBase> fillAlternative;
    public List<TileBase> backgroundToppers;

    public List<TileBase> leftCorner;
    public List<TileBase> rightCorner;

    public List<TileBase> leftSide;
    public List<TileBase> rightSide;

    public List<TileBase> downSide;

    public List<GameObject> enemies;
    public GameObject props;

    private Tilemap baseMap, foreground, background;

    private bool[,] mapArray;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(props.GetComponent<Renderer>().bounds.size.y);
        // Reference each part of the TileMap
        baseMap = GameObject.Find("Tilemap_Base").GetComponent<Tilemap>();
        foreground = GameObject.Find("Foreground").GetComponent<Tilemap>();
        background = GameObject.Find("Background").GetComponent<Tilemap>();

        // Create positions for Ground and BackGround
        Vector3Int[] positions = new Vector3Int[size.x * size.y];
        Vector3Int[] backPositions = new Vector3Int[size.x * size.y];
        
        // Create TileBae arrays for Ground and BackGround
        TileBase[] tileArray = new TileBase[positions.Length];
        TileBase[] backTileArray = new TileBase[positions.Length];

        // Initialize array
        mapArray = new bool[size.x, size.y];
        initialArray(mapArray, size);

        int k = 0;
        for (int i = 0; i < size.x; i++)
        {   for (int j = 0; j < size.y; j++, k++)
            {
                // Setting tiles positions
                positions[k] = new Vector3Int(i + offset.x, - j + offset.y, 0);
                backPositions[k] = new Vector3Int(i + offset.x, - j + offset.y + 1, 0);

                TileBase tile;
                TileBase background;

                bool foundTop = false;

                if (isTop(i, j) && !foundTop)
                {
                    if (Random.value > 0.95)
                       generateObject(enemies[Random.Range(0, enemies.Count)], (i + offset.x) * 0.159f, 1, GameObject.Find("EnemiesParent"));

                    float propWidth = props.GetComponent<Renderer>().bounds.size.x;
                    float propHeight = props.GetComponent<Renderer>().bounds.size.y;

                    if (hasEmptySpace(i, j, (int)Mathf.Ceil(propWidth / 0.159f)))
                        generateObject(props, (i + offset.x) * 0.159f + propWidth / 2, (offset.y - j + 1) * 0.159f + propHeight / 2, GameObject.Find("PropsParent"));

                    if (isLeftSide(i, j))
                        tile = leftCorner[levelStyle];
                    else
                    {
                        if (isRightSide(i, j))
                            tile = rightCorner[levelStyle];
                        else
                            // Setting special tiles for top tiles
                            tile = topperTiles[levelStyle];
                    }
                    
                    background = Random.value > 0.6 ? backgroundToppers[Random.Range(0,3) + levelStyle * 3] : null;

                    // Setting foundTop to true to avoid checking for top when we have found it in a column
                    // Performance decision
                    foundTop = true;

                } else
                {   
                    if (isLeftSide(i, j))
                        tile = leftSide[levelStyle];
                    else
                    {
                        if (isRightSide(i, j))
                          tile = rightSide[levelStyle];
                        else if (isDownSide(i, j))
                            tile = downSide[levelStyle];
                        else
                            // Setting normal tiles and no background tiles
                            tile = Random.value > 0.03 ? fillTiles[levelStyle] : fillAlternative[Random.Range(0,2) + levelStyle * 2];
                    }
                    background = null;
                }
        
                // Setting tileArrays
                tileArray[k] = mapArray[i, j] ? tile : null;
                backTileArray[k] = background;
            }
            /*
            if (Random.value > 0.95)
                offset.y++;
            */
        }

        // Setting tiles into the TileMap
        baseMap.SetTiles(positions, tileArray);
        background.SetTiles(backPositions, backTileArray);

        //generateEnemies(enemy, (int)Mathf.Round(transform.position.x) + offset.x / 16 + 2, 1);
        //generateEnemies(enemy, offset.x * 0.159f, 1);
    }

    // Initialize Ground Tiles array
    // We also perform the logic of the terrain
    void initialArray(bool[,] array, Vector2Int size)
    {
        // Gap control variables
        bool gap = false;
        int gapWidth = 0;
        float gapProbability = Random.Range(0.85f, 0.95f);

        // Terrain cut variables
        int topCut = Random.Range(0, size.y / 4);
        int bottomCut = Random.Range(size.y - size.y / 2, size.y - 1);

        for (int i = 0; i < size.x; i++)
        {
            if (gap)
            {    
                gapWidth--;
                gap = gapWidth == 0 ? false : true; 
            }
            
            for (int j = 0; j < size.y; j++)
            {
                if (gap || j <= topCut || j >= bottomCut)
                     array[i, j] = false;
                else
                     array[i, j] = true;
                //array[i, j] = gap ? false : true;
            } 
            
            // Gap behaviour
            if (!gap)
                gap = Random.value > gapProbability ? true : false;

            if (gap && gapWidth == 0)
                gapWidth = Random.Range(2, 4);


            // TopCut change
            if (Random.value > 0.7)
                topCut = Mathf.Clamp(topCut + Random.Range(-3, 3), 0, size.y / 3);

            // BottomCut change
            if (Random.value > 0.7)
                bottomCut = Mathf.Clamp(bottomCut + Random.Range(-3, 3), size.y - size.y / 3, size.y - 1);
        }      
    }

    // Checking if a tile is the first one; the topper
    private bool isTop(int x, int y)
    {
        if (!mapArray[x, y])
            return false;

        bool isTop = true;
        int j = y - 1;
        for (; j > 0 && !mapArray[x, j]; j--);

        return j == 0 ? true : false;
    }

    private bool isLeftSide(int x, int y)
    {
        if (x == 0)
            return true;

        return !mapArray[Mathf.Max(x - 1, 0), y];
    }

    private bool isRightSide(int x, int y)
    {
        if (x == size.x - 1)
            return true;

        return !mapArray[Mathf.Min(x + 1, size.x - 1), y];
    }

    private bool isDownSide(int x, int y)
    {
        if (y == size.y)
            return true;

        return !mapArray[x, Mathf.Min(y + 1, size.y)];
    }

    // Spawn enemies and objects and make them child of a parent object which will help us on debugging
    private void generateObject(GameObject prefab, float x, float y, GameObject parent)
    {
        var myPrefab = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        myPrefab.transform.parent = parent.transform;
    }

    private bool hasEmptySpace(int x, int y, int width)
    {
        int j = 0;
        for (int i = x; i < size.x && j < width && mapArray[i, y] && (y == 0 || !mapArray[i, y - 1]); i++, j++);
        
        return j == width ? true : false;
    }

    // Debug function
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            Start();
    }
}
