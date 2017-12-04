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


	public float przeszkodyCzas;
	private bool nastepna = true;
	private float score;
	public float velocity;
	public float chmurkaSila;
	public float acceleration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (nastepna) 
		{
			StartCoroutine (NowaPrzeszkoda() );
			nastepna = false;
		}
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
		
		yield return new WaitForSeconds (przeszkodyCzas);
		//float r = (background.GetComponent<SpriteRenderer> ().bounds.size.x / 2); 
		float x = Random.Range(-5, 5);
		Instantiate (przeszkoda, new Vector3(x, generator.transform.position.y, 1), Quaternion.identity,generator);
		nastepna = true;

		Debug.Log ("Nowa przeszkoda");
	}

	private void UpdateScore()
	{
		score += (Time.deltaTime * velocity);
		scoreText.text = "Score: " + (int)score;
	}
}
