using UnityEngine;

public class AnimatorSaver : SelfSaver
{
    int shortNameHash;
    float stateNormalizedTime;
    protected override void Load()
    {
        GetComponent<Animator>().Play(shortNameHash, 0, stateNormalizedTime);
    }
    protected override void Save()
    {
        AnimatorStateInfo info = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        stateNormalizedTime = info.normalizedTime;
        shortNameHash = info.shortNameHash;
    }
}
