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

	public float timeBeforeRespawn;
	Bounds ourDimension;

	SpawmEnemy swarmSpawner;
	public SpawmEnemy SwarmSpawner
	{
		set { swarmSpawner = value; }
		get { return swarmSpawner; }
	}

	// Use this for initialization
	void Start () 
	{
		ourDimension = GetComponent<Collider>().bounds;
		StartCoroutine(Seek());
	}
	
	// Update is called once per frame
	public IEnumerator Seek() 
	{
		while (isActive) 
		{
			Move(swarmSpeed);
			yield return new WaitForFixedUpdate();
		}

		rigidbody.AddTorque(50f,0,0, ForceMode.VelocityChange);
		yield return new WaitForSeconds (timeBeforeRespawn);
		GameObject temp = gameObject;
		swarmSpawner.SetPosition (ref temp);
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
