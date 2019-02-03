using UnityEngine;

public class BodyController : MonoBehaviour
{
    public Element element;
    public float bulletSpeed;
    public GameObject bullet;
    private PlayerController playerController;
    private float charge = 1f;
    private Rigidbody2D rb2d;

    public Renderer LoadingBarRenderer;
    public Color FullColor;
    public Color EmptyColor;

    public float chargeSpeed;
    
    private static readonly int Color = Shader.PropertyToID("_FullColor");
    private static readonly int EmptyColor1 = Shader.PropertyToID("_EmptyColor");
    private static readonly int Charge = Shader.PropertyToID("_Charge");

    public Vector2 GetPosition()
    {
        return transform.position;
    }
    // Start is called before the first frame update
    public Rigidbody2D getRigidBody()
    {
        
        return rb2d;
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Init(PlayerController p)
    {
        playerController = p;
    }

    private void Update()
    {
        if (element == Element.fire)
        {
            if (charge < 1f)
            {
                charge += chargeSpeed * Time.deltaTime;
            }
        }

        LoadingBarRenderer.material.SetColor(Color, FullColor);
        LoadingBarRenderer.material.SetColor(EmptyColor1, EmptyColor);
        LoadingBarRenderer.material.SetFloat(Charge, charge);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("waterpickup") && element == Element.water)
        {
            if (charge < 1f)
            {
                charge += chargeSpeed * Time.deltaTime;
            }
        }
    }


    public void Fire()
    {
        if (charge >= 1f)
        {
            GameObject bulletInstance = Instantiate(bullet,
                transform.position + transform.right / 1.5f, Quaternion.identity);
            Rigidbody2D bulletrb2d = bulletInstance.GetComponent<Rigidbody2D>();
            bulletrb2d.velocity = transform.right.normalized * bulletSpeed;
            charge = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("earthpickup") && element == Element.earth)
        {
            charge = 1f;
        }

        if (other.CompareTag("court"))
        {
            playerController.Kill(this);
        }
    }
}