using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateGridButton : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private TMP_InputField gridWidthInputField;
    [SerializeField] private TMP_InputField gridHeightInputField;


    // Start is called before the first frame update
    void Start()
    {
        // Get ref to the button component
        Button button = GetComponent<Button>();

        // Add listener to the button
        button.onClick.AddListener(OnButtonClick);

        // Initialize grid size inputField
        gridWidthInputField.text = grid.Width.ToString();
        gridHeightInputField.text = grid.Height.ToString();
    }


    /// <summary>
    /// Trigger event when the button is click
    /// </summary>
    private void OnButtonClick()
    {
        // Get the data from the UI
        int targetWidth = ConvertToInt(gridWidthInputField.text);
        int targetHeight = ConvertToInt(gridHeightInputField.text);

        // Generate grid with new values
        grid.updateGridSize(targetWidth, targetHeight);
        grid.GenerateGrid(targetWidth, targetHeight);
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
