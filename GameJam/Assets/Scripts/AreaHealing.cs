using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHealing : MonoBehaviour
{
    [SerializeField] private float radius = 10f;
    [SerializeField] private int healAmmount = 10;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float cooldown = 10f;

    private float timer;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > cooldown)
            DoHealing();
    }

    void DoHealing()
    {
        timer = 0;

        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, radius, enemyMask);
        if (hits.Length > 0)
        {
            foreach (Collider2D hit in hits)
            {
                var healing = hit.GetComponent<DefenseArea>();
                if (healing != null)
                    healing.Heal(healAmmount);
            }
        }
    }
}
