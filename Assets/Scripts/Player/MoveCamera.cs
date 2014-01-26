using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	Vector3 lookDirection;
	public Camera kingCamera;
	public Transform target;

	float oldmouseX;
	

	float mouseX;
	public float rotateSpeed;

	// Use this for initialization
	void Start () 
	{
		oldmouseX = Input.GetAxis ("Mouse X");
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		mouseX = Input.GetAxis ("Mouse X");

		float deltaMouseX = mouseX - oldmouseX;

		transform.RotateAround(target.position, transform.up , deltaMouseX * rotateSpeed);
	}
}
