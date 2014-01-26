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
	public JumpListener jumplistener;

	public Animation VassalAnim;

	// Use this for initialization
	void Start () 
	{
		movType = movementType.idle;
		if (movDirection == movementDirection.left)
            rotateSpeed *= -1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(leftFoot) && movType != movementType.leftFoot)
		{
			movType = movementType.leftFoot;
			VassalAnim.PlayQueued("Walk1");
			StartCoroutine(Move(moveSpeed));
		}
		else if (Input.GetKeyDown(rightFoot) && movType != movementType.rightFoot)
		{
			movType = movementType.rightFoot;
			VassalAnim.PlayQueued("Walk2");
			StartCoroutine(Move(moveSpeed));
		}
		else if (Input.GetKeyDown(jump) && isGrounded)
		{
			StartCoroutine(PreparingToJump());
		}

		if (forcedAppliedTo.velocity.magnitude < IDLE_SPEED)
			movType = movementType.idle;
	}

	IEnumerator Move(float speed)
	{
		if (forcedAppliedTo.velocity.magnitude <= MAX_MOVE_SPEED) 
		{
            forcedAppliedTo.AddForce(transform.forward * speed + Vector3.up, ForceMode.VelocityChange);
            forcedAppliedTo.AddTorque(new Vector3(0f, rotateSpeed * 4f, 0f), ForceMode.VelocityChange);
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
