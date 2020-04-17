using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RocketController : MonoBehaviour
{
    private Planet planet;
    public float rocketTurboForce = 200.0f;
    [SerializeField]
    private float fuel = 100f;
    [SerializeField]
    private float fuelBurnRate = 20f;
    private Vector2 spawnPos;
    public float currentFuel;
    [SerializeField]
    private Slider fuelSlider;
    [SerializeField]
    private ParticleSystem turboParticle;
    public float forwardMovementSpeed = 3.0f;
    public Rigidbody2D rb;
    public int touch;
    public bool isGrounded;
    public bool isDead;
    public bool turboActive;
    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 30;
        rb = GetComponent<Rigidbody2D>();
        currentFuel = fuel;
        spawnPos = rb.transform.position;
        turboParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        touchInputs();
        //testPC();
        fuelSlider.value = currentFuel / fuel;
    }

    private void FixedUpdate()
    {
        adjustTurbo(turboActive);
        Vector2 movementOnX = rb.velocity;
        movementOnX.x = forwardMovementSpeed;
        rb.velocity = movementOnX;
    }

    // A method to handle touch inputs of the player

    public void touchInputs()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary && currentFuel > 0)
        {
            turboParticle.Play();
            turboActive = true;
            currentFuel -= fuelBurnRate * Time.deltaTime;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            turboParticle.Stop();
            turboActive = false;
        }
    }

    public void testPC()
    {
        if (Input.GetMouseButton(0) && currentFuel > 0)
        {
            turboParticle.Play();
            turboActive = true;
            currentFuel -= fuelBurnRate * Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            turboParticle.Stop();
            turboActive = false;
        }
    }

    // A method to check when turbo is active or not
    // and set velocity positive if is true
    // else set velocity negative if is false

    public bool adjustTurbo(bool turbo)
    {
        switch (turbo)
        {
            case true:
                rb.velocity = new Vector2(0.0f, rocketTurboForce * Time.deltaTime);
                //rb.AddForce(new Vector2(0f, rocketTurboForce), ForceMode2D.Force);
                break;
            case false:
                rb.velocity = new Vector2(0.0f, -rocketTurboForce * Time.deltaTime);
                //rb.AddForce(new Vector2(0f, 0f), ForceMode2D.Force);
                break;
        }
        return turbo;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            GameManager.instance.OnDestroy();
            SceneManager.LoadScene("Gameplay");
            //transform.position = spawnPos;
            currentFuel = 100f;
            rb.velocity = Vector2.zero; // set rocket velocity value 0 to spawn stable
        }
    }

}
