using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellsSelectionButton : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private TMP_Dropdown typeDropDown;
    [SerializeField] private TMP_InputField targetCellIndexInputField;
    [SerializeField] private TMP_InputField maxDistanceInputField;
    [SerializeField] private Toggle includeTargetCellToggle;

    private Action<int, int, bool, Vector2Int> activateNeighboringCellsMethod;

    // Start is called before the first frame update
    void Start()
    {
        // Get ref to the button component
        Button button = GetComponent<Button>();

        // Add listener to the button
        button.onClick.AddListener(OnButtonClick);

        // Add listener to the drop down
        typeDropDown.onValueChanged.AddListener(OnDropdownValueChanged);

        // Set the default action value
        activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateInRangeNeighboringCells(x, y, includeInitialCell);
    }

    /// <summary>
    /// Change data based on new drop down value
    /// </summary>
    /// <param name="value"></param>
    private void OnDropdownValueChanged(int value)
    {
        switch (value)
        {
            case 0:
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateInRangeNeighboringCells(x, y, includeInitialCell); 
                break;
            case 1:
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateSquareNeighboringCells(x, y, includeInitialCell);
                break;
            case 2:
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateCrossNeighboringCells(x, y, includeInitialCell);
                break;
            case 3:
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateCircularNeighboringCells(x, y, includeInitialCell);
                break;
            case 4:
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateDiagonalNeighboringCells(x, y, includeInitialCell);
                break;
            case 5: // North
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateNeighboringCellsInDirection(x, y, includeInitialCell, new Vector2Int(0, -1));
                break;
            case 6: // East
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateNeighboringCellsInDirection(x, y, includeInitialCell, new Vector2Int(1, 0));
                break;
            case 7: // South
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateNeighboringCellsInDirection(x, y, includeInitialCell, new Vector2Int(0, 1));
                break;
            case 8: // West
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateNeighboringCellsInDirection(x, y, includeInitialCell, new Vector2Int(-1, 0));
                break;
            case 9: // North-East
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateNeighboringCellsInDirection(x, y, includeInitialCell, new Vector2Int(1, -1));
                break;
            case 10: // North-West
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateNeighboringCellsInDirection(x, y, includeInitialCell, new Vector2Int(-1, -1));
                break;
            case 11: // South-East
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateNeighboringCellsInDirection(x, y, includeInitialCell, new Vector2Int(1, 1));
                break;
            case 12: // South-West
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateNeighboringCellsInDirection(x, y, includeInitialCell, new Vector2Int(-1, 1));
                break;
            default:
                activateNeighboringCellsMethod = (x, y, includeInitialCell, direction) => grid.ActivateInRangeNeighboringCells(x, y, includeInitialCell);
                break;
        }
    }

    /// <summary>
    /// Trigger event when the button is click
    /// </summary>
    private void OnButtonClick()
    {
        // Get the data from the UI
        int targetCellIndex = ConvertToInt(targetCellIndexInputField.text);
        int maxDistance = ConvertToInt(maxDistanceInputField.text);
        bool includeTargetCell = includeTargetCellToggle.isOn;

        // Set a default direction if needed
        Vector2Int defaultDirection = Vector2Int.up;
        activateNeighboringCellsMethod(targetCellIndex, maxDistance, includeTargetCell, defaultDirection);
    }

    /// <summary>
    /// Convert a string to an int
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private int ConvertToInt(string value)
    {
        if (int.TryParse(value, out int intValue))
        {
            return intValue;
        }
        else if (float.TryParse(value, out float floatValue))
        {
            return Mathf.RoundToInt(floatValue);
        }
        else
        {
            Debug.LogWarning("Failed to convert to int");
            return 0;
        }
    }
}
