using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    public float startingForce;
    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    private float lowEnough;
    private float highEnough;
    private float upwardBounce;
    private float loweredBounce;
    public AudioClip bounceSound;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb=GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * startingForce,ForceMode.Impulse);
        floatForce=84.0f;
        gameOver=false;
        startingForce=5.0f;
        lowEnough=0.5f;
        highEnough=14.5f;
        upwardBounce=1.2f;
        loweredBounce=0.9f;

        
        

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }
        // while player is highenough
        if (playerRb.transform.position.y>highEnough){
            
            playerRb.AddForce(Vector3.down * loweredBounce, ForceMode.Impulse);

        } 
        // while game is not over and player is extremely low
        if (!gameOver){
            if (playerRb.transform.position.y<lowEnough){
            playerAudio.PlayOneShot(bounceSound, 1.0f);
            playerRb.AddForce(Vector3.up * upwardBounce, ForceMode.Impulse);
        
    }
        }}

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb") )
        {
            
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 
       
            
        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {   fireworksParticle.transform.position=playerRb.transform.position;
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
        
        
    }

}
