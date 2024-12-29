using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public int maxHealth = 100;
    public int currentHealth;
    EnemyAl enemyai;
    void Start()
    {
        currentHealth = maxHealth;
        enemyai = GetComponent<EnemyAl>();
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        anim.SetTrigger("Hurt");

        if(currentHealth <=0){
            Die();
        }
    }

    void Die(){
        anim.SetBool("isDead",true);
        GetComponent<Collider2D>().enabled = false;
        enemyai.followspeed = 0;
        this.enabled = false;
        Destroy(gameObject,1f);
    }
}
