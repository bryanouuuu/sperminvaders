using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_BulletPlayer : MonoBehaviour
{
    public float Force = 600f, DestroyTime=1f;
    Rigidbody2D rb;
    public GameObject ExplosionPrefab;
    private BG_PlayerController playercontroller;
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<BG_PlayerController>();

    }

    void Start()
    {
    	//on applique la force
    	rb.AddForce(Vector2.down * Force);
    	// on choisit la destruction de l'objet et le moment ou il se detruit
    	Destroy(gameObject, DestroyTime);
        
    }
    // cette variable permet de trouver l'objet avec le tag Player, aller chercher la variable public canshoot et la mettre en vrai
    private void OnDestroy(){
    	playercontroller.CanShoot = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    	if(collision.gameObject.CompareTag("Alien"));
    	{
    		// je detruis l'alien
    	Destroy(collision.gameObject); 
    	// j'instantie l'explosion 	
    	Instantiate(ExplosionPrefab, collision.transform.position, Quaternion.identity);
    	playercontroller.Score += 50;
    	// je detruis l'objet
    	Destroy(this.gameObject);

    }
    }
}
