using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField]
    Transform moving, point0, point1;
    [SerializeField]
    float timeToNext = 2f;
    float timer = 0f, k = 1;
    void Start()
    {
        timer = UnityEngine.Random.Range(0f, timeToNext);
    }
    void FixedUpdate()
    {
        timer += Time.deltaTime * k;
        float t = Mathf.SmoothStep(0, 1, timer / timeToNext);
        moving.position = Vector3.Lerp(point0.position, point1.position, t);
        if (k > 0 && timer > timeToNext) k = -1;
        if (k < 0 && timer < 0f) k = 1;
    }
}
