using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hasPowerUp=false;
    private float speed=5f;
    private float powerUpForce=16f;
    private GameObject focalPoint;
    public GameObject powerUpIndicator;
    private float jumpForce=60f;
    private float gravityForce=1;
    private bool isOnGround=true;
    private Rigidbody playerRb;
    void Start()
    {
        playerRb=GetComponent<Rigidbody>();
        focalPoint=GameObject.Find("Focal Point");//пустой объект к которому привязана камера
        hasPowerUp=true;
        powerUpIndicator.SetActive(true);
        StartCoroutine(PowerUpCountdownRoutine());
        Physics.gravity*=gravityForce;
    }
    void Update()
    {
        float moveForward=Input.GetAxis("Vertical");
        powerUpIndicator.transform.position=transform.position+new Vector3(0,-0.3f,0);
        playerRb.AddForce(focalPoint.transform.forward*moveForward*speed);//движение вперёд , взависимости от направления камеры
        JumpPowerUp();
    }    
    void JumpPowerUp(){
        if(Input.GetKeyDown(KeyCode.Space)&&isOnGround&&hasPowerUp){
        playerRb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
        isOnGround=false;}
    }         
    IEnumerator PowerUpCountdownRoutine(){//отсчёт времени действия баффа
    yield return new WaitForSeconds(8);
    hasPowerUp=false;
    powerUpIndicator.SetActive(false);
    }
    private void OnTriggerEnter(Collider otcher)//уничтожает обьект ПаверАпа при срабатывания триггера коллайдера
    {
           if(otcher.CompareTag("PowerUp"))
     {
            hasPowerUp=true;
            Destroy(otcher.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
            powerUpIndicator.SetActive(true);
     }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy")&&hasPowerUp){
          Rigidbody enemyRigidbody=collision.gameObject.GetComponent<Rigidbody>();//разъёб при активном ПаверАпе
          Vector3 awayFromPlayer=collision.gameObject.transform.position-transform.position;
          enemyRigidbody.AddForce(awayFromPlayer*powerUpForce,ForceMode.Impulse);
          Debug.Log("Collidet with "+collision.gameObject.name+" with PowerUp to "+hasPowerUp);
        }
        if(collision.gameObject.CompareTag("Ground")){
        isOnGround=true;
      }
    }
}
