using UnityEngine;
using System.Collections;

[RequireComponent( typeof(SpawmEnemy))]

public class IncrementalChargerSpawn : MonoBehaviour {

	public float timeBetweenSpawns;
	SpawmEnemy spawnEnemy;

	// Use this for initialization
	void Start () 
	{
		spawnEnemy = GetComponent<SpawmEnemy>();
		StartCoroutine(Spawn ());
	}

	IEnumerator Spawn()
	{
		while (enabled)
		{
			yield return new WaitForSeconds(timeBetweenSpawns);
			spawnEnemy.SpawnSwarmAgent(1);
		}
	}
}
