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

    private Action<int, int, bool> activateNeighboringCellsMethod;

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
        activateNeighboringCellsMethod = grid.ActivateSquareNeighboringCells;
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
                activateNeighboringCellsMethod = grid.ActivateSquareNeighboringCells;
                break;
            case 1:
                activateNeighboringCellsMethod = grid.ActivateCrossNeighboringCells;
                break;
            case 2:
                activateNeighboringCellsMethod = grid.ActivateCircularNeighboringCells;
                break;
            case 3:
                activateNeighboringCellsMethod = grid.ActivateDiagonalNeighboringCells;
                break;
            default:
                activateNeighboringCellsMethod = grid.ActivateCircularNeighboringCells;
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

        activateNeighboringCellsMethod(targetCellIndex, maxDistance, includeTargetCell);

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
