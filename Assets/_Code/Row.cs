using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row
{
    public GameObject[] Cells;

    private bool _isVertical;
    public Row(int size, bool isVertical)
    {
        Cells = new GameObject[size];
        _isVertical = isVertical;
    }

    public void ResetRow()
    {
        foreach (GameObject go in Cells)
        {
            GameObject.Destroy(go);
        }
    }
    
    // Pull the first entry out, shift everything to the left
    // return the pulled first entry
    public GameObject PullRow(GameObject newEntry)
    {
        Vector3 dir = _isVertical ? Vector3.up : Vector3.right;
        
        GameObject firstEntry = null;
        firstEntry = Cells[0];
        
        for (int i = 0; i < Cells.Length - 1; i++)
        {
            Cells[i] = Cells[i + 1];
            Cells[i].transform.position += -dir;
        }
        
        newEntry.transform.position += -dir;
        Cells[Cells.Length - 1] = newEntry;
        
        firstEntry.transform.parent = null;
        
        return firstEntry;
    }

    public GameObject PushRow(GameObject newEntry)
    {
        Vector3 dir = _isVertical ? Vector3.up : Vector3.right;
        
        GameObject firstEntry = null;
        firstEntry = Cells[Cells.Length - 1];
        
        for (int i = Cells.Length - 1; i > 0 ; i--)
        {
            Cells[i] = Cells[i - 1];
            Cells[i].transform.position += dir;
        }
        
        newEntry.transform.position += dir;
        Cells[0] = newEntry;

        firstEntry.transform.parent = null;
        
        return firstEntry;
    }
}