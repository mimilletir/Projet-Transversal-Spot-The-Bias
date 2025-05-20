using UnityEngine;

public class RandomCharacterGenerator : MonoBehaviour
{
    [Header("Heads")]
    public GameObject[] heads;

    [Header("Bodies")]
    public GameObject[] bodies;

    [Header("Legs")]
    public GameObject[] legs;

    public void GenerateRandomCharacter(Transform anchor)
    {
        // Ajouter chaque partie à un point d'ancrage
        InstantiateRandomPart(heads, anchor, "Head");
        InstantiateRandomPart(bodies, anchor, "Body");
        InstantiateRandomPart(legs, anchor, "Legs");
    }

    void InstantiateRandomPart(GameObject[] parts, Transform parent, string partName)
    {
        if (parts.Length == 0) return;

        int index = Random.Range(0, parts.Length);
        GameObject part = Instantiate(parts[index], parent);
        part.name = partName;
    }
}
