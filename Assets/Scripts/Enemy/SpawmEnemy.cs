using UnityEngine;
using System.Collections;
using Pool;

public class SpawmEnemy : MonoBehaviour {

	public float enemyAmount;
	public float enemySpeed;
	public float distanceBehindPlayer;
	public GameObject swarmEnemtObject;
	public Transform target;

	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < enemyAmount; i++)
		{
			GameObject minion =  (GameObject)PoolManager.Instantiate(swarmEnemtObject);
			minion.transform.position = RandomSpawnLocation(GetComponent<Collider>().bounds);
			minion.transform.rotation = transform.rotation;
			SwarmEnemyMovement movement = minion.GetComponent<SwarmEnemyMovement>();
			movement.Target = target;
			movement.SwarmSpeed = enemySpeed;
		}
	}

	void Update()
	{
		transform.position = new Vector3(target.position.x - distanceBehindPlayer, transform.position.y, transform.position.z);
	}

	Vector3 RandomSpawnLocation(Bounds limit)
	{
		Vector3 randomPos = new Vector3(Random.Range(limit.min.x,limit.max.x), transform.position.y, Random.Range(limit.min.z,limit.max.z) + transform.position.z);
		return randomPos;

	}
}
