using UnityEngine;

public class BodyController : MonoBehaviour
{
    public Element element;
    public float bulletSpeed;
    public GameObject bullet;
    private PlayerController playerController;
    private float charge = 1;
    private Rigidbody2D rb2d;

    public Renderer LoadingBarRenderer;
    public Color FullColor;
    public Color EmptyColor;

    public float chargeSpeed;
    
    private static readonly int Color = Shader.PropertyToID("_FullColor");
    private static readonly int EmptyColor1 = Shader.PropertyToID("_EmptyColor");
    private static readonly int Charge = Shader.PropertyToID("_Charge");


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
        if (element == Element.fire && charge < 1)
            charge += chargeSpeed * Time.deltaTime;

        LoadingBarRenderer.material.SetColor(Color, FullColor);
        LoadingBarRenderer.material.SetColor(EmptyColor1, EmptyColor);
        LoadingBarRenderer.material.SetFloat(Charge, charge);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (element == Element.water && other.CompareTag("waterpickup") && charge < 1)
            charge += chargeSpeed * Time.deltaTime;
        
        if (element == Element.earth && other.CompareTag("earthpickup"))
            charge += chargeSpeed * Time.deltaTime;
    }

    public Rigidbody2D getRigidBody()
    {
        return rb2d;
    }
    
    public void StopVelocity()
    {
        rb2d.velocity = Vector3.zero;
    }

    public void Fire()
    {
        if (charge >= 1)
        {
            GameObject bulletInstance = Instantiate(bullet,
                transform.position + transform.right / 1.5f, Quaternion.identity);
            Rigidbody2D bulletrb2d = bulletInstance.GetComponent<Rigidbody2D>();
            bulletrb2d.velocity = transform.right.normalized * bulletSpeed;
            charge = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {


        if (other.CompareTag("court"))
            playerController.Kill(this);
    }
}