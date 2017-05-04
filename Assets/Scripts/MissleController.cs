using UnityEngine;
using System.Collections;

public class MissleController : MonoBehaviour {

	public float speed;

	public PlayerController player;

	public GameObject enemyDeathEffect;

	public GameObject impactEffect;

	public int pointsForKill;

	public int damageToGive;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();

		transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);

		if (player.transform.localScale.x < 0){
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			speed = - speed;
		
		}
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = new Vector2 (speed, GetComponent<Rigidbody2D>().velocity.y);﻿
	}

	void OnTriggerEnter2D(Collider2D other)
	{	
		if (other.tag == "Enemy") 
		{
			//Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
			//Destroy (other.gameObject);
			//ScoreManager.AddPoints(pointsForKill);

			other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
		}

		Instantiate (impactEffect, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
