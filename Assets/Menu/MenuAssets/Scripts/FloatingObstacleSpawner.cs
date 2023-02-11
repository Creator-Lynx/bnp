using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FloatingObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawPrefabs;
    void Start()
    {

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
    IEnumerator SpanwWaiter()
    {
        yield return new WaitForSeconds(1);
    }
}
