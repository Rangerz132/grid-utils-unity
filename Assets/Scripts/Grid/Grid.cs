using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width;
    public int height;

    [field: SerializeField] public float OffsetX { get; private set; }
    [field: SerializeField] public float OffsetY { get; private set; }
    [field: SerializeField] public Cell Cell { get; private set; }  
    [field: SerializeField] public List<Cell> Cells { get; private set; } = new List<Cell>();


    private GridUtils gridUtils;

    
    // Start is called before the first frame update
    void Start()
    {
        gridUtils = new GridUtils();
        GenerateGrid();
    }

    /// <summary>
    /// Generate the grid
    /// </summary>
    public void GenerateGrid()
    {
        // Clear grid before generating a new one
        ClearGrid();

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                // Get the current index and position according to the x and y
                int currentIndex = gridUtils.GetCellIndex(height, x, y);
                Vector3 currentPosition = new Vector3(x * OffsetX - width/2, y * OffsetY + height / 2, 0);

                // Instantite Cell
                Cell currentCell = Instantiate(Cell);

                // Initialize Cell with the correct properties
                currentCell.Initialize(x, y, currentIndex, currentPosition);

                // Set cell children of the grid
                currentCell.transform.SetParent(transform);

                // Add Cell to the list of Cells
                Cells.Add(currentCell);
            }
        }
    }


    /// <summary>
    /// Destroy any element in the grid
    /// </summary>
    private void ClearGrid()
    {
        // Destroy all the gameObject
        for (var i = 0; i < Cells.Count; i++)
        {
            Destroy(Cells[i].gameObject);
        }

        // Clear the list
        Cells.Clear();
    }

    /// <summary>
    /// Deactivate all cells
    /// </summary>
    public void DeactivateGrid()
    {
        // Deactivate all cells
        for (var i = 0; i < Cells.Count; i++)
        {
            Cells[i].DeactivateCellVisual();
        }
    }

    /// <summary>
    /// Activate all corresponding cells
    /// </summary>
    private void ActivateGrid(List<Cell> cellList)
    {
        for (var i = 0; i < cellList.Count; i++)
        {
            cellList[i].ActivateCellVisual();
        }
    }

    /// <summary>
    /// Activate neighbor cells in a square shape
    /// </summary>
    /// <param name="initialCellIndex"></param>
    /// <param name="maxDistance"></param>
    /// <param name="includeInitialCell"></param>
    public void ActivateSquareNeighboringCells(int initialCellIndex, int maxDistance, bool includeInitialCell)
    {
        var cellList = gridUtils.GetSquareNeighboringCells(height, gridUtils.GetCellAtIndex(initialCellIndex, Cells), Cells, maxDistance, includeInitialCell);
        ActivateGrid(cellList);
    }

    /// <summary>
    /// Activate neighbor cells in a Cross shape
    /// </summary>
    /// <param name="initialCellIndex"></param>
    /// <param name="maxDistance"></param>
    /// <param name="includeInitialCell"></param>
    public void ActivateCrossNeighboringCells(int initialCellIndex, int maxDistance, bool includeInitialCell)
    {
        var cellList = gridUtils.GetCrossNeighboringCells(height, gridUtils.GetCellAtIndex(initialCellIndex, Cells), Cells, maxDistance, includeInitialCell);
        ActivateGrid(cellList);
    }

    /// <summary>
    /// Activate neighbor cells in a circular shape
    /// </summary>
    /// <param name="initialCellIndex"></param>
    /// <param name="maxDistance"></param>
    /// <param name="includeInitialCell"></param>
    public void ActivateCircularNeighboringCells(int initialCellIndex, int maxDistance, bool includeInitialCell)
    {
        var cellList = gridUtils.GetCircularNeighboringCells(height, gridUtils.GetCellAtIndex(initialCellIndex, Cells), Cells, maxDistance, includeInitialCell);
        ActivateGrid(cellList);
    }

    /// <summary>
    /// Activate neighbor cells in a Diagonal shape
    /// </summary>
    /// <param name="initialCellIndex"></param>
    /// <param name="maxDistance"></param>
    /// <param name="includeInitialCell"></param>
    public void ActivateDiagonalNeighboringCells(int initialCellIndex, int maxDistance, bool includeInitialCell)
    {
        var cellList = gridUtils.GetDiagonalNeighboringCells(height, gridUtils.GetCellAtIndex(initialCellIndex, Cells), Cells, maxDistance, includeInitialCell);
        ActivateGrid(cellList);
    }


}
