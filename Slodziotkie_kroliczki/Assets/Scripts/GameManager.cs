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
    public GameObject gameOverButtons;


	private float spawnCheck;
	private float score;
	public float velocity;
	public float chmurkaSila;
	public float acceleration;
	private float distance;
	public float obstacleIntervals; //co ile pojawia sie chmurka

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
		}
			
		velocity += acceleration * Time.deltaTime;
		distance += velocity * Time.deltaTime + (acceleration * Time.deltaTime * Time.deltaTime) / 2; //funkcja na droge .-.
		spawnCheck = (int)distance % obstacleIntervals;
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
			if(spawnCheck == 10) //10 zeby dac graczowi chwile na ogarniecie
			{
				Instantiate (przeszkody[0], new Vector3(x, generator.transform.position.y, 1), Quaternion.identity,generator);


				//NowaPrzeszkoda ();
				Debug.Log ("Nowa przeszkoda");
			 	// new WaitForFixedUpdate (przeszkodyCzas);
				yield return new WaitForSeconds(1); //inaczej spawnuje miliord chmurek na raz, ale teraz pojawia sie max 1 na sekunde, niezaleznie od predkosci
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
