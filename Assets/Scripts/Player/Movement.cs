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

enum JumpState
{
	none = 0,
	prepared,
	jumping
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
	JumpState jumpType;
	public movementDirection movDirection;

	public bool isGrounded = true;
	public KeyCode leftFoot;
	public KeyCode rightFoot;
	public KeyCode jump;
	public Rigidbody forcedAppliedTo;
	public JumpListener jumplistener;

	// Use this for initialization
	void Start () 
	{
		movType = movementType.idle;
		jumpType = JumpState.none;
		if (movDirection == movementDirection.left) 
			rotateSpeed *= -1;
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
			jumpType = JumpState.prepared;
			StartCoroutine(PreparingToJump());
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

	IEnumerator PreparingToJump()
	{
		bool jumping = false;
		while (!jumping) 
		{
			// Animation of preparing a jump
			if (Input.GetKeyUp(jump))
				jumping = true;

			yield return null;
		}
		// Animation of a jump
		// Check if both are jumping
		StartCoroutine (jumplistener.checkJumpState (jumpPower));
	}

	IEnumerator Jump(float speed)
	{


		yield return new WaitForFixedUpdate();
	}
}
