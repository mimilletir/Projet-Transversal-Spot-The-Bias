using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class PopupData
{
    public string objectName;
    [TextArea(2, 5)] public string title;
    [TextArea(2, 5)] public string popupText;
    [TextArea(2, 5)] public string popupTextsolu;
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Popup UI Elements")]
    public GameObject popup;
    public TextMeshProUGUI popupText;
    public TextMeshProUGUI popupTextSolution;
    public Button closeButton;

    [Header("Popups configurables")]
    public List<PopupData> popups = new List<PopupData>();

    [Header("Tuto")]
    public GameObject tuto;
    public Timer timer;

    [Header("Final")]
    public GameObject final;

    [Header("TextMemory")]
    public TextMemory textMemory;

    private Dictionary<string, PopupData> popupDictionary;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        if (closeButton != null)
            closeButton.onClick.AddListener(ClosePopup);

        if (popup != null)
            popup.SetActive(false);

        popupDictionary = new Dictionary<string, PopupData>();
        foreach (var data in popups)
        {
            if (!popupDictionary.ContainsKey(data.objectName))
                popupDictionary.Add(data.objectName, data);
        }

        if (tuto != null)
        {
            tuto.SetActive(true);
            timer.timerIsRunning = false;
        }

        if (final != null)
            final.SetActive(false);

    }

    public void HandleInteraction(string objectName)
    {
        if (popupDictionary.ContainsKey(objectName))
        {
            PopupData data = popupDictionary[objectName];
            ShowPopup(data.popupText, data.popupTextsolu);
            textMemory.AddText(popupDictionary[objectName].title);
        }
        else
        {
            UIManager.Instance.ShowMessage("Ce n'est rien d'important");
        }
    }

    public void ShowPopup(string message, string solution)
    {
        if (popup != null && popupText != null)
        {
            popupText.text = message;
            if (popupTextSolution != null)
            {
                popupTextSolution.text = solution;
            }
            popup.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ClosePopup()
    {
        if (popup != null)
        {
            popup.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}