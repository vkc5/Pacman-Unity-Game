using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    MovementController movementController;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        movementController = GetComponent<MovementController>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetBool("Moving", true);
        if (Input.GetKeyDown(KeyCode.W))
        {
            movementController.SetDirection("up");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            movementController.SetDirection("down");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            movementController.SetDirection("left");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            movementController.SetDirection("rigth");
        }

        bool flipX = false;
        bool flipY = false;

        if (movementController.lastDirection == "up")
        {
            animator.SetInteger("Direction", 1);
        }
        else if (movementController.lastDirection == "down")
        {
            animator.SetInteger("Direction", 1);
            flipY = true;
        }
        else if (movementController.lastDirection == "left")
        {
            animator.SetInteger("Direction", 0);
        }
        else if (movementController.lastDirection == "rigth")
        {
            animator.SetInteger("Direction", 0);
            flipX = true;
        }


        spriteRenderer.flipX = flipX;
        spriteRenderer.flipY = flipY;

    }
}
