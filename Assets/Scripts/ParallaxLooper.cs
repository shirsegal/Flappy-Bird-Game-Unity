using UnityEngine;

public class ParallaxLooper : MonoBehaviour
{
    public float speed = 1f;

    Transform[] tiles;
    float tileWidth;
    Camera cam;

    void Start()
    {
        cam = Camera.main;

        int n = transform.childCount;
        tiles = new Transform[n];
        for (int i = 0; i < n; i++)
            tiles[i] = transform.GetChild(i);

        if (n == 0) return;

        var sr = tiles[0].GetComponent<SpriteRenderer>();
        tileWidth = sr.bounds.size.x;

        System.Array.Sort(tiles, (a, b) => a.position.x.CompareTo(b.position.x));
    }

    void Update()
    {
        if (tiles == null || tiles.Length == 0) return;

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        float camLeft = cam.transform.position.x - cam.orthographicSize * cam.aspect;

        Transform first = tiles[0];
        Transform last  = tiles[tiles.Length - 1];

        if (first.position.x + tileWidth * 0.5f < camLeft)
        {
            float newX = last.position.x + tileWidth;
            first.position = new Vector3(newX, first.position.y, first.position.z);

            for (int i = 0; i < tiles.Length - 1; i++)
                tiles[i] = tiles[i + 1];
            tiles[tiles.Length - 1] = first;
        }
    }
}