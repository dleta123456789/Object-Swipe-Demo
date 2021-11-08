using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    [Header("Difficulty Settings")]
    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetDifficulty()
    {
        Debug.Log(gameObject.name + " was called");
        gameManager.StartGame(difficulty);
    }

}
