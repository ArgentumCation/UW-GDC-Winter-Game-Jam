using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Court : MonoBehaviour
{
    public List<Zone> Zones;
    public List<Line> Lines;
    private List<SpriteRenderer> lineRenderers;
    // Positions to move to when changing zones
    public List<Position> Positions;

    // This line turns green
    // Do not set me directly! Instead use ChangeCenterLine
    private int centerLine = 2;
    
    // How long it takes to move the players when the center line changes
    public float GraceTime;
    // How long until the players can move again
    private float timeUntilMove;
    // Which team is changing zones.
    // When not changing zones, this is garbage!
    private Team movingTeam;
    // Ordered from highest y value to lowest.
    // When not changing zones, this is garbage!
    private List<BodyController> orderedMovers;
    // Where the changing team started.
    // When not changing zones, this is garbage!
    private List<Vector3> moverPositions;
    
    // Used for shader editing, don't touch me
    private static readonly int Active = Shader.PropertyToID("_Active");

    private void Start()
    {
        // Grab all the SpriteRenderers from the lines
        lineRenderers = new List<SpriteRenderer>();
        foreach (var line in Lines)
            lineRenderers.Add(line.GetComponent<SpriteRenderer>());
    }
    
    private void Update()
    {
        if (GameManager.CanMove)  // Normal play
        {
            CheckZoneChanges();
        }
        else  // Changing zones
        {
            // Figure out which zone the movers are going to
            int zone = centerLine;
            if (movingTeam == Team.Red)
                zone++;
            
            // Move, with positions specified by the number of living players
            switch (moverPositions.Count)
            {
                case 3:
                    LerpToPos(movingTeam, 0, Positions[zone].Top);
                    LerpToPos(movingTeam, 1, Positions[zone].Mid);
                    LerpToPos(movingTeam, 2, Positions[zone].Bot);
                    break;
                case 2:
                    LerpToPos(movingTeam, 0, Positions[zone].Top);
                    LerpToPos(movingTeam, 1, Positions[zone].Bot);
                    break;
                case 1:
                    LerpToPos(movingTeam, 0, Positions[zone].Bot);
                    break;
                default:  // Should never get called
                    break;
            }
            
            // Stop all velocities. This is also where you'd set rotations
            foreach(BodyController body in GameManager.RedController.bodies)
                body.StopVelocity();
            
            // Manage how much time is left before play resumes
            timeUntilMove -= Time.deltaTime;
            if (timeUntilMove <= 0)
                GameManager.CanMove = true;
        }
    }
    
    private void LerpToPos(Team team, int index, Vector2 targetXY)
    {
        BodyController body = orderedMovers[index];
        Vector3 startPos = moverPositions[index];
        Vector3 targetPos = new Vector3(targetXY.x, targetXY.y, body.transform.position.z);
        body.transform.position = Vector3.Lerp(startPos, targetPos, 1 - timeUntilMove / GraceTime);
    }

    // Used for changing the centerLine since center needs some extra code
    private void ChangeCenterLine(int newLine)
    {
        // Set green line
        // This needs to be done to all so the old one can be removed
        // and before changing to the new center
        for (int i = 0; i < Lines.Count; i++)
            lineRenderers[i].material.SetFloat(Active, i == centerLine ? 1 : 0);
        
        centerLine = newLine;
        
        // Update which lines cannot be crossed in which directions
        for (int i = 0; i < Lines.Count; i++)
        {
            Lines[i].rotation = (newLine > i) ? 90 : -90;
            Lines[i].useOneWay = (i != centerLine);
        }
        
        // Signal to move the players to the new positions
        timeUntilMove = GraceTime;
        GameManager.CanMove = false;
        
        // Set up some stuff to ensure the lerping works right
        moverPositions = new List<Vector3>();
        foreach (BodyController body in orderedMovers)
            moverPositions.Add(body.transform.position);
    }
    
    // If a zone change is required, this will also call that
    private void CheckZoneChanges()
    {
        // Don't do anything if we're changing zones
        if (!GameManager.CanMove)
            return;
        
        // Figure out which zones to check
        int redZone = centerLine + 1;
        int blueZone = centerLine;
        
        // Check zones
        if (Zones[redZone].PlayersInside == 0)
        {
            movingTeam = Team.Blue;
            orderedMovers = GameManager.BlueController.bodies.OrderBy(b => -b.transform.position.y).ToList();
            ChangeCenterLine(centerLine + 1);
        }
        else if (Zones[blueZone].PlayersInside == 0)
        {
            movingTeam = Team.Red;
            orderedMovers = GameManager.RedController.bodies.OrderBy(b => -b.transform.position.y).ToList();
            ChangeCenterLine(centerLine - 1);
        }
    }
}