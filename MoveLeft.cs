using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;   // How fast the obstacles move
    private SpawnManager spawnManagerScript;
    private float leftBound = 0;    // Left side of the game view

    // Start is called before the first frame update
    void Start()
    {
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();  // Refrencing the spawnmanager script so that we can call gameOver and hasWon
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnManagerScript.gameOver == false && spawnManagerScript.hasWon == false)     // If you didnt win or lost yet
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);    // Move the obj that has this script attached to it with a speed of 30
        }
        

        if (transform.position.x > leftBound && gameObject.CompareTag("Obstacle") && spawnManagerScript.hasWon == false)    // If obj went past left side of the screen(game view)
        {
            Destroy(gameObject);    // Destroy it
        }
    }
}
