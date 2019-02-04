using UnityEngine;

public class Line : MonoBehaviour
{
    private PolygonCollider2D collider;
    private  PlatformEffector2D effector;
    
    public bool enabled
    {
        set => collider.isTrigger = value;
    }
    
    public int rotation
    {
        set => effector.rotationalOffset = value;
    }
    
    public bool useOneWay
    {
        set => effector.useOneWay = value;
    }
    
    private void Awake()
    {
        collider = GetComponent<PolygonCollider2D>();
        effector = GetComponent<PlatformEffector2D>();
    }
}