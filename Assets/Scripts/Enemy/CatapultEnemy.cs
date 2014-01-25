using UnityEngine;
using System.Collections;

public class CatapultEnemy : MonoBehaviour {

	public float throwForce;
	public float throwAngle;
	bool isGrounded = false;

	// Use this for initialization
	void Start () 
	{
		rigidbody.AddForce (throwForce * Mathf.Sin(throwAngle * Mathf.Deg2Rad), throwForce * Mathf.Cos(throwAngle * Mathf.Deg2Rad), 0, ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (rigidbody.angularVelocity.magnitude < 6 && !isGrounded)
			rigidbody.AddRelativeTorque (60, 70, 100, ForceMode.VelocityChange);
	}


	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Ground")
			isGrounded = true;
	}
}
