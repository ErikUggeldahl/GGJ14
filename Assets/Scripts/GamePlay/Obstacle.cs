using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public int damage = 1;

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") 
		{
			other.gameObject.GetComponent<TeamHealth>().TakeDamage(damage);
			Destroy(gameObject, 0.3f);
		}
	}
}
