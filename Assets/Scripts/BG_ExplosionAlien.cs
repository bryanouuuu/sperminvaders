using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_ExplosionAlien : MonoBehaviour
{
	SpriteRenderer sr;
	public float delay = 0.5f;
	AudioSource audiosource;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		audiosource = GetComponent<AudioSource>(); 
	}

	private void Start()
	{
		StartCoroutine(DestroyExplosion());

	}

	IEnumerator DestroyExplosion()
	{
		//Je joue le son d'explosion quand je détruis l'alien
		audiosource.Play();
		yield return new WaitForSeconds(delay); 
		GameObject.Find("BG_Wave").GetComponent<BG_Wave>().Remaining_Alien -= 1;
		//Je détruis l'objet
		Destroy(this.gameObject, delay);
	}
    
  
}
