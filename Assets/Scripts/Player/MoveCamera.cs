using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	Vector3 lookDirection;
	public Camera camera;
	public Transform target;

	float oldmouseX;
	float oldmouseY;

	float orbitDistance;

	float mouseX;
	float mouseY;
	public float rotateSpeed;

	// Use this for initialization
	void Start () 
	{
		oldmouseX = Input.GetAxis ("Mouse X");
		oldmouseY = Input.GetAxis ("Mouse Y");
		orbitDistance = Vector3.Distance(camera.transform.position, target.transform.position);
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		mouseX = Input.GetAxis ("Mouse X");

		float deltaMouseX = mouseX - oldmouseX;
		float deltaMouseY = mouseY - oldmouseY;

		transform.RotateAround(target.position, transform.up , deltaMouseX * rotateSpeed);
	}
}
