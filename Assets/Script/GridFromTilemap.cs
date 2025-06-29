using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class GridFromTilemap : MonoBehaviour
{
    public Tilemap walkableTilemap;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public LayerMask unwalkableMask;
    public bool isReady = false;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;
    public Node[,] grid;

    public List<Node> path;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
        isReady = true;
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - 
            Vector3.right * gridWorldSize.x / 2 - 
            Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + 
                                     Vector3.right * (x * nodeDiameter + nodeRadius) +
                                     Vector3.up * (y * nodeDiameter + nodeRadius);

                Vector3Int cellPosition = walkableTilemap.WorldToCell(worldPoint);
                TileBase tile = walkableTilemap.GetTile(cellPosition);

                bool walkable = tile != null;
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX &&
                    checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    void OnDrawGizmos()
    {
        if (grid != null)
        {
            foreach (var node in grid)
            {
                Gizmos.color = node.walkable ? Color.white : Color.red;
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeRadius * 2 - .1f));
            }
        }
    }
}