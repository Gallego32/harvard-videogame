using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    // Size and position (offset) of the level
    public Vector2Int size;
    public Vector2Int offset;

    // Debug option
    public bool spawnEnemies;

    Vector3Int[] positions;

    // We'll use this array for removing tiles when we reset or change level
    TileBase[] nullArray;

    // One of out level styles will be selected from tileSet Array
    private int levelStyle;

    public GameObject nextLevelPortal;

    private Tilemap baseMap, foreground, background;

    private bool[,] mapArray;

    public TileSet[] tileSet;

    // Start is called before the first frame update
    void Start()
    {
        // Reference each part of the TileMap
        baseMap = GameObject.Find("Tilemap_Base").GetComponent<Tilemap>();
        foreground = GameObject.Find("Foreground").GetComponent<Tilemap>();
        background = GameObject.Find("Background").GetComponent<Tilemap>();

        // Increment Level size
        size.x += LoadGame.level * 20;
        // Randomize y size
        size.y = Random.Range(15, 45);

        // Create positions for Ground and BackGround
        positions = new Vector3Int[size.x * size.y];
        Vector3Int[] backPositions = new Vector3Int[size.x * size.y];
        
        // Create TileBae arrays for Ground and BackGround
        TileBase[] tileArray = new TileBase[positions.Length];
        TileBase[] backTileArray = new TileBase[positions.Length];
        nullArray = new TileBase[positions.Length];

        // Initialize array
        mapArray = new bool[size.x, size.y];
        initialArray(mapArray, size);

        // Initialize our levelStyle variable depending on how much elements there are in tileSet array
        levelStyle = Random.Range(0, tileSet.Length);

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

                // Check if we are on the top
                if (isTop(i, j) && !foundTop)
                {
                    // Generate ENEMIES
                    if (Random.value > 0.96 && i > 25 && spawnEnemies)
                       generateObject(tileSet[levelStyle].enemies[Random.Range(0, tileSet[levelStyle].enemies.Count)], (i + offset.x) * 0.159f, offset.y + 2, GameObject.Find("EnemiesParent"));

                    // Generate PROPS
                    if (Random.value > 0.85)
                    {
                        //GameObject prop = props[levelStyle][Random.Range(0, props[levelStyle].Count)];
                        GameObject prop = tileSet[levelStyle].props[Random.Range(0, tileSet[levelStyle].props.Count)];

                        float propWidth = prop.GetComponent<Renderer>().bounds.size.x;
                        float propHeight = prop.GetComponent<Renderer>().bounds.size.y;

                        if (hasEmptySpace(i, j, (int)Mathf.Ceil(propWidth / 0.159f)))
                            generateObject(prop, (i + offset.x) * 0.159f + propWidth / 2, (offset.y - j + 1) * 0.159f + propHeight / 2, GameObject.Find("PropsParent"));
                    }

                    // Generate ITEMS
                    if (Random.value > 0.95 && i > 25)
                    {
                        //GameObject item = items[Random.Range(0, items.Count)];
                        GameObject item = tileSet[levelStyle].items[Random.Range(0, tileSet[levelStyle].items.Count)];

                        float propWidth = item.GetComponent<Renderer>().bounds.size.x;
                        float propHeight = item.GetComponent<Renderer>().bounds.size.y;

                        generateObject(item, (i + offset.x) * 0.159f + propWidth, (offset.y - j + 2) * 0.159f + propHeight, GameObject.Find("PropsParent"));
                    }

                    if (isLeftSide(i, j))
                        tile = tileSet[levelStyle].leftCorner;
                    else
                    {
                        if (isRightSide(i, j))
                            tile = tileSet[levelStyle].rightCorner;
                        else
                            // Setting special tiles for top tiles
                            tile = tileSet[levelStyle].topperTiles;
                    }
                    
                    background = Random.value > 0.6 ? tileSet[levelStyle].backgroundToppers[Random.Range(0, tileSet[levelStyle].backgroundToppers.Count)] : null;

                    // Setting foundTop to true to avoid checking for top when we have found it in a column
                    // Performance decision
                    foundTop = true;

                // If we are not on the top
                } else
                {   
                    if (isLeftSide(i, j))
                        tile =  tileSet[levelStyle].leftSide;
                    else
                    {
                        if (isRightSide(i, j))
                          tile =  tileSet[levelStyle].rightSide;
                        else if (isDownSide(i, j))
                            tile =  tileSet[levelStyle].downSide;
                        else
                            // Setting normal tiles and no background tiles
                            tile = Random.value > 0.03 ?  tileSet[levelStyle].fillTiles : tileSet[levelStyle].alternativeFill[Random.Range(0, tileSet[levelStyle].alternativeFill.Count)];
                    }
                    background = null;
                }
        
                // Setting tileArrays
                tileArray[k] = mapArray[i, j] ? tile : null;
                backTileArray[k] = background;
                nullArray[k] = null;
            }
        }

        // End game generation
        generateObject(nextLevelPortal, (offset.x + size.x + 5) * 0.159f, offset.y * 0.159f, GameObject.Find("PropsParent"));

        // Setting tiles into the TileMap
        baseMap.SetTiles(positions, tileArray);
        background.SetTiles(backPositions, backTileArray);
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
            if (!gap && i > 7)
                gap = Random.value > gapProbability ? true : false;

            if (gap && gapWidth == 0 && i > 7)
                gapWidth = Random.Range(2, 4);


            // TopCut change
            if (Random.value > 0.7 && i > 7)
                topCut = Mathf.Clamp(topCut + Random.Range(-3, 3), 0, size.y / 3);

            // BottomCut change
            if (Random.value > 0.7 && i > 7)
                bottomCut = Mathf.Clamp(bottomCut + Random.Range(-3, 3), size.y - size.y / 3, size.y - 1);
        }      
    }

    // Checking if a tile is the first one, the topper
    private bool isTop(int x, int y)
    {
        if (!mapArray[x, y])
            return false;

        bool isTop = true;
        int j = y - 1;
        for (; j > 0 && !mapArray[x, j]; j--);

        return j == 0 ? true : false;
    }

    // Check if it's left, right top or down side
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

    // Will determine if we have space to spawn a determined object with a determined width
    // We check if we are on the top and if we don't find any kind of hole
    private bool hasEmptySpace(int x, int y, int width)
    {
        int j = 0;
        for (int i = x; i < size.x && j < width && mapArray[i, y] && (y == 0 || !mapArray[i, y - 1]); i++, j++);
        
        return j == width ? true : false;
    }

    /* Debug function
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ClearChildren(GameObject.Find("PropsParent"));
            ClearChildren(GameObject.Find("EnemiesParent"));
            baseMap.SetTiles(positions, nullArray);
            Start();
        }
    }*/

    public void NextLevel()
    {   
        // Clear level
        // Delete every enemy and prop
        ClearChildren(GameObject.Find("PropsParent"));
        ClearChildren(GameObject.Find("EnemiesParent"));

        // Clear tiles from last level
        baseMap.SetTiles(positions, nullArray);

        // Increment level
        LoadGame.level++;

        // Generate new level
        Start();
    }

    // Function used to destroy children objects from EnemiesParent and PropsParent
    // This way we clear the level
    private void ClearChildren(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];

        int i = 0;
        foreach(Transform child in parent.transform)
        {
            children[i] = child.gameObject;
            i++;
        }

        foreach (GameObject child in children)
        {
            Destroy(child.gameObject);
        }

    }
}
