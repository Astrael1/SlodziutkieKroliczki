using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

	private Rigidbody2D rigidBody;

	public GameObject background;

	[SerializeField]
	private Transform parent;

	public Vector2 next
	{
		get
		{
			return new Vector2 (transform.position.x, transform.position.y - GetComponent<SpriteRenderer> ().bounds.size.y);
		}
	}

	// Use this for initialization
	void Awake () {
		transform.SetParent (parent);
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
		

		if (other.gameObject.CompareTag ("Player")) 
		{
			Instantiate (background, next, Quaternion.identity);
			//Debug.Log ("Kolizja z graczem");
		}
		else if (other.gameObject.CompareTag("Destruktor"))
		{
			//Debug.Log("Kolizja z destruktorem");
			Destroy (this.gameObject);
		}
			


	}

	private void setVelocity(float f)
	{
		rigidBody.velocity = new Vector2 (0, f);
	}
}
