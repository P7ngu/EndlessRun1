using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Want more control? Call methods on rigidbody and don't use Transform!
    private Rigidbody playerRb;

    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;

    private Animator playerAnimator;

    public ParticleSystem explosionParticles;
    public ParticleSystem dirtParticles;

   

    // Start is called before the first frame update
    void Start()
    {
       
        playerRb = GetComponent<Rigidbody>();

        playerAnimator = GetComponent<Animator>();

        //playerRb.AddForce(Vector3.up * 1000);
        //This makes the player jump, and then fall because of gravity ^

        Physics.gravity *= gravityModifier;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnimator.SetTrigger("Jump_trig");
            dirtParticles.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { 
            isOnGround = true;
            dirtParticles.Play();
        } else if (collision.gameObject.CompareTag("Obstacle")) {
            gameOver = true;
            Debug.Log("Game Over :(");
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
            explosionParticles.Play();
            dirtParticles.Stop();
        }
    }
}

