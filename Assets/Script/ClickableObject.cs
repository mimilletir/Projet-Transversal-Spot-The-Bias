using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public string objectName;
    public GameObject tuto;
    public GameObject popup;
    public GameObject final;

    void OnMouseDown()
    {
        if (tuto.activeSelf || popup.activeSelf || final.activeSelf)
            return;
        Debug.Log("Tu as cliqué sur : " + objectName);
        GameManager.Instance.HandleInteraction(objectName);
    }
}