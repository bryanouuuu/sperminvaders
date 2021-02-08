using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Bumpers : MonoBehaviour
{

	public bool detect = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
    	// Avec le tag on va dire que quand l'alien entre en collision avec un mur, l'objet collision va chercher le composant dans le parent
    	if(collision.CompareTag("Alien") && detect)
    	{
    		detect = false;
    		collision.GetComponentInParent<BG_Wave>().WaveTouchBumper();
    		StartCoroutine(wait()); 
    	}
    }

    IEnumerator wait()
    {
    	yield return new WaitForSeconds(0.2f); 
    	detect = true;
    }
}
