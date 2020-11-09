using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * With this simple data structure we will be able to add new level styles easily
 */

[System.Serializable]
public class TileSet
{
    public string style;

    [Header("Up & Down")]
    public TileBase topperTiles;
    public TileBase downSide;

    [Header("Corners")]
    public TileBase leftCorner;
    public TileBase rightCorner;

    [Header("Left & Right")]
    public TileBase leftSide;
    public TileBase rightSide;

    [Header("Fill")]
    public TileBase fillTiles;
    public List<TileBase> alternativeFill;

    [Header("Props and Items")]
    public List<TileBase> backgroundToppers;
    public List<GameObject> props;
    public List<GameObject> items;

    [Space(10)]
    public List<GameObject> enemies;
}
