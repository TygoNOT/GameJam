using UnityEngine;

public class SlowingWeapon : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField, Range(0, 100)] private int slowPercentage = 30;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float cooldown = 8f;
    [SerializeField] private ParticleSystem particleSystems;

    private float timer;
    private Collider2D[] enemiesHit;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > cooldown)
            DoAttack();

        timer += Time.deltaTime;
    }

    void DoAttack()
    {
        timer = 0;
        enemiesHit = Physics2D.OverlapCircleAll(this.transform.position, radius, enemyMask);
        if (enemiesHit.Length > 0)
            foreach (Collider2D hit in enemiesHit)
                hit.GetComponent<BaseEnemyScript>().speedAmp -= slowPercentage/100;

        Invoke("ResetSpeed", 3);
    }

    void ResetSpeed()
    {
        foreach (Collider2D hit in enemiesHit)
            hit.GetComponent<BaseEnemyScript>().speedAmp += slowPercentage / 100;
    }
}
