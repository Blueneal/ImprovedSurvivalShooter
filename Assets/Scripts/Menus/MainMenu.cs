using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //controls the main menu, allows the player to start the game, open the control page and close the game

    public Image controlMenu;
    public TextMeshProUGUI controlText;
    public Button closeControlButton;

    public void OnButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void closeGame()
    {
        Application.Quit();
    }

    public void OpenControl()
    {
        controlMenu.gameObject.SetActive(true);
        controlText.gameObject.SetActive(true);
        closeControlButton.gameObject.SetActive(true);
    }
    public void CloseControl()
    {
        controlMenu.gameObject.SetActive(false);
        controlText.gameObject.SetActive(false);
        closeControlButton.gameObject.SetActive(false);
    }
}
