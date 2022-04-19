using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    private Vector3 spawnPos = new Vector3(-150, 10, 24);       // Position we want the obstacles to spawn at
    private Vector3 spawnPosDb = new Vector3(-150, 15, 28);     // Position we want the dragon balls to spawn at

    public bool hasWon;
    public bool gameOver;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winText;
    public Button restartButton;
    public float startDelay = 1;
    public float repeatRate = 1;
    public bool isGameActive;
    private PlayerController playerControllerScript;
    public GameObject[] obstaclePrefabs;
    public GameObject[] dragonBallPrefabs;
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Goku").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame(float difficulty)
    {
        isGameActive = true;        // Will show that the game is active and ready to play
        score = 0;                  // Before you collected DB score will be 0
        repeatRate /= difficulty;   // What's left will be divided by what's on the right
        UpdateScore(0);             // Call updatescore function
        InvokeRepeating("SpawnObject", startDelay, repeatRate);     // Call SpawnObject on repeat with the same startDelay and repeatRate values
        titleScreen.gameObject.SetActive(false);                    // Set the title screen on off so we wont see it during playing
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;                    // What's left will be added by what's on the right (0 + your current score)
        scoreText.text = "Score: " + score;     // Text and and your current score together
    }

    public void GameOverText()
    {
        restartButton.gameObject.SetActive(true);   // Show restart button when you lose the game
        gameOverText.gameObject.SetActive(true);    // Show text with game over when you lose the game
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // This will start the game again
    }

    public void WinText()
    {
        restartButton.gameObject.SetActive(true);   // Show restart button when you win the game
        winText.gameObject.SetActive(true);         // Show text with You win when you win the game
    }

    void SpawnObject()
    {
        if (gameOver == false && hasWon == false)   // If you didnt win or lost yet
        {
            if (Random.Range(0, 8) > 6)             // Pick a random number between 0 and 8. If that number is smaller than 6
            {
                int dragonballIndex = Random.Range(0, 7);       // Pick a random dragon ball from array
                Instantiate(dragonBallPrefabs[dragonballIndex], spawnPosDb, Quaternion.identity); // Spawn that dragonball on the position we assigned to it.
            }
            else
            {
                int obstacleIndex = Random.Range(0, 2);     // Pick random obstacle from the array
                Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, Quaternion.identity);   //// Spawn that obtacle on the position we assigned to it.
            }
        }
    }    
}
