using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SituationFocus : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<GameObject> situations;

    [SerializeField] private CameraFocus cameraFocus;

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
        cameraFocus.Focus(situations[number].transform);
        number += 1;
    }
}
