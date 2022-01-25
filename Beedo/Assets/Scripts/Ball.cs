using UnityEngine;

public enum Position
{
    Left,
    Right,
    Middle
}

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private BallPool bp;

    //ball color
    [SerializeField] private Sprite[] ballSprites;
    private SpriteRenderer spriteRenderer;
    private int activeColor;

    //ball speed
    private Vector2[] velocities;

    public static event System.Action ballLost;
    public static event System.Action ballSaved;
    public static event System.Action ballBounced;
    public static event System.Action ballHitGas;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bp = GetComponentInParent<BallPool>();
        velocities = new Vector2[] { new Vector2(20f, 20f), new Vector2(12f, 12f), new Vector2(7.5f, 7.5f), new Vector2(5f, 5f) };
    }

    private void SetRandomColor()
    {
        int r = Random.Range(0, ballSprites.Length);
        spriteRenderer.sprite = ballSprites[r];
        activeColor = r;
    }

    private void SetColor(int c)
    {
        spriteRenderer.sprite = ballSprites[c];
        activeColor = c;
    }

    public void Serve()
    {
        SetRandomColor();
        rb.velocity = new Vector2(Random.Range(-2, 3), Random.Range(-5, -2));

        //testServe, move sun under gas and serve up
        //rb.velocity = new Vector2(0, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<SideWall>(out SideWall s))
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            ballBounced?.Invoke();
        }

        if (collision.TryGetComponent<TopWall>(out TopWall t))
        {
            ResetBall();
            ballSaved?.Invoke();
        }

        if (collision.TryGetComponent<BottomWall>(out BottomWall b))
        {
            ResetBall();
            ballLost?.Invoke();
        }

        if (collision.TryGetComponent<PlayerPaddle>(out PlayerPaddle p))
        {
            if (BallPaddleMatch(p.activePaddle) || activeColor == 4)
            {
                SetColor(p.activePaddle);
                Reflect(p.activePaddle, GetPaddleHitPosition(collision));
                ballBounced?.Invoke();
            }

            else
            {
                ResetBall();
                ballLost?.Invoke();
            }
        }

        if (collision.TryGetComponent<GreenhouseGas>(out GreenhouseGas g))
        {
            if (rb.velocity.y < 0)
            {
                return;
            }

            if (!g.gasActive)
            {
                ResetBall();
                ballHitGas?.Invoke();
            }
        }
    }

    private bool BallPaddleMatch(int paddleColor)
    {
        if (paddleColor == activeColor)
        {
            return true;
        }
        return false;
    }

    private Vector2 GetPaddleHitPosition(Collider2D collision)
    {
        //hit left third of paddle
        if (transform.position.x < collision.bounds.center.x - collision.bounds.size.x / 6)
        {
            return new Vector2(-1f, 1f);
        }

        //hit right third of paddle
        else if (transform.position.x > collision.bounds.center.x + collision.bounds.size.x / 6)
        {
            return new Vector2(1f, 1f);
        }

        //hit middle third of paddle
        return new Vector2(0f, 1f);
    }

    private void Reflect(int paddleColor, Vector2 reflectionDirection)
    {
        rb.velocity = velocities[paddleColor] * reflectionDirection;
    }

    public void ServeGasBall(Position p)
    {
        SetColor(4);

        switch (p)
        {
            case Position.Left:
                rb.velocity = new Vector2(1f, -1f);
                break;
            case Position.Middle:
                rb.velocity = new Vector2(0f, -1f);
                break;
            case Position.Right:
                rb.velocity = new Vector2(-1f, -1f);
                break;
            default:
                break;
        }

        rb.velocity *= Random.Range(2, 4);
    }

    public void ResetBall()
    {
        this.gameObject.SetActive(false);
        transform.position = transform.parent.transform.position;
        rb.velocity = Vector2.zero;
        bp.ReturnToPool(this);
    }
}
