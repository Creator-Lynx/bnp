using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FloatingObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawPrefabs;
    [SerializeField]
    float defaultTimeToSpawn = 10, randomFactorOfTime = 5f, randomAmplitudeOfPrefab = 0.2f;
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
        if (spawPrefabs.Length == 0)
            Debug.LogException(new System.Exception("PrefabsArray of spawner is empty."));
        if (variant < 0 || variant >= spawPrefabs.Length)
            Debug.LogException(new System.Exception("Spawn func take an incorrect argument for choosing prefab from array."));
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
        float randFactor = UnityEngine.Random.Range(1 - randomAmplitudeOfPrefab, 1 + randomAmplitudeOfPrefab);
        float randFactor2 = UnityEngine.Random.Range(1 - randomAmplitudeOfPrefab, 1 + randomAmplitudeOfPrefab);
        //randomize rotation and scale
        Transform trans = obst.transform.GetChild(0).transform;
        trans.localScale *= randFactor;
        trans.localRotation *= Quaternion.Euler(0f, 180 * UnityEngine.Random.Range(0, 2), 0f);
        trans.localRotation *= Quaternion.Euler(0f, (randFactor - 1) * 45, (randFactor2 - 1) * 90);
        //randomize floating
        obst.densityAmplitude *= randFactor;
        obst.rig_drag *= randFactor;
        obst.rig_angularDrag *= randFactor;
        obst.floatingAngleAmplitude *= randFactor;
        obst.floatingPeriod *= randFactor;
        //randomize flowing
        float invertRand = (randFactor - 1) * (-1) + 1;
        obst.ToFlowForceValue *= invertRand;
        obst.flowForce *= invertRand;
    }

    IEnumerator SpawnWaiter()
    {
        RandomSpawn();
        yield return new WaitForSeconds(defaultTimeToSpawn + randomFactorOfTime * UnityEngine.Random.Range(-1, 1));
        StartCoroutine(SpawnWaiter());
    }
}
