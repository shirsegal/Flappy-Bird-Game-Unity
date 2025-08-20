using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteCycler : MonoBehaviour
{
    [Header("Frames (order: normal → down → up)")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite upSprite;

    [Header("Timing")]
    [Tooltip("Time (seconds) between sprite changes")]
    [Min(0.01f)][SerializeField] private float secondsPerFrame = 0.2f;

    private SpriteRenderer sr;
    private float timer;
    private int index; // 0=normal, 1=down, 2=up

    // Optional: expose what you're currently showing
    public Sprite CurrentSprite => sr ? sr.sprite : null;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (normalSprite == null || downSprite == null || upSprite == null)
        {
            Debug.LogError("SpriteCycler: Assign all three sprites (normal, down, up). Disabling.");
            enabled = false;
            return;
        }
        index = 0;
        sr.sprite = normalSprite;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < secondsPerFrame) return;
        timer -= secondsPerFrame;

        // advance 0→1→2→0...
        index = (index + 1) % 3;

        Sprite next =
            (index == 0) ? normalSprite :
            (index == 1) ? downSprite :
                           upSprite;

        if (sr.sprite != next)
            sr.sprite = next;    // SpriteRenderer now shows the current sprite
    }
}
