using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pacman;

    public GameObject leftWarpNode;
    public GameObject rightWarpNode;

    public AudioSource siren;
    public AudioSource munch1;
    public AudioSource munch2;
    public int currentMunch = 0;

    public int score;
    public Text scoreText;

    public GameObject gameOverPanel;
    public GameObject startMenu;

    public GameObject winPanel;

    public int totalPellets;
    public int collectedPellets = 0;
    void Awake()
    {
        score = 0;
        collectedPellets = 0;

        NodeController[] allNodes = FindObjectsByType<NodeController>(FindObjectsSortMode.None);

        totalPellets = 0;

        for (int i = 0; i < allNodes.Length; i++)
        {
            Transform pellet = allNodes[i].transform.Find("Pellet");

            if (pellet != null)
            {
                totalPellets++;
            }
        }

        if (startMenu != null)
        {
            startMenu.SetActive(true);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text ="Score: " + score.ToString();
    }


    public void CollectedPellet(NodeController nodeController)
    {
        if (currentMunch == 0)
        {
            munch1.Play();
            currentMunch = 1;
        }
        else
        {
            munch2.Play();
            currentMunch = 0;
        }

        AddScore(10);

        collectedPellets++;

        if (collectedPellets >= totalPellets)
        {
            WinGame();
        }
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1f;

        if (siren != null)
        {
            siren.Play();
        }
    }

    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        if (siren != null)
        {
            siren.Stop();
        }
        Time.timeScale = 0f;
    }

    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void WinGame()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        if (siren != null)
        {
            siren.Stop();
        }

        Time.timeScale = 0f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
