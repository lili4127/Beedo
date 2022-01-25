using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Sprite[] paddleSprites;
    private SpriteRenderer spriteRenderer;
    public int activePaddle { get; private set; }
    private ParticleSystem pSystem;
    public static event System.Action paddleChange;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = paddleSprites[0];
        activePaddle = 0;
        pSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void ChangePaddle(int i)
    {
        if (activePaddle != i)
        {
            pSystem.Play();
            spriteRenderer.sprite = paddleSprites[i];
            activePaddle = i;
            paddleChange?.Invoke();
}
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.timerGoing || gameManager.tutorial)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ChangePaddle(0);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                ChangePaddle(1);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                ChangePaddle(2);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                ChangePaddle(3);
            }
        }
    }
}
