using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemy : Enemy//Наследование
{
    private GameObject player;
    private float hardModifier=2f;
    public float scaleX=1f;
    public float scaleY=1f;
    public float scaleZ=1f;
    
    void Start()
    {
       player=GameObject.Find("Player");
    }

    void Update()
    {  
       
       SpeedForce();//абстракция
    }
   
    public override void SpeedForce()//полиморфизм
    {
      Vector3 lookDirection=(player.transform.position-transform.position).normalized/stabilizer*hardModifier;//преследование игрока
        enemy.AddForce(lookDirection*speed*hardModifier);
        if(transform.position.y<-5||transform.position.y>2){
            Destroy(gameObject);
        }
    }

}
