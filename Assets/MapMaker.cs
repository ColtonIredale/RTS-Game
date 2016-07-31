using UnityEngine;
using System.Collections;
public enum TileType
{
    Ground, Water, Sand, Whatever
}
//Used for setting a specific tiletype during the Generate methods
public class MapMaker : MonoBehaviour
{

    public GameObject[] floortype; //Holds all of the floor tiles.
    public TileType[][] tiles; //A jagged array used to create a grid layout of the map. 

    public int mapHeight;
    public int mapLength;

    Lakes[] lakes;

    //All the values of the lakes are created here. Names are pretty self evident.
    RandomGen lakePieces = new RandomGen(1, 2);
    RandomGen lakeXCoord = new RandomGen(2, 10);
    RandomGen lakeZCoord = new RandomGen(2, 10);
    RandomGen lakeLength = new RandomGen(2, 5);
    RandomGen lakeWidth = new RandomGen(2, 5);
    
    // Use this for initialization
    void Start()
    {
        AssignGrid();
        GenerateLakes();
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
    void GenerateLakes()
    {
        lakes = new Lakes[lakePieces.Randomize];
        //The amount of lakes is selected here

        for (int i = 0; i < lakes.Length; i++)
        {
            lakes[i] = new Lakes();
            lakes[i].SetUpLakes(lakeXCoord.Randomize, lakeZCoord.Randomize, lakeLength.Randomize, lakeWidth.Randomize);
            //Creates a new lake for each lake. Each lake has its stuff randomized.

            for (int x = 0; x < lakes[i].lakeLength; x++)
            {
                int xCoord = lakes[i].xPos + x;

                for (int z = 0; z < lakes[i].lakeHeight; z++)
                {
                    int zCoord = lakes[i].zPos + z;
                    tiles[xCoord][zCoord] = TileType.Water;
                    //Sets the tile number to be assigned to become a water tile during CreateGrid();
                }
            }
        }

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

                //Checks what value each tile is set up with. This is the basic structure that any future tiletype/object that we will place on the map will follow. 
                if (tiles[i][j] == TileType.Water)
                {
                    GameObject mapTile = Instantiate(floortype[1], position, Quaternion.AngleAxis(90, Vector3.left)) as GameObject;
                    mapTile.transform.parent = this.transform;
                    mapTile.name = "maptile" + i + j;
                }
                //Defaults to the generic floor type. 
                else
                {
                    GameObject mapTile = Instantiate(floortype[0], position, Quaternion.AngleAxis(90, Vector3.left)) as GameObject;
                    mapTile.transform.parent = this.transform;
                    mapTile.name = "maptile" + i + j;
                }
            }            
        }
        //Creates a box collider spanning the whole map. Makes the floor and walls essentially.
        BoxCollider floorcollider = this.gameObject.AddComponent<BoxCollider>();
        floorcollider.size = new Vector3(mapLength * 2, 1, mapHeight * 2);
        floorcollider.center = new Vector3(mapLength - 1, -0.5f, mapHeight - 1);

        BoxCollider NorthWallcollider = this.gameObject.AddComponent<BoxCollider>();
        NorthWallcollider.size = new Vector3(mapLength * 2, 2, 1);
        NorthWallcollider.center = new Vector3(mapLength - 1, 0, (mapHeight * 2) - 0.5f);

        BoxCollider SouthWallCollider = this.gameObject.AddComponent<BoxCollider>();
        SouthWallCollider.size = new Vector3(mapLength * 2, 2, 1);
        SouthWallCollider.center = new Vector3(mapLength - 1 , 0, -1.5f);

        BoxCollider EastWallCollider = this.gameObject.AddComponent<BoxCollider>();
        EastWallCollider.size = new Vector3(1, 2, mapHeight * 2);
        EastWallCollider.center = new Vector3(-1.5f, 0, mapHeight - 1);

        BoxCollider WestWallCollider = this.gameObject.AddComponent<BoxCollider>();
        WestWallCollider.size = new Vector3(1, 2, mapHeight * 2);
        WestWallCollider.center = new Vector3((mapLength * 2) - 0.5f, 0, mapHeight - 1);

    }
}
  