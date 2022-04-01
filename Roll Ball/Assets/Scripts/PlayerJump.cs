using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rb;

    private float velocity;
    private bool isGrounded = true;
    private bool jumping = false;
    private bool canJump = false;
    private bool canDoubleJump = true;
    public float groundDistance = 0.5f;
    public float jumpVelocity = 10.0f;
    private float gravity = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Credit https://u3ds.blogspot.com/2021/10/double-jump-rigidbody-unity-game-engine.html
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDistance);
    }
 
    void Update()
    {
        if(jumping)
        {
            if(IsGrounded())
            {
                rb.velocity = Vector3.up * jumpVelocity;
                canDoubleJump = true;
            }
            else if(canDoubleJump)
            {
                rb.velocity = Vector3.up * jumpVelocity;
                canDoubleJump = false;
            }
            jumping = false;
        }
    }

    void OnJump(){
        jumping = true;

    }

    // Credit https://answers.unity.com/questions/149790/checking-if-grounded-on-rigidbody.html
    void OnCollisionStay (Collision collisionInfo)
    {
        isGrounded = true;
        jumping = false;
        velocity = 0.0f;
        canDoubleJump = true;
        Debug.Log("hit ground");
    }
    
    void OnCollisionExit (Collision collisionInfo)
    {
        isGrounded = false;
    }

    private void OnTriggerStay(Collider other)
    {
      if(other.gameObject.CompareTag("Ground")){
        isGrounded = true;
        jumping = false;
        velocity = 0.0f;
        canDoubleJump = true;
      }
      
    }
}
