using UnityEngine;
using System.Collections;

public class Grounded : MonoBehaviour {

	public Movement mover;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ground")
		{
			mover.isGrounded = true;
		}
	}
}
