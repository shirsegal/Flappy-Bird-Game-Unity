using UnityEngine;

public class Pipes : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minXPosition;
    [SerializeField] private GameManager gameManager;

    public void SetUp(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.getCanPlay())
        {
            return;
        }
        transform.Translate(Vector3.left * speed * Time.fixedDeltaTime);
        if (transform.position.x < minXPosition)
        {
            Destroy(gameObject); // Destroy the pipe if it goes off-screen
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        gameManager.IncreaseScore(); 
    }
}
