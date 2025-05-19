using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UiAnimation2 : MonoBehaviour

{
    [SerializeField] private float m_duration = 1f;
    [SerializeField] private Color m_startColor; // On rajoute la couleur en parametre
    [SerializeField] private Color m_endColor; // On rajoute la couleur en parametre

    private Image m_image;
    private float m_time = 0;



    void Awake()

    {

        m_image = GetComponent<Image>();
        if (m_image == null)
        {

            Debug.LogError($"<color=orange> Pas de composant Image sur {this.gameObject.name} </color>");

            enabled = false;

            return;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (m_image != null && m_image.enabled)
        {

            float ratio = m_time / m_duration;
            m_image.color = Color.Lerp(m_startColor, m_endColor, ratio);

            Debug.Log($"<color=orange> {m_time} </color>");

            m_time += Time.unscaledDeltaTime; // deltaTime ca marche aussi (ca dï¿½pend du contexte)

            // On Loop l'animation
                if (m_time > m_duration) 
                     m_time = -0.5f;

        }

    }
}