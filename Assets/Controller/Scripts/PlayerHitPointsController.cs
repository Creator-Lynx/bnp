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
    void Start()
    {
        SetAttempts();
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
        HP -= damage;
        if (HP <= 0)
        {
            HP = maxHP;
            attempts--;
            SetAttempts();
            ReturnToCheckPoint();
        }
        if (attempts <= 0)
        {
            HP = 0;
            Death();
        }
        bar.OnHpChange((float)HP / (float)maxHP);

    }
    void ReturnToCheckPoint()
    {

    }
    void Death()
    {
        GetComponent<PaddleMover>().isUncontrolled = true;
        GetComponent<SailMover>().isUncontrolled = true;
        GetComponent<WaterSimulation>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Animation>().Play("dead");
        BasicGameManager.LoseLevel();
    }
    public void CompleteLevel()
    {
        GetComponent<PaddleMover>().isUncontrolled = true;
        GetComponent<SailMover>().isUncontrolled = true;
    }


}
