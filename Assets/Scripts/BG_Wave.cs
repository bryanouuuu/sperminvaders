using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Wave : MonoBehaviour
{
// variable generateur
	public GameObject[] AlienType;
	public float SpaceColumns = 2f, SpaceRows = 2f;
	public int TotalAlienInLine = 6;

	public bool CanMoove = true;
	public bool WalkRight =true;
	public float WaveStepRight = 1f, WaveStepDown = 1f, WaveSpeed = 0.8f;

// son de la vague
	public AudioClip[] Clipaudio;
	int curClip= 0;
	AudioSource audiosource;

// gstion du nombre d'alien
	public int Total_Alien_In_Wave;
	public int  Remaining_Alien;

// restart de la vague
	Vector2 PositionInitialeWave;
	BG_PlayerController playerController;

	private void Awake()
	{
// Generation de la vague d'alien

for (int i = 0; i < AlienType.Length; i++) //boucle sur tous les elements du tableau
{
float posY = transform.position.y - (SpaceRows * i); //définition de la ligne (y)

for (int n = 0; n < TotalAlienInLine; n++) //boucle sur le nb d'alien à instancier
{
//definition de la position x de l'alien sur la ligne y.
	Vector2 pos = new Vector2(transform.position.x + SpaceColumns * n, posY);
GameObject Go = Instantiate(AlienType[i].gameObject, pos, Quaternion.identity); //instantiation
Go.transform.SetParent(this.transform); //Objet enfant de Wave
Go.name = "Alien" +( n+1) + "-row:" + (i+1); //Définition du nom des aliens 
}
}
// assignation du nombre d'alien
Total_Alien_In_Wave = transform.childCount;
Remaining_Alien = Total_Alien_In_Wave;

//position initiale
PositionInitialeWave = transform.position;
playerController = GameObject.Find("Player").GetComponent<BG_PlayerController>();
}
private void Start()
{
	audiosource = GetComponent<AudioSource>();
	StartCoroutine(MooveWave());

}

IEnumerator MooveWave()
{
while (CanMoove)
{
	IsWaveempty();
	Vector2 direction = WalkRight ? Vector2.right : Vector2.left;
	transform.Translate(direction * WaveStepRight);
	// Ici je vais executer mon script meme si il est privé dans mon objet
	BroadcastMessage("AnimateAlien");
	PlayWaveSound();
	yield return new WaitForSeconds(WaveSpeed);
}
}

public void WaveTouchBumper()
{
	WalkRight = !WalkRight;
	transform.Translate(Vector2.up * WaveStepDown); 
}
void PlayWaveSound()
{
	curClip = curClip < Clipaudio.Length - 1 ? curClip += 1 : curClip = 0;
	audiosource.PlayOneShot(Clipaudio[curClip]);
}

void IsWaveempty()
{
	if(Remaining_Alien==0)
	{
		print("T'as putain de gagné mec");
		StopAllCoroutines();
	}
}

public void StopWave()
{
	StopAllCoroutines();
}

public void RestartWave(float delay)
{
    StartCoroutine(Restart(delay));
}
IEnumerator Restart(float delay)
{
	yield return new WaitForSeconds(delay);
	transform.position = PositionInitialeWave;
	StartCoroutine(MooveWave());
	playerController.InitPlayer();
}
}
