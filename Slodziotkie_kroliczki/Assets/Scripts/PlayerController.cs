using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	float speed;
	private float inverted = 0;
	public Text ConfusedText;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	void Update ()
	{
		if (GameManager.Instance.velocity <=0)
		{
			Destroy (gameObject);
			GameManager.Instance.GameOverMessage ();
		}
	}
	void FixedUpdate () 
	{
		if (inverted == 1) 
		{
			transform.Translate (Vector2.left * Input.GetAxis ("Horizontal") * speed * Time.deltaTime);
		} 
		else
		{
			transform.Translate (Vector2.right * Input.GetAxis ("Horizontal") * speed * Time.deltaTime);
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
			Destroy(other.gameObject);
			StartCoroutine (Inversion());
		}
	}

	private IEnumerator Inversion() //odwraca sterowanie na liczbe sekund ustawiona w GameManager
	{
		inverted = 1;
		//ConfusedText.text = "You're Confused!";  //tekst nie chce mi dzialac xd
		//ConfusedText.gameObject.SetActive (true);
		yield return new WaitForSeconds (GameManager.Instance.InversionDuration);
		inverted = 0;
		//ConfusedText.gameObject.SetActive (false);
	}
}
