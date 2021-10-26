using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;

    private Rigidbody2D playerRigidbody;
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        if (playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
    }
    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, playerRigidbody.velocity.y);
    }

    private void Jump()
    {
        playerRigidbody.velocity = new Vector2(0, jumpPower);
    }
    private bool IsGrounded()
    {
        RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, Vector2.down, .6f, 1 << 3);
        return groundCheck.collider != null && groundCheck.collider.CompareTag("Ground");
    }
    private int WhichWallHeld()
    {
        RaycastHit2D leftCheck = Physics2D.Raycast(transform.position, Vector2.left, .6f, 1 << 3);
        if (leftCheck.collider != null && leftCheck.collider.CompareTag("Ground"))
        {
            return -1;
        }
        RaycastHit2D rightCheck = Physics2D.Raycast(transform.position, Vector2.right, .6f, 1 << 3);
        if (rightCheck.collider != null && rightCheck.collider.CompareTag("Ground"))
        {
            return 1;
        }
        return 0;
    }


    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Debug.DrawRay(transform.position, Vector2.down*.6f, Color.green);
        Debug.DrawRay(transform.position, Vector2.right * .6f, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * .6f, Color.red);
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                Jump();
            }
            else
            {
                int wall = WhichWallHeld();
                if (wall == -1)
                {
                    playerRigidbody.velocity = new Vector2(-playerSpeed, jumpPower);
                }
                else if (wall == 1)
                {
                    playerRigidbody.velocity = new Vector2(playerSpeed, jumpPower);
                }
            }
        }
    }

}
