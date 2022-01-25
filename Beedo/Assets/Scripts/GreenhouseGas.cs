using System;
using UnityEngine;

public class GreenhouseGas : MonoBehaviour
{
    [SerializeField] private BallPool ballPool;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 direction;
    public bool gasActive;
    [SerializeField] private float speed = 5f;
    public static event Action gasLaugh;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        direction = transform.right;
        gasActive = false;
    }

    private void OnEnable()
    {
        Ball.ballHitGas += GasActivated;
    }

    private void FixedUpdate()
    {
        if (!gasActive)
        {
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<SideWall>(out SideWall s))
        {
            direction = -direction;
        }
    }

    private void GasActivated()
    {
        anim.SetTrigger("gasActive");
        gasActive = true;
        Invoke(nameof(ReemitRadiation), 0.25f);
    }

    private void ResetGas()
    {
        gasActive = false;
        anim.ResetTrigger("gasActive");
    }

    private void ReemitRadiation()
    {
        gasLaugh?.Invoke();
        foreach (Position p in Enum.GetValues(typeof(Position)))
        {
            Ball b = ballPool.Get();
            b.transform.position = transform.position;
            b.gameObject.SetActive(true);
            b.ServeGasBall(p);
        }
        Invoke(nameof(ResetGas), 0.5f);
    }

    private void OnDisable()
    {
        Ball.ballHitGas -= GasActivated;
    }
}
