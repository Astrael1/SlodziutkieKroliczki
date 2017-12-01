﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector2.right * Input.GetAxis ("Horizontal") * speed * Time.deltaTime);
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("chmurka"))
			{
				Destroy(other.gameObject);
			GameManager.Instance.velocity -= GameManager.Instance.chmurkaSila;
			}
	}
}
