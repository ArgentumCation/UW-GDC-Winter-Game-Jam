using System.Collections.Generic;
using UnityEngine;

public class Court : MonoBehaviour
{
    public List<Zone> Zones;
    public List<Line> Lines;
    private List<SpriteRenderer> lineRenderers;

    // This line turns green and can be crossed in either direction
    // -1 at the start of the game means all lines are stops
    // Do not set me directly! Instead use ChangeActiveLine
    private int activeLine = -1;
    
    private float timeUntilMove;
    private List<Vector2> positions;
    
    private static readonly int Active = Shader.PropertyToID("_Active");

    private void Start()
    {
        lineRenderers = new List<SpriteRenderer>();
        foreach (var line in Lines)
            lineRenderers.Add(line.GetComponent<SpriteRenderer>());
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.CanMove)
        {
            
            
            timeUntilMove -= Time.deltaTime;
            if (timeUntilMove >= 0)
                GameManager.CanMove = true;
            return;
        }
        
        CheckZoneChanges();

        if (activeLine != -1)
        {
            // Set active line on the field
            for (int i = 0; i < Lines.Count; i++)
            {
                bool thisLineIsActive = (i == activeLine);
                Lines[i].enabled = thisLineIsActive;
                lineRenderers[i].material.SetFloat(Active, thisLineIsActive ? 1 : 0);
            }
        }
    }

    // Used for changing the activeLine since center needs some extra code
    private void ChangeActiveLine(int newLine)
    {
        // Center line
        for (int i = 0; i < Lines.Count; i++)
        {
            Lines[activeLine].rotation = (newLine > activeLine) ? 90 : -90;
        }
        Lines[2].useOneWay = true;

        activeLine = newLine;
        
        timeUntilMove = 1;
        GameManager.CanMove = false;
    }
    
    private void CheckZoneChanges()
    {
        int redZone = activeLine;
        int blueZone = activeLine + 1;
        if (activeLine == -1)
        {
            redZone = 2;
            blueZone = 3;
        }
        
        if (Zones[redZone].PlayersInside == 0)
            ChangeActiveLine(activeLine + 1);
        else if (Zones[blueZone].PlayersInside == 0)
            ChangeActiveLine(activeLine - 1);
    }
}