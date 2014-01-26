using UnityEngine;
using System.Collections;

public class killBox : MonoBehaviour {

	public string targetTag;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == targetTag) 
		{
			GameObject temp = other.gameObject;
			// Kill object
			if (other.GetComponent<ChargingEnemy>() != null)
				StartCoroutine(other.GetComponent<ChargingEnemy>().ChangeState(chargingState.dead));
			else if (other.GetComponent<SwarmEnemyMovement>() != null)
				other.GetComponent<SwarmEnemyMovement>().SwarmSpawner.SetPosition(ref temp);
				else
					Destroy(other.gameObject);
		}
	}
}
