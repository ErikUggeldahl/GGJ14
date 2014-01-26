using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public int damage = 1;
	AudioSource impactSource;

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") 
		{
			other.gameObject.GetComponent<TeamHealth>().TakeDamage(damage);
			Destroy(gameObject, 0.3f);
		}
		else if (other.gameObject.tag == "Enemy")
		{
			GameObject temp = other.gameObject;
			// Kill object
			if (other.gameObject.GetComponent<ChargingEnemy>() != null)
				StartCoroutine(other.gameObject.GetComponent<ChargingEnemy>().ChangeState(chargingState.dead));
			else if (other.gameObject.GetComponent<SwarmEnemyMovement>() != null)
				other.gameObject.GetComponent<SwarmEnemyMovement>().SwarmSpawner.SetPosition(ref temp, true);
			else
				Destroy(other.gameObject);
		}
	}
}
