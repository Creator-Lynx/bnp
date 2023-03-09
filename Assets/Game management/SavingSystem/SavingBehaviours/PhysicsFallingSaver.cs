using UnityEngine;

public class PhysicsFallingSaver : SelfSaver
{
    Rigidbody[] rigidbodies;
    public override void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        rigStates = new rigState[rigidbodies.Length];
        base.Awake();
    }
    struct rigState
    {
        public Vector3 position;
        public Quaternion rotation;
        public bool isKinematic;
        public Vector3 velocity;
    }
    rigState[] rigStates;
    protected override void Load()
    {
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].transform.position = rigStates[i].position;
            rigidbodies[i].transform.rotation = rigStates[i].rotation;
            rigidbodies[i].isKinematic = rigStates[i].isKinematic;
            rigidbodies[i].velocity = rigStates[i].velocity;
        }
    }
    protected override void Save()
    {
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigStates[i].position = rigidbodies[i].transform.position;
            rigStates[i].rotation = rigidbodies[i].transform.rotation;
            rigStates[i].isKinematic = rigidbodies[i].isKinematic;
            rigStates[i].velocity = rigidbodies[i].velocity;
        }
    }
}
