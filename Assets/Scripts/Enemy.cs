using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed=3f;
    private Rigidbody enemy;
    private GameObject player;
    void Start()
    {
        enemy=GetComponent<Rigidbody>();
        player=GameObject.Find("Player");
    }
    void Update()
    {
        Vector3 lookDirection=(player.transform.position-transform.position).normalized/2;//преследование игрока
        enemy.AddForce(lookDirection*speed);
        if(transform.position.y<-5){
            Destroy(gameObject);
        }
    }
}
