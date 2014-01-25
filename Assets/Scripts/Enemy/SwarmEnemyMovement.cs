using UnityEngine;
using System.Collections;

public class SwarmEnemyMovement : MonoBehaviour {


	bool isActive = true;
	public bool IsActive
	{
		set { isActive = value; }
	}

	Transform target;
	public Transform Target
	{
		set{ target = value; }
	}
	float swarmSpeed;
	public float SwarmSpeed
	{
		set{ swarmSpeed = value; }
	}
	Bounds ourDimension;

	// Use this for initialization
	void Start () 
	{
		ourDimension = GetComponent<Collider>().bounds;
		StartCoroutine(Seek());
	}
	
	// Update is called once per frame
	IEnumerator Seek() 
	{
		while (isActive) 
		{
			Move(swarmSpeed);
			yield return new WaitForFixedUpdate();
		}
		Debug.Log ("Dead");
	}

	void Move(float speed)
	{
		Vector3 temp = target.position - transform.position;
		temp.y = 0;
		transform.rotation = Quaternion.LookRotation (temp);
		if (rigidbody.velocity.magnitude <= speed)
			rigidbody.AddForce (transform.forward * speed, ForceMode.VelocityChange);
	}
}
