using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float maxYPosition;
    [SerializeField] private float flyAngle;
    [SerializeField] private GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            body.linearVelocity = Vector2.zero; // Reset velocity on click
            body.AddForce(Vector2.up * speed);
        }
    }

    void FixedUpdate()
    {
        if (body.linearVelocity.y > maxVelocity)
        {
            body.linearVelocity = new Vector2(0, maxVelocity);
        }
        else if (body.linearVelocity.y < -maxVelocity)
        {
            body.linearVelocity = new Vector2(0, -maxVelocity);
        }
        if (transform.position.y > maxYPosition)
        {
            transform.position = new Vector3(0, maxYPosition, 0);
        }
        if (body.linearVelocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, flyAngle);
        }
        else if (body.linearVelocity.y < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -flyAngle);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        gameManager.setCanPlay(false); // Stop the game when a collision occurs
        body.constraints = RigidbodyConstraints2D.FreezeAll; // Freeze the bird's movement
    }
}
