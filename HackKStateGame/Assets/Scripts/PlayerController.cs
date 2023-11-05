using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    GameManager gm;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;
    private Animator anim;

    void Start ()
    {
        gm = FindObjectOfType<GameManager>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        if (body.velocity.magnitude > 0) {
            anim.SetBool("walking", true);
        } else {
            anim.SetBool("walking", false);
        }

        UpdateCharacterRotation();
    }

    void FixedUpdate()
    {
        move();
    }

    private void move() {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        } 

        body.velocity = new Vector2(horizontal, vertical).normalized * runSpeed;
    }

    void UpdateCharacterRotation()
    {
        if (horizontal != 0 || vertical != 0)
        {
            // Calculate the angle in radians between the movement direction and the character's forward direction
            float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;

            // Set the character's rotation based on the calculated angle
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }
    }
}
