using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutAttack : MonoBehaviour
{
    [SerializeField] private float radius = 10f;
    [SerializeField] private int minDamage = 8;
    [SerializeField] private int maxDamge = 15;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float cooldown = 9f;
    [SerializeField] private ParticleSystem damageEffect;

    private float timer;
    private GameObject player;
    private CharacterStats characterStats;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterStats = player.GetComponent<CharacterStats>();
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
        damageEffect.Play();

        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, radius, enemyMask);
        if (hits.Length > 0)
        {
            foreach (Collider2D hit in hits)
            {
                int damage = UnityEngine.Random.Range(minDamage, maxDamge + 1);
                hit.GetComponent<BaseEnemyScript>().TakeDamage(damage * characterStats.playerBaseAttackDmg);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
