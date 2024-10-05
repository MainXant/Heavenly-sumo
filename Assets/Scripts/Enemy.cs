using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public Rigidbody enemy;
    private GameObject player;
    public float stabilizer{get;set;}=2;
    public float speed{get;private set;}=3f;//Инкапсуляция
    void Start()
    {
        enemy=GetComponent<Rigidbody>();
        player=GameObject.Find("Player");
    }
   void Update()
    {
       SpeedForce();
       
    }
    public virtual void SpeedForce(){
    Vector3 lookDirection=(player.transform.position-transform.position).normalized/stabilizer;
        enemy.AddForce(lookDirection*speed);
        if(transform.position.y<-5){
            Destroy(gameObject);
        }   
        
   }
}
