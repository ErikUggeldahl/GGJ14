using UnityEngine;
using System.Collections;

public class FallDownLogic : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			if (other.GetComponent<ChargingEnemy>() != null)
				StartCoroutine(other.GetComponent<ChargingEnemy>().ChangeState(chargingState.dead));
			else if (other.GetComponent<SwarmEnemyMovement>() != null)
				other.GetComponent<SwarmEnemyMovement>().IsActive = false;

			other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		}
		else if(other.tag == "Player")
		{
			Application.LoadLevel("EndMenu");
		}
	}
}
