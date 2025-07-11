using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    //script freezes the game and allows the player to control the game volume and the access the main menu or quit the game
    public bool paused;

    public GameObject pauseMenuUI;
    public GameObject scoreUI;

    public GameObject player;
    PlayerShooting playerShooting;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.SetActive(false);

            pauseMenuUI.SetActive(true);
            scoreUI.SetActive(false);

            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        player.SetActive(true);

        pauseMenuUI.SetActive(false);
        scoreUI.SetActive(true);

        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        player.SetActive(true);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
