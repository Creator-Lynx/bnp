using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitPointsController : MonoBehaviour
{
    public int HitPoints { get => HP; }
    [SerializeField]
    int HP = 10;
    public int MaximumHitPoints { get => maxHP; }
    [SerializeField]
    int maxHP = 10;
    public int AttemptsNumber { get => attempts; }
    [SerializeField]
    int attempts = 3;
    [SerializeField]
    [Space(30)]
    HPBar bar;
    [SerializeField]
    Image[] attemptsImages = new Image[3];

    [SerializeField]
    bool isDamageble = true;
    bool isDead = false;
    [SerializeField]
    float damageblaDelay = 1f;
    [SerializeField]
    float oneHPInDecimal = .1f;
    [SerializeField]
    CheckPointReturn checkPointReturn;

    ParticleSystem damageParticle;
    void Start()
    {
        SetAttempts();
        bar.OnHpChange(HP * oneHPInDecimal);
        damageParticle = Camera.main.GetComponentInChildren<ParticleSystem>();
    }

    void SetAttempts()
    {
        for (int i = 0; i < attemptsImages.Length; i++)
        {
            if (i >= attempts) attemptsImages[i].color = Color.gray / 2f;
        }
    }
    public void SetDamage(int damage)
    {
        if (!isDamageble || isDead) return;
        HP -= damage;
#if !PLATFORM_STANDALONE
        Handheld.Vibrate();
#endif
        damageParticle.Play();
        if (HP <= 0)
        {
            HP = maxHP;
            attempts--;
            SetAttempts();
            if (attempts <= 0)
            {
                HP = 0;
                Death();
            }
            else
                checkPointReturn.StartReturn();
        }

        bar.OnHpChange(HP * oneHPInDecimal);
        StartCoroutine(DamageblaDelay());
    }

    void Death()
    {
        GetComponent<PaddleMover>().isUncontrolled = true;
        GetComponent<SailMover>().isUncontrolled = true;
        GetComponent<SailMover>().enabled = false;
        GetComponent<WaterSimulation>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Animation>().Play("dead");
        StopCoroutine(DamageblaDelay());
        isDamageble = false;
        isDead = true;
        BasicGameManager.LoseLevel();
    }
    public void CompleteLevel()
    {
        GetComponent<PaddleMover>().isUncontrolled = true;
        GetComponent<SailMover>().isUncontrolled = true;
    }

    IEnumerator DamageblaDelay()
    {
        isDamageble = false;
        yield return new WaitForSeconds(damageblaDelay);
        isDamageble = true;
    }
}
