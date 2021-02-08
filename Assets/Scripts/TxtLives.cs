using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtLives : MonoBehaviour
{
    public GameObject[] slots;
    public int remainingSlot;

    private void Awake()
    {
    	slots = GameObject.FindGameObjectWithTag("Slot"); 
    	remainingSlot = slots.Length;
    }

    public void LoseSlot()
    {
    	remainingSlot -= 1;

    	switch (remainingSlot)
    	{
    		case 2:
    		slots[0].SetActive(false);
    		break;

    		case 1:
    		slots[1].SetActive(false);
    		break;

    		case 0:
    		slots[2].SetActive(false);
    		GameOver();

    		break;
    	}
    }

    void GameOver()
    {
    	print ("Game Over duuuude");
    }
}
