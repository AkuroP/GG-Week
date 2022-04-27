using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float shootingRate = 1f;
    private float nextFireTime;
    public SpriteRenderer EnemySprite;
    public GameObject bullet;
    public GameObject bulletParent;
    public GameObject bulletParent2;
    private Transform player;

    public int damageOnCollision = 20;

    public int enemyLife;
    public int healthPoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        if (player != null) { 

                float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
                if (distanceFromPlayer < lineOfSite && distanceFromPlayer>shootingRange)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);            
                }
                else if (distanceFromPlayer <= shootingRange && nextFireTime<Time.time)
                {   
                    if(player.position.x < this.transform.position.x)
                    {
                
                        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                        EnemySprite.flipX = false;

                    }
                    else
                    {
                        Instantiate(bullet, bulletParent2.transform.position, Quaternion.identity);
                        EnemySprite.flipX = true;
                    }
                        nextFireTime = Time.time + shootingRate;
                }
            }
        }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }

public void Hit(int damage)
    {
        enemyLife -= damage;
        if(enemyLife <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(this.gameObject);
        PlayerHealth.instance.HealPlayer(healthPoint);
    }
}
