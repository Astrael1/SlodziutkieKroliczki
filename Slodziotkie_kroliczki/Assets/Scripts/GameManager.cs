using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> 
{
	public GameObject przeszkoda;
	public Transform generator;
	public GameObject background;
	public Text scoreText;
	public Text gameOverText;
    public GameObject gameOverButtons;


	public float przeszkodyCzas;
	private float score;
	public float velocity;
	public float chmurkaSila;
	public float acceleration;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (NowaPrzeszkoda() );		
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
		UpdateScore ();

		
	}

	private IEnumerator NowaPrzeszkoda()
	{
		

		//float r = (background.GetComponent<SpriteRenderer> ().bounds.size.x / 2);
		while (true) 
		{
			float x = Random.Range(-5, 5);
			Instantiate (przeszkoda, new Vector3(x, generator.transform.position.y, 1), Quaternion.identity,generator);


			//NowaPrzeszkoda ();
			Debug.Log ("Nowa przeszkoda");
			yield return new WaitForSeconds (przeszkodyCzas);
		}


	}

	private void UpdateScore()
	{
		score += (Time.deltaTime * velocity);
		scoreText.text = "Score: " + (int)score;
	}

	public void GameOverMessage()
	{
		gameOverText.text = "GAME OVER\nScore: " + (int)score;
		gameOverText.gameObject.SetActive(true);
        gameOverButtons.SetActive(true);
	}
}
