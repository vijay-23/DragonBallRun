using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody playerRb; // Player rigid body
    private BoxCollider playerBc; // Player box collider
    private Animator playerAnim;

    private AudioSource playerAudio;
    public AudioClip crashSound;
    public AudioClip dyingVoice;
    public AudioClip shenron;
    public AudioClip victorySong;
    public AudioClip coinCollected;
    public AudioClip slidingSound;

    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public float dragonballCount = 0;
    private SpawnManager spawnManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = GetComponent<Rigidbody>();
        playerBc= GetComponent<BoxCollider>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;                                                  // Gravity times the gravity we give it in unity
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();   // Refrencing the spawnmanager script so that we can call haswon and gameover
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up") && isOnGround && !spawnManagerScript.gameOver && !spawnManagerScript.hasWon) // If the player is on the ground and not is game over or has won yet and presses on up arrow key
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);   // Make the character jump
            isOnGround = false;
            playerAnim.Play("Jump");    // Play the jump animation
        }

        if (Input.GetKeyDown("down") && isOnGround && !spawnManagerScript.gameOver && !spawnManagerScript.hasWon) // If the player is on the ground and not is game over or has won yet and presses on down arrow key
        {
            playerBc.size = new Vector3(24, 30, 39);
            playerBc.center = new Vector3(0.6f, 20, 0.6f);
            isOnGround = true;
            playerAnim.Play("Running Slide");               // Play the jump animation
            playerAudio.PlayOneShot(slidingSound, 2.0f);    // Play the jump sound effect
        }
        if (Input.GetKeyUp ("down") && isOnGround && !spawnManagerScript.gameOver && !spawnManagerScript.hasWon) // If the player is on the ground and not is game over or has won yet and releases down arrow key
        {
            playerBc.size = new Vector3(24, 71, 39);
            playerBc.center = new Vector3(0.6f, 35, 0.6f);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))     // If player collides with obj that has Obstacle tag
        {
            spawnManagerScript.gameOver = true;             // Conclude Game over
            Debug.Log("Game Over!");
            playerAnim.SetBool("HasDied", true);            // Set boolean to true
            playerAnim.Play("Dying Backwards");             // Play Dying animation
            playerAudio.PlayOneShot(crashSound, 1.0f);      // Play crash sound effect
            playerAudio.PlayOneShot(dyingVoice, 1.0f);      // Play Dying voice sound effect
            spawnManagerScript.GameOverText();              // Show game over screen
        }

        if (collision.gameObject.CompareTag("Dragonball"))  // If player collides with obj that has Dragonball tag
        {
            Debug.Log("Dragonball collected");
            Destroy(collision.gameObject);                  // Make db dissapear
            dragonballCount++;                              // DB count gets one added to it(so +1)
            spawnManagerScript.UpdateScore(1);              // Update your score
            playerAudio.PlayOneShot(coinCollected, 1.0f);   // Play coincollect sound effect
        }
        if (dragonballCount == 7)                           // If you collected all 7 db
        {
            spawnManagerScript.hasWon = true;               // Conclude win
            Debug.Log("You won!");
            playerAnim.SetBool("HasCompleted", true);       // Set boolean to true
            playerAnim.Play("Hip Hop Dancing");             // Play dance animation
            playerAudio.PlayOneShot(shenron, 1.0f);         // Play Shenron sound effect
            spawnManagerScript.WinText();                   // Show Win screen
            playerAudio.PlayOneShot(victorySong, 1.0f);     // Play victory song
        }
    }

    private void OnCollisionEnter(Collision collision)
    {   // If player is on the ground set boolean on true
        isOnGround = true;

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}

    
