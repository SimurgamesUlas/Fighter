using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAl : MonoBehaviour
{
    
    public Vector2 pos1;
    public Vector2 pos2;
    
    public float leftrightspeed;
    public float oldPosition;
    
    public float distance;

    private Transform target;

    private Animator anim;
    public float followspeed;
    
    void Start()
    {
        Physics2D.queriesStartInColliders = false; 
        target= GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        EnemyAi();

    }

    void EnemyMove(){

        transform.position = Vector3.Lerp(pos1,pos2, Mathf.PingPong(Time.time * leftrightspeed,1.0f));

        if(transform.position.x > oldPosition){
            transform.localRotation = Quaternion.Euler(0,180,0);
        }
        if(transform.position.x < oldPosition){
            transform.localRotation = Quaternion.Euler(0,0,0);
        }
        oldPosition = transform.position.x;
    }

    void EnemyAi(){
        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, -transform.right, distance);

        if(hitEnemy.collider != null && hitEnemy.collider.name != "Enemy"){
            Debug.DrawLine(transform.position, hitEnemy.point, Color.red);
            anim.SetBool("Attack",true);
            EnemyFollow();

        }else{

            Debug.DrawLine(transform.position,transform.position - transform.right * distance,Color.green);
            anim.SetBool("Attack",false);
            EnemyMove();
        }
    }

    void EnemyFollow(){
        Vector3 targetPosition = new Vector3(target.position.x, gameObject.transform.position.y ,target.position.x);
        transform.position = Vector2.MoveTowards(transform.position,targetPosition,followspeed * Time.deltaTime); 
    }
}