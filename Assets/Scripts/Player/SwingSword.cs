using UnityEngine;
using System.Collections;

public class SwingSword : MonoBehaviour {

	bool isSwinging = false;
	public float delayTimer;
	public float swingTime;
	public Collider killBox;
	float timer;

	void Start()
	{
		killBox.enabled = false;
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
		killBox.enabled = true;
		yield return new WaitForSeconds (time);
		killBox.enabled = false;
	}
}
