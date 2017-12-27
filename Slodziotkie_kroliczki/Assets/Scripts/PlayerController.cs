using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	float speed;
	public Text ConfusedText;

	// tablica na korutyny
	private Coroutine[] coroutines = new Coroutine[10];


	// Use this for initialization
	void Start () 
	{
		
	}
	
	void Update ()
	{
		if (GameManager.Instance.velocity <=0)
		{
			//Destroy (gameObject);
			GameManager.Instance.GameOverMessage ();
		}
	}
	void FixedUpdate () 
	{
		if (GameManager.Instance.playerStatus [2] != true) 
		{
			if (GameManager.Instance.playerStatus [0] == true) 
			{
				transform.Translate (Vector2.left * Input.GetAxis ("Horizontal") * speed * Time.deltaTime);
			} 
			else 
			{
				transform.Translate (Vector2.right * Input.GetAxis ("Horizontal") * speed * Time.deltaTime);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("chmurka"))
		{
			Destroy(other.gameObject);
			GameManager.Instance.velocity -= GameManager.Instance.chmurkaSila;
		}

		if(other.gameObject.CompareTag("Inverter"))
		{
			IEnumerator num = other.GetComponent<Przeszkoda_A>().Effect(); // wynajduje efekt przeszkody
			Destroy(other.gameObject); // niszczy przeszkode
			if (GameManager.Instance.playerStatus [0] == true) // jesli efekt juz jest 
			{
				StopCoroutine (coroutines[0]); // zatrzymaj korutyne (inaczej sie nie dało)
			}
			coroutines[0] = StartCoroutine (num); // zaaplikuj efekt
		}
		if(other.gameObject.CompareTag("ReduceVision"))
		{
			IEnumerator num = other.GetComponent<Przeszkoda_A>().Effect();
			Destroy(other.gameObject);
			if (GameManager.Instance.playerStatus [1] == true) 
			{
				StopCoroutine (coroutines[1]);
			}
			coroutines[1] = StartCoroutine (num);
		}
		if(other.gameObject.CompareTag("InstaDeath"))
		{
			GameManager.Instance.velocity = -1;
		}
	}

	private IEnumerator Inversion() //odwraca sterowanie na liczbe sekund ustawiona w GameManager
	{
		GameManager.Instance.playerStatus[0] = true; // zapisz w tablicy, ze sterowanie jest odwrocone
		ConfusedText.text = "You're Confused!";
		ConfusedText.gameObject.SetActive (true);
		yield return new WaitForSeconds (GameManager.Instance.InversionDuration);
		GameManager.Instance.playerStatus[0] = false;
		ConfusedText.gameObject.SetActive (false);
	}

	private IEnumerator ReduceVision()
	{
		Debug.Log ("Zaciemniam wizje");
		GameManager.Instance.playerStatus [1] = true;
		GameManager.Instance.dirt.gameObject.SetActive (true);
		yield return new WaitForSeconds (GameManager.Instance.VisionReductionDuration);
		GameManager.Instance.dirt.gameObject.SetActive (false);
		Debug.Log ("Koniec zaciemniania");
	}
}
