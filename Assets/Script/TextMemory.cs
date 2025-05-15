using TMPro;
using UnityEngine;

public class TextMemory : MonoBehaviour
{
    private TMP_Text m_Text;

    private void Start()
    {
        m_Text = GetComponent<TMP_Text>();
    }

    public void AddText(string text)
    {
        m_Text.text += text + "\n";
    }
}
