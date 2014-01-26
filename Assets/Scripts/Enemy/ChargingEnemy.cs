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
	

	public float chargingSpeed;
	public float delayBetweenSound;
	float ATTACKDISTANCE = 2;
	public SpawmEnemy spawning;

	public AudioClip[] chargerSounds;
	AudioSource chargerSource;

	void Start () 
	{
		chargerSource = GetComponent<AudioSource>();
		StartCoroutine (ChangeState (chargingState.charging));
	}
	
	public IEnumerator ChangeState(chargingState state)
	{
		StopAllCoroutines();
		switch (state) 
		{
		case chargingState.idle:
			StartCoroutine(Idle());
				break;
		case chargingState.charging:
			StartCoroutine(Charging());
			break;
		case chargingState.attacking:
			StartCoroutine(Attack());
			break;
		case chargingState.dead:
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
		StartCoroutine("DelayRepeatSound", delayBetweenSound); 
		while (Vector3.Distance(transform.position, target.position) > ATTACKDISTANCE) 
		{
			Move(chargingSpeed);
			yield return new WaitForFixedUpdate();
		}
		StopCoroutine("DelayRepeatSound");
		StartCoroutine(ChangeState (chargingState.attacking));
	}

	IEnumerator DelayRepeatSound(float delay)
	{
		while (Vector3.Distance(transform.position, target.position) > ATTACKDISTANCE)
		{
			chargerSource.clip = chargerSounds[0];
			chargerSource.Play();
			yield return new WaitForSeconds(delay);
		}
	}

	IEnumerator Attack()
	{
		while (Vector3.Distance(transform.position, target.position) < ATTACKDISTANCE) 
		{
			// Decide what to do when the player is hit
			target.GetComponent<TeamHealth>().TakeDamage(1);
			yield return null;
		}
		StartCoroutine(ChangeState (chargingState.charging));
	}

	IEnumerator Death()
	{
		GameObject temp = gameObject;
		spawning.SetPosition(ref temp, false);
		Start();
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
