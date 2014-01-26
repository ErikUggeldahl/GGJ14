using UnityEngine;
using System.Collections;

enum KingState
{
	idle,
	attacking,
	ducking
}

public class SwingSword : MonoBehaviour {

	KingState kState;
	public float delayTimer;
	public float swingTime;
	public Collider killBox;
	float timer;
	public Animation liegeAnim;
	public CapsuleCollider capCollider;


	void Start()
	{
		killBox.enabled = false;
	}

	// Update is called once per frame
	void Update () 
	{
		//Triggered Swinging
		if (Input.GetMouseButtonDown(1) && kState != KingState.ducking) 
		{
			kState = KingState.ducking;
			StartCoroutine(Dodge());
		}
		else  if (Input.GetMouseButtonDown(0) && kState == KingState.idle) 
		{
			StartCoroutine(activateSword(swingTime));
			timer = delayTimer;
			liegeAnim.Stop();
			liegeAnim.PlayQueued("Attack");
			kState = KingState.attacking;
		}
		//Cooldown
		else if (timer >= 0)
		{
			timer -= Time.deltaTime;
		}
		//Cooldown over
		else
		{
			kState = KingState.idle;
			timer = 0;
		}
	}

	IEnumerator Dodge()
	{
		liegeAnim.Stop();
		liegeAnim.PlayQueued("Duck");

		while(Input.GetMouseButton(1))
		{
			capCollider.center = new Vector3(capCollider.center.x, 2.0f, capCollider.center.z);
			yield return null;
		}
		capCollider.center = new Vector3(capCollider.center.x, 2.5f, capCollider.center.z);
		liegeAnim.Stop();
		liegeAnim.PlayQueued("Idle");
	}

	IEnumerator activateSword(float time)
	{
		killBox.enabled = true;
		yield return new WaitForSeconds (time);
		liegeAnim.PlayQueued("Idle");
		killBox.enabled = false;
	}
}
