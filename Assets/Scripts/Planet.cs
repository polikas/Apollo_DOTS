using UnityEngine;

public class Planet : MonoBehaviour
{
    private RocketController rocket;
    private const float gravityForce = -9.81f;
    [SerializeField] float mass = 2.5f;
    [SerializeField] float dragForce;
    private Vector2 planetVector;
    private bool isTrue = false;

    // Start is called before the first frame update
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket").GetComponent<RocketController>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rocket")
        {
            isTrue = true;
            rocket.currentFuel = 100f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rocket")
        {
            isTrue = false;
        }
    }

    private void FixedUpdate()
    {
        if (isTrue)
        {
           pull();
          // pull2();
        }
       
    }

    private void pull()
    {
        float distance = Vector2.Distance(transform.position, rocket.rb.transform.position);
        dragForce = gravityForce * mass / distance;
        planetVector = Vector2.MoveTowards(transform.position, rocket.rb.transform.position, dragForce);
        rocket.rb.AddForce(planetVector, ForceMode2D.Force);
    }

    private void pull2()
    {
        Vector2 direction = transform.position - rocket.transform.position;
        float distance = direction.sqrMagnitude;

        float forceMagnitude = gravityForce * (mass * rocket.rb.mass) / distance;
        Vector2 force = direction.normalized * forceMagnitude;

        rocket.rb.AddForce(force, ForceMode2D.Force);
    }
}
