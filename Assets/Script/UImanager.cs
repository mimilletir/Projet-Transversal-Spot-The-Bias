using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text feedbackText;

    public static UIManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string message)
    {
        feedbackText.text = message;
    }
}
