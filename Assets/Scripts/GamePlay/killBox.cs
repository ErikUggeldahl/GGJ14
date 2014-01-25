using UnityEngine;
using System.Collections;

public class killBox : MonoBehaviour {

	public string targetTag;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == targetTag) 
		{
			// Kill object
			if (other.GetComponent<ChargingEnemy>() != null)
				StartCoroutine(other.GetComponent<ChargingEnemy>().ChangeState(chargingState.dead));
			else
				Destroy(other.gameObject);
		}
	}
}
