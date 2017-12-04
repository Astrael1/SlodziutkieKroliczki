using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Przeszkoda : MonoBehaviour 
{

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		setVelocity (GameManager.Instance.velocity);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		setVelocity (GameManager.Instance.velocity);
		
	}

	void OnTriggerEnter2D( Collider2D other )
	{


		if (other.gameObject.CompareTag ("Destruktor")) {
			//Debug.Log("Kolizja z destruktorem");
			Destroy (this.gameObject);
		}
	}


	private void setVelocity(float f)
	{
		rigidBody.velocity = new Vector2 (0, f);
	}
}
