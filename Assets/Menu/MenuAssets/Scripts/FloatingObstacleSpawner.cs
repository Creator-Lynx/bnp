using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FloatingObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawPrefabs;
    [SerializeField]
    float defaultTimeToSpawn = 10, randomFactorOfTime = 5f;
    [SerializeField]
    int percentSpawnChance = 25;
    void Start()
    {
        System.TimeSpan a = new System.TimeSpan();
        Random.InitState(a.Seconds);
        StartCoroutine(SpawnWaiter());
    }

    void SpawnVariants(int variant)
    {
        Debug.Log(variant);
        if (variant < 0 || variant >= spawPrefabs.Length)
            Debug.LogException(new System.Exception("Spawn func take an incorrect argument for choosing prefab from array."));
        if (spawPrefabs.Length == 0)
            Debug.LogException(new System.Exception("PrefabsArray of spawner is empty."));
        RandomizeFloatingPrefab(
        Instantiate(spawPrefabs[variant], transform.position, Quaternion.identity).GetComponent<ObstacleWaterSimulation>());
    }

    void RandomSpawn()
    {
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand < percentSpawnChance) SpawnVariants(0);
        else
        {
            SpawnVariants(UnityEngine.Random.Range(1, 4));
        }
    }

    void RandomizeFloatingPrefab(ObstacleWaterSimulation obst)
    {

    }

    IEnumerator SpawnWaiter()
    {
        RandomSpawn();
        yield return new WaitForSeconds(defaultTimeToSpawn + randomFactorOfTime * UnityEngine.Random.Range(-1, 1));
        StartCoroutine(SpawnWaiter());
    }
}
