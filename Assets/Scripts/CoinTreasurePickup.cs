using UnityEngine;
using System.Collections;

public class CoinTreasurePickup : MonoBehaviour {

	public int pointsToAdd;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.GetComponent<PlayerController> () == null) //player only get coins, not enemy
			return;

		ScoreManager.AddPoints (pointsToAdd);

		Destroy (gameObject);
	}

}
