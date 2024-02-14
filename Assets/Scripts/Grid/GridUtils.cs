using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUtils 
{
    /// <summary>
    /// Get the index of a Cell in the grid
    /// </summary>
    /// <param name="gridHeight"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public int GetCellIndex(int gridHeight, int x, int y)
    {
        return (x * gridHeight + y);
    }

    /// <summary>
    /// Get the position of a Cell according to it's index
    /// </summary>
    /// <param name="gridHeight"></param>
    /// <param name="cellIndex"></param>
    public Vector2 GetPositionFromCellIndex(int gridHeight, int cellIndex)
    {
        return new Vector2(Mathf.Floor(cellIndex / gridHeight), cellIndex % gridHeight);
    }

    /// <summary>
    /// Get a Cell a in the grid according to it's index
    /// </summary>
    /// <param name="cellIndex"></param>
    /// <param name="cellList"></param>
    public Cell GetCellAtIndex(int cellIndex, List<Cell> cellList)
    {
        if (cellIndex < 0 || cellIndex >= cellList.Count)
        {
            return null;
        }

        return cellList[cellIndex];
    }

    /// <summary>
    /// Get the neighbor Cell
    /// </summary>
    /// <param name="gridHeight"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    /// <param name="cell"></param>
    /// <param name="cellList"></param>
    public Cell GetNeighborCell(int gridHeight, int offsetX, int offsetY, Cell cell, List<Cell> cellList)
    {
        int neighborCellIndex = GetCellIndex(gridHeight, cell.x + offsetX, cell.y + offsetY);
        return GetCellAtIndex(neighborCellIndex, cellList);
    }

    /// <summary>
    /// Get all neighboring cells around a specific cell
    /// </summary>
    /// <param name="gridHeight"></param>
    /// <param name="cell"></param>
    /// <param name="cellList"></param>
    public List<Cell> GetSquareNeighboringCells(int gridHeight, Cell cell, List<Cell> cellList, int maxDistance, bool includeInitialCell)
    {
        List<Cell> neighbors = new List<Cell>();

        // Iterate through all possible offsets up to the maximum distance
        for (int dx = -maxDistance; dx <= maxDistance; dx++)
        {
            for (int dy = -maxDistance; dy <= maxDistance; dy++)
            {
                // Skip the center cell (0,0) which is the cell itself
                if (dx == 0 && dy == 0 && !includeInitialCell)
                    continue;

                int neighborX = cell.x + dx;
                int neighborY = cell.y + dy;

                // Check if the neighboring cell is within the bounds of the grid
                if (neighborX >= 0 && neighborX < gridHeight && neighborY >= 0 && neighborY < gridHeight)
                {
                    // Get the neighbor cell index and retrieve the cell from the list
                    int neighborCellIndex = GetCellIndex(gridHeight, neighborX, neighborY);
                    Cell neighborCell = GetCellAtIndex(neighborCellIndex, cellList);

                    // Add the neighbor cell to the list
                    if (neighborCell != null)
                        neighbors.Add(neighborCell);
                }
            }
        }

        return neighbors;
    }

    /// <summary>
    /// Get all neighboring cells in a cross shape (top, bottom, left, and right directions) around a specific cell up to a maximum distance
    /// </summary>
    /// <param name="gridHeight">Height of the grid</param>
    /// <param name="cell">The cell for which cross neighbors are to be found</param>
    /// <param name="cellList">List of all cells</param>
    /// <param name="maxDistance">Maximum distance to consider for cross neighbors</param>
    /// <param name="includeInitialCell">Whether to include the initial cell in the list of neighbors</param>
    /// <returns>List of cross neighboring cells</returns>
    public List<Cell> GetCrossNeighboringCells(int gridHeight, Cell cell, List<Cell> cellList, int maxDistance, bool includeInitialCell)
    {
        List<Cell> neighbors = new List<Cell>();

        // Define the offsets for top, bottom, left, and right directions
        int[] offsetX = { 0, 0, -1, 1 };
        int[] offsetY = { -1, 1, 0, 0 };

        // Include the initial cell if specified
        if (includeInitialCell)
        {
            neighbors.Add(cell);
        }

        // Iterate through the offsets up to the maximum distance
        for (int dist = 1; dist <= maxDistance; dist++)
        {
            foreach (int i in new int[] { 0, 1, 2, 3 })
            {
                int neighborX = cell.x + (offsetX[i] * dist);
                int neighborY = cell.y + (offsetY[i] * dist);

                // Check if the neighboring cell is within the bounds of the grid
                if (neighborX >= 0 && neighborX < gridHeight && neighborY >= 0 && neighborY < gridHeight)
                {
                    // Get the neighbor cell index and retrieve the cell from the list
                    int neighborCellIndex = GetCellIndex(gridHeight, neighborX, neighborY);
                    Cell neighborCell = GetCellAtIndex(neighborCellIndex, cellList);

                    // Add the neighbor cell to the list
                    if (neighborCell != null)
                        neighbors.Add(neighborCell);
                }
            }
        }

        return neighbors;
    }

    /// <summary>
    /// Get all neighboring cells in diagonal positions around a specific cell
    /// </summary>
    /// <param name="gridHeight">Height of the grid</param>
    /// <param name="cell">The cell for which diagonal neighbors are to be found</param>
    /// <param name="cellList">List of all cells</param>
    /// <param name="maxDistance">Maximum distance to consider for diagonal cells</param>
    public List<Cell> GetDiagonalNeighboringCells(int gridHeight, Cell cell, List<Cell> cellList, int maxDistance, bool includeInitialCell)
    {
        List<Cell> neighbors = new List<Cell>();

        // Iterate through all possible offsets up to the maximum distance
        for (int dx = -maxDistance; dx <= maxDistance; dx++)
        {
            for (int dy = -maxDistance; dy <= maxDistance; dy++)
            {
                // Skip the center cell (0,0) which is the cell itself
                if (dx == 0 && dy == 0 && !includeInitialCell)
                    continue;

                // Skip if both dx and dy are not of the same absolute value, as it's not a diagonal cell
                if (Mathf.Abs(dx) != Mathf.Abs(dy))
                    continue;

                int neighborX = cell.x + dx;
                int neighborY = cell.y + dy;

                // Check if the neighboring cell is within the bounds of the grid
                if (neighborX >= 0 && neighborX < gridHeight && neighborY >= 0 && neighborY < gridHeight)
                {
                    // Get the neighbor cell index and retrieve the cell from the list
                    int neighborCellIndex = GetCellIndex(gridHeight, neighborX, neighborY);
                    Cell neighborCell = GetCellAtIndex(neighborCellIndex, cellList);

                    // Add the neighbor cell to the list
                    if (neighborCell != null)
                        neighbors.Add(neighborCell);
                }
            }
        }

        return neighbors;
    }

    /// <summary>
    /// Get all neighboring cells in a circular pattern around a specific cell
    /// </summary>
    /// <param name="gridHeight">Height of the grid</param>
    /// <param name="cell">The cell for which circular neighbors are to be found</param>
    /// <param name="cellList">List of all cells</param>
    /// <param name="radius">Radius of the circular pattern</param>
    public List<Cell> GetCircularNeighboringCells(int gridHeight, Cell cell, List<Cell> cellList, int radius, bool includeInitialCell)
    {
        List<Cell> neighbors = new List<Cell>();

        // Calculate the squared radius for efficient comparison
        int radiusSquared = radius * radius;

        // Iterate through all cells in the grid
        for (int x = 0; x < gridHeight; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                int dx = x - cell.x;
                int dy = y - cell.y;

                // Skip the center cell (0,0) which is the cell itself
                if (dx == 0 && dy == 0 && !includeInitialCell)
                    continue;

                // Check if the distance from the initial cell to the current cell is within the radius
                if (dx * dx + dy * dy <= radiusSquared)
                {
                    // Get the neighbor cell index and retrieve the cell from the list
                    int neighborCellIndex = GetCellIndex(gridHeight, x, y);
                    Cell neighborCell = GetCellAtIndex(neighborCellIndex, cellList);

                    // Add the neighbor cell to the list
                    if (neighborCell != null)
                        neighbors.Add(neighborCell);
                }
            }
        }

        return neighbors;
    }
}
