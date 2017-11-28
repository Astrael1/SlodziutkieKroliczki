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
	void Start () {
		transform.SetParent (parent);
		rigidBody = GetComponent<Rigidbody2D> ();
		rigidBody.velocity = new Vector2 (0, 10);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			Instantiate (background, next, Quaternion.identity);
			Debug.Log ("Kolizja z graczem");
		}
		else if (other.gameObject.CompareTag("Destruktor"))
		{
			Debug.Log("Kolizja z destruktorem");
			Destroy (this.gameObject);
		}
			


	}
}
