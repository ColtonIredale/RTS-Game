using UnityEngine;
using System.Collections;
public enum TileType
{
    Ground, Water, Sand, Whatever
}
//Used for setting a specific tiletype
public class MapMaker : MonoBehaviour
{
    public GameObject[] floortype;
    public TileType[][] tiles;

    public int mapHeight;
    public int mapLength;

    // Use this for initialization
    void Start()
    {
        AssignGrid();
        CreateGrid();
    }
    void AssignGrid()
    {
        tiles = new TileType[mapLength][];

        for (int i = 0; i < mapHeight; i++)
        {
            tiles[i] = new TileType[mapHeight];

        }
        //Assigns the tiles array to the specified length and width.
    }
    void CreateGrid()
    {

        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                /*
                  This chunk basically sets the position of the map and makes sure that no tiles overlap.
                  2f = Length of each tile. 
                  Subtracting mapLength/mapHeigh= makes sure that the center of the map coincides with unity's center of map for ease of use.                
                */
                float x = (float)i;
                float z = (float)j;
                float xOffset = (x * 2f);
                float zOffset = (z * 2f);
                Vector3 position = new Vector3(xOffset, 0, zOffset);

                GameObject mapTile = Instantiate(floortype[0], position, Quaternion.identity) as GameObject;
                mapTile.transform.parent = this.transform;
                mapTile.name = "maptile" + i + j;

            }
        }
    }
}
  