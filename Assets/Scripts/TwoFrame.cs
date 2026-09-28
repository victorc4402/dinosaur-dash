using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TwoFrame : MonoBehaviour
{
    [Header("Frames")]
    [SerializeField] public Sprite frame1;
    [SerializeField] public Sprite frame2;

    [Header("Settings")]
    [Tooltip("Time in seconds between frame switches")]
    [SerializeField] private float frameRate = 0.5f;

    private SpriteRenderer spriteRenderer;
    private float timer;
    private bool showFirstFrame = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (frame1 == null || frame2 == null) return;

        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer -= frameRate;
            showFirstFrame = !showFirstFrame;
            spriteRenderer.sprite = showFirstFrame ? frame1 : frame2;
            Debug.Log("dfdffdfß");
        }
    }
}
