using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public string objectName;
    public GameObject tuto;
    public GameObject popup;
    public GameObject final;

    private bool clickable = false;
    private bool notBeenClick = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Clic droit
        {
            if (tuto.activeSelf || popup.activeSelf || final.activeSelf)
                    return;

            if (clickable && notBeenClick)
            {
                GameManager.Instance.HandleInteraction(objectName);
                notBeenClick = false;
            }
        }
    }

    private void OnMouseEnter()
    {
        clickable = true;
    }

    private void OnMouseExit()
    {
        clickable = false;
    }
}