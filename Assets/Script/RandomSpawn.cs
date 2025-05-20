using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class RandomSpawn : MonoBehaviour
{
    public GameObject peoplePrefab;
    public int maxPeople = 100;
    public float spawnInterval = 0.01f;
    public Vector2 spawnArea;
    public float minDistance = 1f;

    [SerializeField] private RandomCharacterGenerator rndChaGen;

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
        rndChaGen.GenerateRandomCharacter(newPeople.transform);
        newPeople.GetComponent<SortingGroup>().sortingOrder = (int)(-newPeople.transform.position.y * 1000);

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}

