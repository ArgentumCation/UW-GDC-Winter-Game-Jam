using System.Collections.Generic;
using UnityEngine;

public class Court : MonoBehaviour
{
    public List<Collider2D> Zones;
    public List<Collider2D> Lines;
    private PlatformEffector2D centerEffector;
    private List<Renderer> lineRenderers;

    // This line turns green and can be crossed in either direction
    // -1 at the start of the game means all lines are stops
    // Do not set me directly! Instead use ChangeActiveLine
    private int activeLine = 3;
    private static readonly int Active = Shader.PropertyToID("Active");

    private void Start()
    {
        centerEffector = Lines[2].GetComponent<PlatformEffector2D>();
        
        lineRenderers = new List<Renderer>();
        foreach (var line in Lines)
            lineRenderers.Add(line.GetComponent<Renderer>());
    }
    
    // Update is called once per frame
    private void Update()
    {
        CheckZoneChanges();
        
        if (activeLine != -1)
        {
            // Set active line on the field
            for (int i = 0; i < Lines.Count; i++)
            {
                bool thisLineIsActive = (i == activeLine);
                Lines[i].isTrigger = thisLineIsActive;
                lineRenderers[i].material.SetFloat(Active, thisLineIsActive ? 1 : 0);
            }
        }
    }

    // Used for changing the activeLine since center needs some extra code
    private void ChangeActiveLine(int newLine)
    {
        // Center line
        if (newLine == 2)
        {
            centerEffector.rotationalOffset = (activeLine == 1 ? -90 : 90);
        }

        activeLine = newLine;
    }
    
    private void CheckZoneChanges()
    {
        
    }
}