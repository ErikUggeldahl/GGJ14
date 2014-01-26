using UnityEngine;
using System.Collections;

public class JumpListener : MonoBehaviour {

	public float timeForJumpSync;
	public Movement[] movers;

	private float timer;
	private int jumperCount = 0;

	public IEnumerator checkJumpState(float jumpPower)
	{
		jumperCount++;
		while (timer < timeForJumpSync) 
		{
			if (jumperCount >= movers.Length)
			{
				Jump(jumpPower);
				break;
			}
			timer += Time.deltaTime;
			yield return null;
		}
		jumperCount = 0;
		timer = 0f;
	}

	void Jump(float jumpPower)
	{
		rigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.VelocityChange);
		for (int i = 0; i < movers.Length; i++)
			movers[i].isGrounded = false;
	}
}
