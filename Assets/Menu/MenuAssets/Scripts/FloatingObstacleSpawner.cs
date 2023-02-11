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
    void Start()
    {
        System.TimeSpan a = new System.TimeSpan();
        Random.InitState(a.Seconds);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnVariants(int variant)
    {
        if (variant < 0 || variant >= spawPrefabs.Length)
        {
            Debug.LogException(new System.Exception("Spawn func take an incorrect argument for choosing prefab from array"));
        }
        Instantiate(spawPrefabs[variant], transform.position, Quaternion.identity);
    }
    void RandomSpawn()
    {

    }
    IEnumerator SpanwWaiter()
    {
        RandomSpawn();
        yield return new WaitForSeconds(defaultTimeToSpawn + randomFactorOfTime * UnityEngine.Random.Range(-1, 1));
    }
}
