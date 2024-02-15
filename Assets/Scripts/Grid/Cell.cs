using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;
    public int index;
    public float size;

    [SerializeField] private SpriteRenderer cellVisual;
    [SerializeField] private TextMeshPro cellText;
    [SerializeField] private Color activateColor;
    [SerializeField] private Color deactivateColor;

    /// <summary>
    /// Initialize cell with corresponding data
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="index"></param>
    /// <param name="position"></param>
    public void Initialize(int x, int y, int index, Vector3 position)
    {
        this.x = x;
        this.y = y;
        this.index = index;
        transform.position = position;
        SetCellText();

    }

    /// <summary>
    /// Set the cell text value to it's index
    /// </summary>
    public void SetCellText()
    {
        cellText.text = index.ToString();
    }

    /// <summary>
    /// Change the color of the Cell visual to the Activate one
    /// </summary>
    public void ActivateCellVisual()
    {
        cellVisual.color = activateColor;
    }

    /// <summary>
    /// Change the color of the Cell visual to the Deactivate one
    /// </summary>
    public void DeactivateCellVisual()
    {
       cellVisual.color = deactivateColor;
    }
}
