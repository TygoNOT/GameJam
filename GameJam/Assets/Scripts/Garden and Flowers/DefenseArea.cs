using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseArea : MonoBehaviour
{
    [Header("Attribute")]
    public int maxHealth = 100;
    protected int currentHealth;

    [Header("Health Bar")]
    [SerializeField] private HealthBar healthBar;

    protected virtual void Start()
    {

        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            OnDestroyed();
        }
    }

    protected virtual void OnDestroyed()
    {
        Debug.Log($"{gameObject.name} уничтожен!");
        Destroy(gameObject);
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            TakeDamage(10);

        if (Input.GetKeyDown(KeyCode.H))
            Heal(10);
    }
    public virtual void Heal(int amount)
    {
        if (currentHealth > 0)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //TakeDamage(10);
        }
    }
}
