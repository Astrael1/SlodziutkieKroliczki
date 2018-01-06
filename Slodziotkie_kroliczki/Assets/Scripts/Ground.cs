using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	private Rigidbody2D rigidBody;

	private void setVelocity(float f)
	{
		rigidBody.velocity = new Vector2 (0, f);
	}

	void Awake () {
		rigidBody = GetComponent<Rigidbody2D> ();
		setVelocity (GameManager.Instance.velocity);

	}
	
	// Update is called once per frame
	void OnTriggerEnter2D( Collider2D other )
	{
		if (other.gameObject.CompareTag("Destruktor"))
		{
			//Debug.Log("Kolizja z destruktorem");
			Destroy (this.gameObject);
		}
	}
	void Update () 
	{
		setVelocity (GameManager.Instance.velocity);
	}
}
