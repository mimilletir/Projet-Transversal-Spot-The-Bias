using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class PopupData
{
    public string objectName;
    [TextArea(2, 5)] public string popupText;
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Popup UI Elements")]
    public GameObject popup;
    public TextMeshProUGUI popupText;
    public Button closeButton;

    [Header("Popups configurables")]
    public List<PopupData> popups = new List<PopupData>();

    private Dictionary<string, string> popupDictionary;

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

        popupDictionary = new Dictionary<string, string>();
        foreach (var data in popups)
        {
            if (!popupDictionary.ContainsKey(data.objectName))
                popupDictionary.Add(data.objectName, data.popupText);
        }
    }

    public void HandleInteraction(string objectName)
    {
        if (popupDictionary.ContainsKey(objectName))
        {
            ShowPopup(popupDictionary[objectName]);
        }
        else
        {
            UIManager.Instance.ShowMessage("Ce n'est rien d'important");
        }
    }

    public void ShowPopup(string message)
    {
        if (popup != null && popupText != null)
        {
            popupText.text = message;
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

