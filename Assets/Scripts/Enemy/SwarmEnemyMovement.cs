using UnityEngine;
using System.Collections;

public class SwarmEnemyMovement : MonoBehaviour {


	public float swarmSoundRandomValMin;
	public float swarmSoundRandomValMax;
	public AudioClip[] swarmClips;
	public AudioSource swarmSource;

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

	SpawmEnemy swarmSpawner;
	public SpawmEnemy SwarmSpawner
	{
		set { swarmSpawner = value; }
		get { return swarmSpawner; }
	}

	public Animation swarmAnim;

	const float SWARM_ATTACK_RANGE = 0.3f;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(Seek());
	}
	
	// Update is called once per frame
	public IEnumerator Seek() 
	{
		swarmAnim.PlayQueued("Walk");
		StartCoroutine (RandomDelaySoundLoop(swarmSoundRandomValMin, swarmSoundRandomValMax));
		while (isActive) 
		{
			Move(swarmSpeed);
			if (Vector3.Distance(transform.position, target.position) < SWARM_ATTACK_RANGE)
				Attack(1);
			yield return new WaitForFixedUpdate();
		}

		swarmAnim.Stop();
		swarmAnim.PlayQueued("Splay");
		rigidbody.AddTorque(50f,0,0, ForceMode.VelocityChange);
		yield return new WaitForSeconds (timeBeforeRespawn);
		GameObject temp = gameObject;
		swarmSpawner.SetPosition (ref temp, true);
	}

	IEnumerator RandomDelaySoundLoop(float min, float max)
	{
		while (isActive) 
		{
			yield return new WaitForSeconds(Random.Range(min,max));
			if (Random.value > 0.90f)
			{
				swarmSource.clip = swarmClips[0];
				swarmSource.Play();
			}
		}

	}

	void Attack(int damage)
	{
		target.GetComponent<TeamHealth>().TakeDamage(damage);
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
