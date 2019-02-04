using System.Collections.Generic;
using UnityEngine;

public class Court : MonoBehaviour
{
    public List<Zone> Zones;
    public List<Line> Lines;
    private List<SpriteRenderer> lineRenderers;
    public List<Position> Positions;

    // This line turns green and can be crossed in either direction
    // -1 at the start of the game means all lines are stops
    // Do not set me directly! Instead use ChangeActiveLine
    private int activeLine = -1;
    
    private float timeUntilMove;
    private List<Vector3> redPositions;
    private List<Vector3> bluePositions;
    
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
            int redZone = activeLine + 1;
            int blueZone = activeLine;
            Vector3 v;
            
            switch (redPositions.Count)
            {
                case 3:
                    v = new Vector3(Positions[redZone].Top.x, Positions[redZone].Top.y, GameManager.RedController.bodies[0].transform.position.z);
                    GameManager.RedController.bodies[0].transform.position = Vector3.Lerp(GameManager.RedController.bodies[0].transform.position, v, Smooth(timeUntilMove));
                    v = new Vector3(Positions[redZone].Mid.x, Positions[redZone].Mid.y, GameManager.RedController.bodies[1].transform.position.z);
                    GameManager.RedController.bodies[1].transform.position = Vector3.Lerp(GameManager.RedController.bodies[1].transform.position, v, Smooth(timeUntilMove));
                    v = new Vector3(Positions[redZone].Bot.x, Positions[redZone].Bot.y, GameManager.RedController.bodies[2].transform.position.z);
                    GameManager.RedController.bodies[2].transform.position = Vector3.Lerp(GameManager.RedController.bodies[2].transform.position, v, Smooth(timeUntilMove));
                    break;
                case 2:
                    v = new Vector3(Positions[redZone].Top.x, Positions[redZone].Top.y, GameManager.RedController.bodies[0].transform.position.z);
                    GameManager.RedController.bodies[0].transform.position = Vector3.Lerp(GameManager.RedController.bodies[0].transform.position, v, Smooth(timeUntilMove));
                    v = new Vector3(Positions[redZone].Bot.x, Positions[redZone].Bot.y, GameManager.RedController.bodies[1].transform.position.z);
                    GameManager.RedController.bodies[1].transform.position = Vector3.Lerp(GameManager.RedController.bodies[1].transform.position, v, Smooth(timeUntilMove));
                    break;
                case 1:
                    v = new Vector3(Positions[redZone].Mid.x, Positions[redZone].Mid.y, GameManager.RedController.bodies[0].transform.position.z);
                    GameManager.RedController.bodies[0].transform.position = Vector3.Lerp(GameManager.RedController.bodies[0].transform.position, v, Smooth(timeUntilMove));
                    break;
                default:
                    break;
            }
            
            switch (bluePositions.Count)
            {
                case 3:
                    v = new Vector3(Positions[blueZone].Top.x, Positions[blueZone].Top.y, GameManager.BlueController.bodies[0].transform.position.z);
                    GameManager.BlueController.bodies[0].transform.position = Vector3.Lerp(GameManager.BlueController.bodies[0].transform.position, v, Smooth(timeUntilMove));
                    v = new Vector3(Positions[blueZone].Mid.x, Positions[blueZone].Mid.y, GameManager.BlueController.bodies[1].transform.position.z);
                    GameManager.BlueController.bodies[1].transform.position = Vector3.Lerp(GameManager.BlueController.bodies[1].transform.position, v, Smooth(timeUntilMove));
                    v = new Vector3(Positions[blueZone].Bot.x, Positions[blueZone].Bot.y, GameManager.BlueController.bodies[2].transform.position.z);
                    GameManager.BlueController.bodies[2].transform.position = Vector3.Lerp(GameManager.BlueController.bodies[2].transform.position, v, Smooth(timeUntilMove));
                    break;
                case 2:
                    v = new Vector3(Positions[blueZone].Top.x, Positions[blueZone].Top.y, GameManager.BlueController.bodies[0].transform.position.z);
                    GameManager.BlueController.bodies[0].transform.position = Vector3.Lerp(GameManager.BlueController.bodies[0].transform.position, v, Smooth(timeUntilMove));
                    v = new Vector3(Positions[blueZone].Bot.x, Positions[blueZone].Bot.y, GameManager.BlueController.bodies[1].transform.position.z);
                    GameManager.BlueController.bodies[1].transform.position = Vector3.Lerp(GameManager.BlueController.bodies[1].transform.position, v, Smooth(timeUntilMove));
                    break;
                case 1:
                    v = new Vector3(Positions[blueZone].Mid.x, Positions[blueZone].Mid.y, GameManager.BlueController.bodies[0].transform.position.z);
                    GameManager.BlueController.bodies[0].transform.position = Vector3.Lerp(GameManager.BlueController.bodies[0].transform.position, v, Smooth(timeUntilMove));
                    break;
                default:
                    break;
            }
            
            timeUntilMove -= Time.deltaTime;
            if (timeUntilMove >= 0)
                GameManager.CanMove = true;
            return;
        }
        
        CheckZoneChanges();
    }
    
    private float Smooth(float inFrac)
    {
        return 0.5087537579583935f * Mathf.Atan(3 * inFrac + 1.5f) + 0.5f;
    }

    // Used for changing the activeLine since center needs some extra code
    private void ChangeActiveLine(int newLine)
    {
        // Set active line on the field
        for (int i = 0; i < Lines.Count; i++)
        {
            bool thisLineIsActive = (i == activeLine);
            Lines[i].enabled = thisLineIsActive;
            lineRenderers[i].material.SetFloat(Active, thisLineIsActive ? 1 : 0);
        }
        
        activeLine = newLine;
        
        for (int i = 0; i < Lines.Count; i++)
        {
            Lines[i].rotation = (newLine > i) ? 90 : -90;
        }
        Lines[2].useOneWay = true;
        
        timeUntilMove = 1;
        GameManager.CanMove = false;
        
        redPositions = new List<Vector3>();
        foreach (BodyController body in GameManager.RedController.bodies)
            redPositions.Add(body.transform.position);
        bluePositions = new List<Vector3>();
        foreach (BodyController body in GameManager.BlueController.bodies)
            bluePositions.Add(body.transform.position);
    }
    
    private void CheckZoneChanges()
    {
        if (!GameManager.CanMove)
            return;
        
        int testLine = (activeLine == -1 ? 2 : activeLine);
        
        int redZone = testLine + 1;
        int blueZone = testLine;
        
        if (Zones[redZone].PlayersInside == 0)
            ChangeActiveLine(testLine + 1);
        else if (Zones[blueZone].PlayersInside == 0)
            ChangeActiveLine(testLine - 1);
    }
}