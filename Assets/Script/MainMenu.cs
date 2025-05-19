using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsOverlay;
    public GameObject mainMenuOverlay;

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ShowCredits()
    {
        creditsOverlay.SetActive(true);
        mainMenuOverlay.SetActive(false);
    }

    public void HideCredits()
    {
        creditsOverlay.SetActive(false);
        mainMenuOverlay.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
