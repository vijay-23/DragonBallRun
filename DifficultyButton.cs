using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    public float difficulty;    // Here we can put in on unity how difficult we want it to be
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();    // This is our button
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();    // Refrencing the spawnmanager script so that we can call startGame funtion and fill in the difficulty
        button.onClick.AddListener(SetDifficulty);  // If you click on one of the buttons the difficulty will be what the difficulty we've assigned in unity
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficulty()
    {
        Debug.Log(gameObject.name + " was clicked");    // To check if the button does something
        spawnManager.StartGame(difficulty); // Call the startGame funtion from spawnmanager and fill in the number of difficulty we filled in on unity
    }
}
