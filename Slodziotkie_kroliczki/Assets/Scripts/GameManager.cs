using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> 
{
	public GameObject[] przeszkody;
	public bool[] czyPrzeszkoda = new bool[20];
	public Transform generator;
	public GameObject background;
	public Text scoreText;
	public Text gameOverText;
	public Image dirt;
    public GameObject gameOverButtons;

	private float cloudSpawnCheck;
	public float CloudIntervals; //co ile pojawia sie chmurka
	private float cloud2SpawnCheck;
	public float Cloud2Intervals; //co ile pojawia sie chmurka o wlasnej predkosci
	private float InverterSpawnCheck;
	public float InverterIntervals; //co ile pojawia sie odwracacz sterowania
	public float InversionDuration; //ile trwa odwrocenie sterowania
	private float VisionReductionCheck;
	public float VisionReductionIntervals; // co ile pojawia sie plama
	public float VisionReductionDuration; // jak dlugo trwa plama
	public float InstaDeathIntervals;
	private float InstaDeathCheck;
	private float score;
	public float velocity;
	public float chmurkaSila;
	public float acceleration;
	public float distance;
	public float endDistance; //po jakim dystansie gra ma sie skonczyc

	public bool[] playerStatus = new bool[20];

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (SpawnujPrzeszkody() );
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (velocity < 0) 
		{
			velocity = 0;
			acceleration = 0;
			playerStatus [2] = true;
		}
			
		velocity += acceleration * Time.deltaTime;
		distance += velocity * Time.deltaTime + (acceleration * Time.deltaTime * Time.deltaTime) / 2; //funkcja na droge .-.
		cloudSpawnCheck = (int)distance % CloudIntervals;
		cloud2SpawnCheck = (int)distance % Cloud2Intervals;
		InverterSpawnCheck = (int)distance % InverterIntervals;
		VisionReductionCheck = (int)distance % VisionReductionIntervals;
		InstaDeathCheck = (int)distance % InstaDeathIntervals;
		UpdateScore ();

		
	}

	public void ZamowPrzeszkode(int p)
	{
		czyPrzeszkoda [p] = true;
	}

	private IEnumerator SpawnujPrzeszkody()
	{		
		//float r = (background.GetComponent<SpriteRenderer> ().bounds.size.x / 2);
		while (true) 
		{
			float x = Random.Range(-5, 5);
			float x1 = Random.Range(-5, 5);
			float x2 = Random.Range(-5, 5);
			if(cloudSpawnCheck == 10) //10 zeby dac graczowi chwile na ogarniecie
			{
				Instantiate (przeszkody[0], new Vector3(x, generator.transform.position.y, 1), Quaternion.identity,generator); //podstawowa chmurka

				//NowaPrzeszkoda ();
				Debug.Log ("Nowa przeszkoda");
			 	// new WaitForFixedUpdate (przeszkodyCzas);
				yield return new WaitForSeconds(1); //inaczej spawnuje miliord chmurek na raz, ale teraz pojawia sie max 1 na sekunde, niezaleznie od predkosci
			}
			if(cloud2SpawnCheck == 30) //jak wyzej
			{
				ZamowPrzeszkode(1); //churka z wlasna predkoscia

				//NowaPrzeszkoda ();
				Debug.Log ("Nowa przeszkoda");
				// new WaitForFixedUpdate (przeszkodyCzas);
				yield return new WaitForSeconds(1); 
			}
			if(InverterSpawnCheck == 50) //jak wyzej
			{
				ZamowPrzeszkode (2); //odwracacz sterowania

				//NowaPrzeszkoda ();
				Debug.Log ("Nowa przeszkoda");
				// new WaitForFixedUpdate (przeszkodyCzas);
				yield return new WaitForSeconds(1);
			}
			if (VisionReductionCheck == 45) 
			{
				ZamowPrzeszkode (3);
				yield return new WaitForSeconds(1);
			}
			if (InstaDeathCheck == 25) 
			{
				ZamowPrzeszkode (4);
				yield return new WaitForSeconds(1);
			}
			for (int i = 0; i <= przeszkody.GetLength (0); i++) 
			{
				if (czyPrzeszkoda [i]) 
				{
					Instantiate (przeszkody [i], new Vector3 (x, generator.transform.position.y, 1), Quaternion.identity, generator);
					czyPrzeszkoda [i] = false;
				}
			}
			yield return null;
		}
	}


	private void UpdateScore()
	{
		score += (Time.deltaTime * velocity);
		scoreText.text = "Score: " + (int)distance;
	}

	public void GameOverMessage()
	{
		gameOverText.text = "GAME OVER\nScore: " + (int)score;
		gameOverText.gameObject.SetActive(true);
        gameOverButtons.SetActive(true);
	}
}
