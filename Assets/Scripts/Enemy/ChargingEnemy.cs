using UnityEngine;
using System.Collections;

public enum chargingState
{
	idle,
	charging,
	attacking,
	dead
}

public class ChargingEnemy : MonoBehaviour {

	public Transform target;
	Transform Target
	{
		get { return target; }
		set { target = value; }
	}


	chargingState state;

	public float chargingSpeed;
	float ATTACKDISTANCE = 2;

	void Start () 
	{
		StartCoroutine (ChangeState (chargingState.charging));
	}
	
	public IEnumerator ChangeState(chargingState state)
	{
		StopAllCoroutines();
		switch (state) 
		{
		case chargingState.idle:
			this.state = chargingState.idle;
			StartCoroutine(Idle());
				break;
		case chargingState.charging:
			this.state = chargingState.charging;
			StartCoroutine(Charging());
			break;
		case chargingState.attacking:
			this.state = chargingState.attacking;
			StartCoroutine(Attack());
			break;
		case chargingState.dead:
			this.state = chargingState.dead;
			StartCoroutine(Death());
			break;
		}
		yield return null;
	}

	IEnumerator Idle()
	{
		while (target == null) 
		{
			yield return new WaitForSeconds(2f);
		}
		StartCoroutine(ChangeState (chargingState.charging));
	}

	IEnumerator Charging()
	{
		while (Vector3.Distance(transform.position, target.position) > ATTACKDISTANCE) 
		{
			Move(chargingSpeed);
			yield return new WaitForFixedUpdate();
		}
		StartCoroutine(ChangeState (chargingState.attacking));
	}

	IEnumerator Attack()
	{
		while (Vector3.Distance(transform.position, target.position) < ATTACKDISTANCE) 
		{
			// Decide what to do when the player is hit
			yield return null;
		}
		StartCoroutine(ChangeState (chargingState.charging));
	}

	IEnumerator Death()
	{
		Destroy (gameObject);
		yield return null;
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
