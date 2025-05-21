using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    public void MainMenu() => SceneManager.LoadScene("MainMenu");
}
