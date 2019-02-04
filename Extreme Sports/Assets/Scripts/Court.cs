using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Court : MonoBehaviour
{
    public List<Zone> Zones;
    public List<Line> Lines;
    private List<SpriteRenderer> lineRenderers;
    public List<Position> Positions;

    // This line turns green
    // -1 at the start of the game means all lines are stops
    // Do not set me directly! Instead use ChangeCenterLine
    private int centerLine = -1;
    
    public float GraceTime;
    private Team movingTeam;
    private float timeUntilMove;
    private List<Vector3> moverPositions;
    private List<BodyController> orderedMovers;
    
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
        if (GameManager.CanMove)
        {
            CheckZoneChanges();
        }
        else
        {
            int zone = centerLine;
            if (movingTeam == Team.Red)
            {
                zone++;
            }
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
                default:
                    break;
            }
            
            foreach(BodyController body in GameManager.RedController.bodies)
                body.StopVelocity();
            
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
        
        for (int i = 0; i < Lines.Count; i++)
        {
            Lines[i].rotation = (newLine > i) ? 90 : -90;
            Lines[i].useOneWay = (i != centerLine);
        }
        
        timeUntilMove = GraceTime;
        GameManager.CanMove = false;
        
        moverPositions = new List<Vector3>();
        foreach (BodyController body in orderedMovers)
            moverPositions.Add(body.transform.position);
    }
    
    private void CheckZoneChanges()
    {
        if (!GameManager.CanMove)
            return;
        
        int testLine = (centerLine == -1 ? 2 : centerLine);
        
        int redZone = testLine + 1;
        int blueZone = testLine;
        
        if (Zones[redZone].PlayersInside == 0)
        {
            movingTeam = Team.Blue;
            orderedMovers = GameManager.BlueController.bodies.OrderBy(b => -b.transform.position.y).ToList();
            ChangeCenterLine(testLine + 1);
        }
        else if (Zones[blueZone].PlayersInside == 0)
        {
            movingTeam = Team.Red;
            orderedMovers = GameManager.RedController.bodies.OrderBy(b => -b.transform.position.y).ToList();
            ChangeCenterLine(testLine - 1);
        }
    }
}