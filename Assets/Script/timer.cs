using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10f;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timer;
    public Button skipbutton;
    public GameObject finalscreen;
    [SerializeField] private GameObject canvasPopup;
    [SerializeField] private GameObject canvasFinal;
    [SerializeField] private CameraController cameraController;

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
        }
    }

    IEnumerator WaitBeforeSceneChange(float delay)
    {
        cameraController.canDrag = false;
        cameraController.canZoom = false;
        cameraController._camera.orthographicSize = cameraController.maxZoom;
        yield return new WaitForSecondsRealtime(delay);

        Time.timeScale = 1f;
        canvasPopup.SetActive(false);
        finalscreen.SetActive(false);
        canvasFinal.SetActive(true);

        StartCoroutine(FadeOutPeople(2f));
    }

    IEnumerator FadeOutPeople(float duration)
    {
        GameObject[] people = GameObject.FindGameObjectsWithTag("People");
        float elapsed = 0f;

        List<SpriteRenderer[]> allRenderers = new List<SpriteRenderer[]>();
        foreach (GameObject person in people)
        {
            SpriteRenderer[] renderers = person.GetComponentsInChildren<SpriteRenderer>();
            allRenderers.Add(renderers);
        }

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            foreach (SpriteRenderer[] renderers in allRenderers)
            {
                foreach (SpriteRenderer sr in renderers)
                {
                    if (sr != null)
                    {
                        Color color = sr.color;
                        color.a = alpha;
                        sr.color = color;
                    }
                }  
            }

            elapsed += Time.unscaledDeltaTime;
            yield return null;
            
        }
    }
}
