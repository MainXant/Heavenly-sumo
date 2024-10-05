using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool hasPowerUp=false;
    private bool isOnGround=true;
    public bool gameOver=false;
    private GameObject focalPoint;
    public GameObject powerUpIndicator;
    public GameObject jumpText;
    public AudioSource playerSounds;
    public AudioClip crashSound;
    private Rigidbody playerRb;
    private float speed=5f;
    private float powerUpForce=16f;
    private float jumpForce=60f;
    private float gravityForce=1;
    
    
    void Start()
    {
        playerRb=GetComponent<Rigidbody>();
        focalPoint=GameObject.Find("Focal Point");//пустой объект к которому привязана камера
        hasPowerUp=true;
        powerUpIndicator.SetActive(true);
        StartCoroutine(PowerUpCountdownRoutine());
        Physics.gravity*=gravityForce;
        jumpText.SetActive(true);
    }
    void Update()
    {
        float moveForward=Input.GetAxis("Vertical");
        powerUpIndicator.transform.position=transform.position+new Vector3(0,-0.3f,0);
        playerRb.AddForce(focalPoint.transform.forward*moveForward*speed);//движение вперёд , взависимости от направления камеры
        JumpPowerUp();
        GameOver();
    }    
    void JumpPowerUp(){
        if(Input.GetKeyDown(KeyCode.Space)&&isOnGround&&hasPowerUp){
        playerRb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
        isOnGround=false;}
    }         
    IEnumerator PowerUpCountdownRoutine(){//отсчёт времени действия баффа
    yield return new WaitForSeconds(8);
    jumpText.SetActive(false);
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
            jumpText.SetActive(true);
     }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy")&&hasPowerUp){
          Rigidbody enemyRigidbody=collision.gameObject.GetComponent<Rigidbody>();//разъёб при активном ПаверАпе
          Vector3 awayFromPlayer=collision.gameObject.transform.position-transform.position;
          enemyRigidbody.AddForce(2*awayFromPlayer*powerUpForce,ForceMode.Impulse);
        }
         if(collision.gameObject.CompareTag("Enemy")){
             playerSounds.PlayOneShot(crashSound,0.5f);
         }
        if(collision.gameObject.CompareTag("Ground")){
        isOnGround=true;
      }
    }
    private void GameOver(){
        if(transform.position.y<-5){
            Destroy(gameObject);
            gameOver=true;
            SceneManager.LoadScene(2);
        }
    }
}
