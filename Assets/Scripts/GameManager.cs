using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pipes;
    [SerializeField] private Vector3 pipeSpawnPosition;
    [SerializeField] private float spawnTime;
    [SerializeField] private float minYPosition;
    [SerializeField] private float maxYPosition;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject gameOverBanner;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private Rigidbody2D birdRb;
    private float spawnTimeSeconds;
    private bool canPlay = true;
    private int score = 0;
    private bool gameStarted = false;
    private float originalGravity = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimeSeconds = spawnTime;
        scoreText.text = "" + score;
        gameOverBanner.SetActive(false);
        restartButton.SetActive(false);
        if (birdRb != null)
        {
            originalGravity = birdRb.gravityScale;
            birdRb.gravityScale = 0f;  
            birdRb.linearVelocity = Vector2.zero;
        }
 
    }

    // Update is called once per frame
    void Update()
    {
               if (!canPlay) return;

        if (!gameStarted)
        {
            if (Pressed()) StartGame();
            return; 
        }

        spawnTimeSeconds -= Time.deltaTime; 
        if (spawnTimeSeconds < 0)
        {
            spawnTimeSeconds = spawnTime;
            float yOffset = Random.Range(minYPosition, maxYPosition);
            pipeSpawnPosition.y = yOffset;
            GameObject tmp = Instantiate(pipes, pipeSpawnPosition, Quaternion.identity);
            Pipes tmpPipe = tmp.GetComponent<Pipes>();
            tmpPipe.SetUp(this);
        }
    }

    private bool Pressed()
    {
        return Input.GetMouseButtonDown(0)
            || Input.GetKeyDown(KeyCode.Space)
            || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
    }
    
    private void StartGame()
    {
        gameStarted = true;
        spawnTimeSeconds = spawnTime;
        if (birdRb != null) birdRb.gravityScale = originalGravity; // מפעילים נפילה/פיזיקה
    }

    public bool IsGameStarted() => gameStarted;

    public bool getCanPlay()
    {
        return canPlay;
    }
    public void setCanPlay(bool canPlay)
    {
        this.canPlay = canPlay;
        if (!canPlay)
        {
            gameOverBanner.SetActive(true);
            restartButton.SetActive(true);
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "" + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
