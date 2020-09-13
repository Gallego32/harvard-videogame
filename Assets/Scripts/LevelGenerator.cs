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

        mapArray[0, 0] = false;
        mapArray[0, 1] = false;
        mapArray[3, 1] = false;
        mapArray[3, 3] = false;

        for (int i = 0; i < positions.Length; i++)
        {
            //positions[i] = new Vector3Int(i % size.x + 10, - i / size.y + 10, 0);
            //tileArray[i] = i % 2 == 0 ? tileA : tileB;
        }
        int k = 0;
        for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++, k++)
            {
                positions[k] = new Vector3Int(j + offset.x, - i + offset.y, 0);
                tileArray[k] = mapArray[i, j] ? (Random.value > 0.5 ? tileA : tileB) : null;
            }

        //tileArray[1] = null;

        baseMap.SetTiles(positions, tileArray);

        /*showArray(mapArray);

        Debug.Log("\n" + (mapArray[0] ? 1 : 0));
        Debug.Log("\n" + (mapArray[1] ? 1 : 0));
        Debug.Log("\n" + (mapArray[2] ? 1 : 0));*/
    }

    void initialArray(bool[,] array, Vector2Int size)
    {
        for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                array[i ,j] = true;
    }

    void showArray(bool[] array)
    {
        for (int i = 0; i < array.Length; i++)
            Debug.Log(" " + (array[i] ? 1 : 0).ToString());
        
    }

}
