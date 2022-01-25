using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private float movementInput;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        movementInput = Input.GetAxis("Horizontal");

        if (movementInput != 0)
        {
            anim.SetBool("isMoving", true);
        }

        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void FixedUpdate()
    {
        if (movementInput != 0)
        {
            rb.MovePosition(transform.position + transform.right * movementInput * speed * Time.fixedDeltaTime);
        }
    }
}
