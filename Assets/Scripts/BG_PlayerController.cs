using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BG_PlayerController : MonoBehaviour
{
    Vector2 PositionPlayer;
    float speed = 5f;
    float limitx = 8.2f;
    public GameObject BulletPrefab;
    Transform ejectPosition;
    // variable boolean pour pas shooter en rafale comme dans le vrai jeu
    public bool CanShoot = true;
    //declaraion de variable pour le score
    Text TxtScore;
    // Pour pouvoir utiliser mon script wave dans ce script 
    BG_Wave waveScript;
    bool detect = true;

    private int score = 0;
    public int Score
    {
        get 
        {
            return score; 
        }

        set{
            score = value;
            //On concatene
            TxtScore.text = "Score:" + score;

        }
    }


    void Start()
    {
        PositionPlayer = transform.position; 
        ejectPosition = GameObject.Find("BG_Eject").transform;
        TxtScore = GameObject.Find("TxtScore").GetComponent<Text>(); 
        waveScript = GameObject.Find("BG_Wave").GetComponent<BG_Wave>();
    }

    
    void Update()
    {
         MovePlayer();
         PlayerShoot();
    }

    void MovePlayer() {
        if (CanShoot)
        {

        PositionPlayer.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
         PositionPlayer.x = Mathf.Clamp (PositionPlayer.x, -limitx, limitx);
         transform.position = PositionPlayer; 
     }

    }

    void PlayerShoot()
    {
        if(Input.GetKeyDown(KeyCode.Space) && CanShoot)
        //J'instancie mon objet//
        {
            // on declare en faux pour ne plus pouvoir tirer
            CanShoot = false;
            Instantiate(BulletPrefab, ejectPosition.position, Quaternion.identity);

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Alien") & detect)
        {
            detect = false;
            StartCoroutine (AlienKillPlayer());
        }
    }

    IEnumerator AlienKillPlayer()
    {
        waveScript.StopWave(); 
        PlayerExplosion();
        GameObject.Find("TxtLives").GetComponent<TxtLives>().LoseSlot();
       yield return new WaitForSeconds(0.2f);
       detect = true;
       waveScript.RestartWave(1f);
      
    }

    void PlayerExplosion()
    {
        GetComponent<Animator>().SetTrigger("explosion"); 
        GetComponent<AudioSource>().Play(); 
        CanShoot = false;

    }

    public void InitPlayer()
    {
        GetComponent<Animator>().SetTrigger("normal");
        CanShoot = true; 
    }
}
