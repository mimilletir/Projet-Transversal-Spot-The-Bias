using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SituationFocus : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private List<GameObject> situations;
    [SerializeField] private TextMeshProUGUI textSituation;
    [SerializeField] private TextMeshProUGUI textSolution;


    private List<bool> founds = new List<bool>();
    private int number = 0;

    private void Start()
    {
        foreach (PopupData popup in gameManager.popups)
        {
            founds.Add(false);
        }
    }

    public void SituationFound(int number)
    {
        founds[number - 1] = true;
    }

    public void FocusScene()
    {
        cameraController.StartFocus(true, situations[number].transform);
        textSituation.text = gameManager.popups[number].popupText;
        textSolution.text = gameManager.popups[number].popupText;
        number += 1;
        if (number >= gameManager.popups.Count)
            number = 0;
    }
}
