using UnityEngine;
using System.Collections.Generic;

public class RandomSpawn : MonoBehaviour
{
    public GameObject peoplePrefab;
    public int maxPeople = 100;
    public float spawnInterval = 0.01f;
    public Vector2 spawnArea;
    public float minDistance = 1f;
    public List<string> names = new List<string>();

    private int _currentPeople = 0;
    private List<Vector2> spawnedPositions = new List<Vector2>();

    void Start()
    {
        InvokeRepeating(nameof(SpawnPeople), spawnInterval, spawnInterval);
    }

    void SpawnPeople()
    {
        if (_currentPeople >= maxPeople) return;

        Vector2 spawnPosition = Vector2.zero;
        bool validPosition = false;
        int attempts = 0;

        while (!validPosition && attempts < 20)
        {
            spawnPosition = GetRandomPosition();
            validPosition = IsPositionValid(spawnPosition);
            attempts++;
        }

        if (!validPosition) return;

        spawnedPositions.Add(spawnPosition);
        GameObject newPeople = Instantiate(peoplePrefab, spawnPosition, Quaternion.identity);

        string name = GetRandomName();
        newPeople.GetComponent<ClickableObject>().objectName = name;

        Color color = GetColor(name);
        newPeople.GetComponent<SpriteRenderer>().color = color;

        _currentPeople++;
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(transform.position.x - (spawnArea.x / 2), transform.position.x + (spawnArea.x / 2));
        float y = Random.Range(transform.position.y - (spawnArea.y / 2), transform.position.y + (spawnArea.y / 2));
        return new Vector2(x, y);
    }

    bool IsPositionValid(Vector2 newPos)
    {
        foreach (Vector2 pos in spawnedPositions)
        {
            if (Vector2.Distance(pos, newPos) < minDistance)
                return false;
        }
        return true;
    }

    string GetRandomName()
    {
        int id = Random.Range(0, names.Count);
        return names[id];
    }

    Color GetColor(string name)
    {
        switch (name.ToLower())
        {
            case "truc":
                return Color.blue;
            case "chose":
                return Color.red;
            case "machin":
                return Color.green;
            default:
                return Color.white;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}

