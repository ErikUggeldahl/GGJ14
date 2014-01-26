using UnityEngine;
using System.Collections;

public class SpawmEnemy : MonoBehaviour {

	public int enemyAmount;
	public float enemySpeed;
	public float distanceBehindPlayer;
	public GameObject swarmEnemtObject;
	public Transform target;
	public float timeBeforeRespawn;
	BoxCollider ourCollider;

	// Use this for initialization
	void Start () 
	{
		ourCollider = GetComponent<BoxCollider> ();
		SpawnSwarmAgent(enemyAmount);
	}

	public void SpawnSwarmAgent(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			GameObject minion =  (GameObject)Instantiate(swarmEnemtObject);
			SetPosition(ref minion, true);
		}
	}

	public void SetPosition(ref GameObject minion, bool valueSetup)
	{
		minion.transform.position = RandomSpawnLocation(ourCollider.bounds);
		minion.transform.rotation = transform.rotation;
		if (valueSetup)
			SetupValue(minion);
	}

	public void SetupValue(GameObject minion)
	{
		minion.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

		if (minion.GetComponent<SwarmEnemyMovement> () != null) 
		{
			SwarmEnemyMovement movement = minion.GetComponent<SwarmEnemyMovement> ();
			movement.timeBeforeRespawn = timeBeforeRespawn;
			movement.SwarmSpawner = this;
			movement.Target = target;
			movement.SwarmSpeed = enemySpeed;
			movement.IsActive = true;
			StartCoroutine (movement.Seek ());
		}
		else if (minion.GetComponent<ChargingEnemy> () != null)
		{
			ChargingEnemy movement = minion.GetComponent<ChargingEnemy> ();
			movement.target = target;
			movement.spawning = this;
		}
	}
	
	void Update()
	{
		// Track player
		transform.position = new Vector3(target.position.x, transform.position.y, target.position.z - distanceBehindPlayer);
	}

	Vector3 RandomSpawnLocation(Bounds limit)
	{
		return new Vector3(Random.Range(limit.min.x,limit.max.x), transform.position.y, Random.Range(limit.min.z,limit.max.z));
	}
}
