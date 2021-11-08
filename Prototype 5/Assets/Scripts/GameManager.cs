using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Spawning")]
    public float spawnRate;
    public List<GameObject> targets;

    [Header("TextMesh Score Settings")]
    public TextMeshProUGUI scoreText;
    public int score;

    [Header("TextMesh Lives Settings")]
    public TextMeshProUGUI livesCounter;
    public int lives;

    [Header("TextMesh Game Over Settings")]
    public TextMeshProUGUI gameOverText;

    [Header("Pause  Settings")]
    public GameObject pauseScreen;
    public bool pause;
    public KeyCode pauseKey;


    [Header("Audio")]
    private AudioSource audioSource;

    [Header("TextMesh Lives Settings")]
    public Slider audioSlider;

    [Header ("Game State")]
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            ChangePauseStatus();
        }
    }
    IEnumerator Spawntarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
    }
    public void UpdateScore(int addScore)
    {
        if(isGameActive)
        {
            score += addScore;
            scoreText.text = "Score: " + score;
        }
    }
    public void GameOver()
    {
        isGameActive = false;
        Debug.Log("Testing");
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        StopMusic();
    }
    public bool GameState()
    {
        return isGameActive;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(Spawntarget());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        livesCounter.text =lives.ToString();
    }
    public void RemoveLife()
    {
        if(isGameActive == true)
        {
            --lives;
            //livesText.text = "Lives: " + lives;
            livesCounter.text = lives.ToString();
            if (lives <= 0)
            {
                GameOver();
            }
        }
        
    }
    private void StopMusic()
    {
        audioSource.Stop();
    }
    private void ChangePauseStatus()
    {
        if (pause == false && isGameActive)
        {
            pause = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            audioSource.Pause();

        }
        else if(pause == true && isGameActive)
        {
            pause = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            audioSource.UnPause();
        }
               

     }
}
