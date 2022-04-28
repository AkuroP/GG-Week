using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool isInvincible = false;

    public bool isDamaged = false;

    public SpriteRenderer graphics;

    public HealthBar healthBar;

    public float invincibilityTime = 0.15f;
    public float invincibilityTimeAfterHit = 2f;

    public float DamageTime = 0.15f;
    public float DamageTimeAfterHit = 2f;

    public static PlayerHealth instance;

    private Enemy enemy;
    

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus de 1 heal");
            return;
        }

        instance = this;
    }



    void Start()
    {
        currentHealth = 80;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    void Update()
    {
    
        //TakeDamage(20);

        
    }

    public void HealPlayer(int amount)
    {
        if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        
        healthBar.SetHealth(currentHealth);
    }
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if(currentHealth <= 0)
            {
                Die();
                return;
            }



            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public IEnumerator TakeDamageInDeathWorld(int damage)
    {
       
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();           
        }
        isDamaged = true;
        StartCoroutine(DamageFlash());
        StartCoroutine(HandleDamageDelay());
        yield return new WaitForSeconds(1f);


        if (Player.instance.lifeOrDeath == false)
        {
            StartCoroutine(TakeDamageInDeathWorld(damage));
        }
    }

    public void Die()
    {
        Debug.Log("Le joueur est mort");
        Player.instance.enabled = false;
        Player.instance.animator.SetTrigger("Death");
        //Player.instance.rb.bodyType = RigidbodyType2D.Static;
        //Player.instance.circleCollider.enabled = false;
        Player.instance.tag = "Untagged";
        GameOverManager.instance.OnPlayerDeath();
        

    }

    public void Destruction()
    {
        Destroy(Player.instance.gameObject);
    }

    public IEnumerator DamageFlash()
    {
        while (isDamaged)
        {
            graphics.color = new Color(0.6f, 0f, 0f, 1f);
            yield return new WaitForSeconds(DamageTime);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(DamageTime);
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityTime);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityTime);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }

    public IEnumerator HandleDamageDelay()
    {
        yield return new WaitForSeconds(DamageTimeAfterHit);
        isDamaged = false;
    }
}
