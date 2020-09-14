using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    public TileBase tileA;
    public TileBase tileB;
    public Vector2Int size;
    public Vector2Int offset;

    Tilemap baseMap, foreground, background;

    private bool[,] mapArray;

    // Start is called before the first frame update
    void Start()
    {
        baseMap = GameObject.Find("Tilemap_Base").GetComponent<Tilemap>();
        foreground = GameObject.Find("Foreground").GetComponent<Tilemap>();
        background = GameObject.Find("Background").GetComponent<Tilemap>();

        Vector3Int[] positions = new Vector3Int[size.x * size.y];
        TileBase[] tileArray = new TileBase[positions.Length];

        mapArray = new bool[size.x, size.y];
        initialArray(mapArray, size);
        /*
        mapArray[0, 0] = false;
        mapArray[0, 1] = false;
        mapArray[3, 1] = false;
        mapArray[3, 3] = false;
        */
        for (int i = 0; i < positions.Length; i++)
        {
            //positions[i] = new Vector3Int(i % size.x + 10, - i / size.y + 10, 0);
            //tileArray[i] = i % 2 == 0 ? tileA : tileB;
        }
        int k = 0;
        for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++, k++)
            {
                positions[k] = new Vector3Int(i + offset.x, - j + offset.y, 0);
                TileBase tile = isTop(i, j) ? tileA : tileB;
                tileArray[k] = mapArray[i, j] ? tile : null;
            }

        //tileArray[1] = null;

        baseMap.SetTiles(positions, tileArray);

    }

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

    bool isTop(int x, int y)
    {
        if (!mapArray[x, y])
            return false;

        bool isTop = true;
        int j = y - 1;
        for (; j > 0 && !mapArray[x, j]; j--);

        return j == 0 ? true : false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            Start();
    }
}
