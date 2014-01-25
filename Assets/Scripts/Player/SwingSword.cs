using UnityEngine;
using System.Collections;

public class SwingSword : MonoBehaviour {

	bool isSwinging = false;
	public float delayTimer;
	public float swingTime;
	float timer;

	void Start()
	{
		collider.enabled = false;
	}

	// Update is called once per frame
	void Update () 
	{
		//Triggered Swinging
		if (Input.GetMouseButtonDown (0) && !isSwinging) 
		{
			StartCoroutine(activateSword(swingTime));
			timer = delayTimer;
			isSwinging = true;
		}
		//Cooldown
		else if (timer >= 0)
		{
			timer -= Time.deltaTime;
		}
		//Cooldown over
		else
		{
			timer = 0;
			isSwinging = false;
		}
	}

	IEnumerator activateSword(float time)
	{
		collider.enabled = true;
		yield return new WaitForSeconds (time);
		collider.enabled = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (isSwinging && other.tag == "Enemy") 
		{
			// Kill
			Destroy(other.gameObject);
		}
	}
}
