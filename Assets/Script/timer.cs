using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10f;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timer;
    public Button skipbutton;
    public GameObject finalscreen;

    private void Start()
    {
        if (skipbutton != null)
        {
            skipbutton.onClick.AddListener(Stopbutton);
        }

        if (finalscreen != null)
        {
            finalscreen.SetActive(false);
        }
    }

    public void StartGame()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerDisplay(timeRemaining);
                Debug.Log("Time has run out!");
                Stopbutton(); 
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(0, timeToDisplay);
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Stopbutton()
    {
        timeRemaining = 0;
        timerIsRunning = false;
        UpdateTimerDisplay(timeRemaining);
        Debug.Log("Time has run out!");
        ShowPopup();

        
        StartCoroutine(WaitBeforeSceneChange(3f)); 
    }

    public void ShowPopup()
    {
        if (finalscreen != null)
        {
            finalscreen.SetActive(true);
            Time.timeScale = 0f; 
        }
    }

    IEnumerator WaitBeforeSceneChange(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene("EndScene");
    }
}
