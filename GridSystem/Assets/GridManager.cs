using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    public GameObject defaultTile;
    public Texture2D Heightmap;
    public float HeightmapScale = 1;
    public int gridX = 1, gridY = 1;
    private int gridXCheck, gridYCheck;
    public float tileSizeX = 1.0f, tileSizeY = 1.0f;
    private float tileSizeXCheck, tileSizeYCheck;
    [Range(0.1f, 20f)]
    public float HeightMapStepSpeed = 1;
     myGrid[,] grid;
	// Use this for initialization
	void Start () {
        InitializeGrid();
	}
	
	// Update is called once per frame
	void Update () {
		if (hasTileSizeChanged())
            resetTilePosition();
        if (hasGridSizeChanged())
        {
            
            InitializeGrid();
            deleteFromGrid();
        }
	}
    bool hasTileSizeChanged()
    {
        if (tileSizeXCheck != tileSizeX || tileSizeYCheck != tileSizeY)
        {
            tileSizeXCheck = tileSizeX;
            tileSizeYCheck = tileSizeY;
            return true;
        }
         return false;
    }
    bool hasGridSizeChanged()
    {
        if (gridX != gridXCheck || gridY != gridYCheck)
        {
            gridYCheck = gridY;
            gridXCheck = gridX;
            return true;
        }
         return false;
    }
    public void InitializeGrid()
    {
        Dictionary<string,myGrid> current = new Dictionary<string, myGrid>();
        for(int x = 0; x < transform.childCount; x++)
        {
            myGrid mg = transform.GetChild(x).GetComponent<myGrid>();
            current.Add(mg.posX + " " + mg.posY, mg);
        }
        grid = new myGrid[gridX , gridY];
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                if (current.ContainsKey(x + " " + y))
                {
                    grid[x, y] = current[x + " " + y];
                }
                else
                {
                    GameObject t = Instantiate(defaultTile, transform);
                    myGrid temp = t.AddComponent<myGrid>();
                    temp.setGrid(x, y);
                    t.transform.rotation = defaultTile.transform.rotation;
                    t.transform.position = tileLocation(x, y);
                }
                
            }
        }
    }
    Vector3 tileLocation(int x, int y)
    {
        return new Vector3(x * tileSizeX + transform.position.x, 0,
            y * tileSizeY + transform.position.z);
    }
    //void addToGrid()
    //{

    //    Dictionary<string, myGrid> current = new Dictionary<string, myGrid>();
    //    for (int x = 0; x < transform.childCount; x++)
    //    {
    //        myGrid mg = transform.GetChild(x).GetComponent<myGrid>();
    //        current.Add(mg.posX + " " + mg.posY, mg);
    //    }
    //    grid = new myGrid[gridX, gridY];
    //    for (int x = 0; x < gridX; x++)
    //    {
    //        for (int y = 0; y < gridY; y++)
    //        {
    //            if (tempHoldGrid.GetLength(0) >= x && tempHoldGrid.GetLength(1) >= y)
    //            {
    //                grid[x, y] = tempHoldGrid[x, y];
    //            }
    //            else
    //            {
    //                GameObject t = Instantiate(defaultTile, transform);
    //                myGrid temp = t.AddComponent<myGrid>();
    //                temp.setGrid(x, y);
    //                t.transform.rotation = defaultTile.transform.rotation;
    //                t.transform.position = tileLocation(x, y);
    //            }
    //        }
    //    }
    //}
    public void deleteFromGrid()
    {

        List<myGrid> allChildren = new List<myGrid>();
        for (int x = 0; x < transform.childCount; x++)
        {
            allChildren.Add(transform.GetChild(x).GetComponent<myGrid>());
        }
        foreach (myGrid g in allChildren)
        {
            if (g.posX > gridX || g.posY > gridY)
            {
                DestroyImmediate(g.gameObject);
            }
        }
    }
    public void resetTilePosition()
    {
        if (Heightmap == null)
        { 
            for (int x = 0; x < transform.childCount; x++)
            {
                myGrid mg = transform.GetChild(x).GetComponent<myGrid>();
                mg.transform.position = tileLocation(mg.posX, mg.posY);
                
            }
        }
        else
        {
            for (int x = 0; x < transform.childCount; x++)
            {
                myGrid mg = transform.GetChild(x).GetComponent<myGrid>();
                Mesh m = transform.GetChild(x).GetComponent<MeshFilter>().mesh;
                Vector3[] vert = getWorldSpaceVertices(mg.gameObject);
                mg.transform.position = tileLocation(mg.posX, mg.posY);
                Debug.Log(m.vertices[0]);
                for (int y = 0; y < vert.Length; y++)
                {
                    //Debug.Log(m.vertices[y]);
                    vert[y].z = Heightmap.GetPixelBilinear(vert[y].x * HeightMapStepSpeed / Heightmap.width, vert[y].z * HeightMapStepSpeed / Heightmap.height).g * HeightmapScale;
                    //Debug.Log(vert[y]);
                    vert[y].z -= HeightmapScale/2;
                    vert[y].x = m.vertices[y].x;
                    vert[y].y = m.vertices[y].y;
                }
                m.vertices = vert;
                m.RecalculateNormals();
            }
        }
    }
    public void deleteAll()
    {
        
    }
    public void UpdatePosition()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x, y].transform.position = new Vector3(x * tileSizeX, 0, y * tileSizeY);
            }
        }
    }
    public Vector3[] getWorldSpaceVertices(GameObject g)
    {
        Mesh m = g.GetComponent<MeshFilter>().mesh;
        Vector3[] vert = m.vertices;
        for (int x = 0; x < vert.Length; x++)
        {
            vert[x] = m.vertices[x] + g.transform.position;
        }
        return vert;
    }
}
