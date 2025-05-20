using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public string objectName;
    public GameObject tuto;
    public GameObject popup;

    void OnMouseDown()
    {
        if (tuto.activeSelf || popup.activeSelf)
            return;
        Debug.Log("Tu as cliqué sur : " + objectName);
        GameManager.Instance.HandleInteraction(objectName);
    }
}