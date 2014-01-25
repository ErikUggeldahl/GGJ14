using UnityEngine;
using System.Collections;



enum movementType
{
	leftFoot = 0,
	rightFoot,
	jump,
	idle,
	cantMove
}

public enum movementDirection
{
	left,
	right
}
public class Movement : MonoBehaviour {

	public float jumpPower = 10f;
	public float moveSpeed = 5f;
	public float rotateSpeed = 20f;

	const float MAX_MOVE_SPEED = 20f;
	const float IDLE_SPEED = 0.3f;
	movementType movType;
	public movementDirection movDirection;

	public bool isGrounded = true;
	public KeyCode leftFoot;
	public KeyCode rightFoot;
	public KeyCode jump;
	public Rigidbody forcedAppliedTo;

	// Use this for initialization
	void Start () 
	{
		movType = movementType.idle;

		if (movDirection == movementDirection.left) 
		{
			rotateSpeed *= -1;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(leftFoot) && movType != movementType.leftFoot)
		{
			movType = movementType.leftFoot;
			StartCoroutine(Move(moveSpeed));
		}
		else if (Input.GetKeyDown(rightFoot) && movType != movementType.rightFoot)
		{
			movType = movementType.rightFoot;
			StartCoroutine(Move(moveSpeed));
		}
		else if (Input.GetKeyDown(jump) && isGrounded)
		{
			movType = movementType.jump;
			StartCoroutine(Jump(jumpPower));
		}

		if (forcedAppliedTo.velocity.magnitude < IDLE_SPEED)
			movType = movementType.idle;
	}

	IEnumerator Move(float speed)
	{
		if (forcedAppliedTo.velocity.magnitude <= MAX_MOVE_SPEED) 
		{
			forcedAppliedTo.AddForce (transform.forward * speed, ForceMode.VelocityChange);
			forcedAppliedTo.AddTorque(new Vector3(0, rotateSpeed * forcedAppliedTo.velocity.magnitude, 0), ForceMode.VelocityChange);
		}
		yield return new WaitForFixedUpdate();
	}

	IEnumerator Jump(float speed)
	{
		forcedAppliedTo.AddForce(new Vector3(0, speed, 0), ForceMode.VelocityChange);
		isGrounded = false;

		yield return new WaitForFixedUpdate();
	}
}
