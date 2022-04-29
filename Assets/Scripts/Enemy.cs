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
    private Transform player;
    private Animator enemyAnim;

    public bool isAShooter = false;

    public int damageOnCollision = 20;

    public int enemyLife;
    public int healthPoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnim = this.GetComponent<Animator>();
    }

    
    void Update()
    {
        if (player != null) { 
                if(EnemySprite != null)
                {
                    if(player.position.x < this.transform.position.x)
                    {
                        EnemySprite.flipX = true;
                    }
                    else
                    {
                        EnemySprite.flipX = false;
                    }
                }

                float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
                if (distanceFromPlayer < lineOfSite && distanceFromPlayer>shootingRange)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);       
                    if(enemyAnim != null)
                    {
                        enemyAnim.SetBool("Chase", true);     
                    }
                }
                else if (distanceFromPlayer <= shootingRange && nextFireTime<Time.time) 
                {   
                    if(enemyAnim != null)
                    {
                        enemyAnim.SetBool("Chase", false);     
                        enemyAnim.SetTrigger("Attack");
                    }
                    if(isAShooter)
                    {

                        nextFireTime = Time.time + shootingRate;
                    }
                        
                }
            }
        }

    public void Shoot()
    {
        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isAShooter == false)
        {
            if (collision.transform.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(damageOnCollision);
            }

        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if(isAShooter == false)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(damageOnCollision);
            }

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
