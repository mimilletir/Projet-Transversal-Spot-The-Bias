using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsOverlay;

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ShowCredits()
    {
        creditsOverlay.SetActive(true);
    }

    public void HideCredits()
    {
        creditsOverlay.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
