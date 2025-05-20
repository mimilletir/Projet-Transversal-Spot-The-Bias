using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public string objectName;

    void OnMouseDown()
    {
        Debug.Log("Tu as cliqué sur : " + objectName);
        GameManager.Instance.HandleInteraction(objectName);
    }
}



