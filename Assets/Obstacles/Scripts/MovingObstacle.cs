using UnityEngine;

public class MovingObstacle : SelfSaver
{
    [SerializeField]
    Transform moving, point0, point1;
    [SerializeField]
    float timeToNext = 2f;
    float timer = 0f, k = 1;
    Animation animated;
    public override void Awake()
    {
        animated = GetComponentInChildren<Animation>();
        timer = UnityEngine.Random.Range(0f, timeToNext);
        base.Awake();
    }
    void Update()
    {
        timer += Time.deltaTime * k;
        float t = Mathf.SmoothStep(0, 1, timer / timeToNext);
        moving.position = Vector3.Lerp(point0.position, point1.position, t);
        if (k > 0 && timer > timeToNext) k = -1;
        if (k < 0 && timer < 0f) k = 1;
    }
    float savedTimer, savedK;
    protected override void Save()
    {
        savedTimer = timer;
        savedK = k;
        animated.Sample();

    }
    protected override void Load()
    {
        timer = savedTimer;
        k = savedK;
    }
}
