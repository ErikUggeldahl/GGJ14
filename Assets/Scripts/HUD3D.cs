using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD3D : MonoBehaviour {

	public List<GameObject> hPHudItems;
	public float objectMoveSpeed;
	public Vector3 deplacement;

	public void MoveHPHudItem(int damage)
	{
		if (hPHudItems.Count == 0)
			return;
		StartCoroutine(SmoothMove(hPHudItems[0].transform));
	}

	IEnumerator SmoothMove(Transform objectToMove)
	{
		Vector3 destination = objectToMove.position + deplacement;
		// Initialize values for the lerping process
		float startTime = 0f;
		float distance = Vector3.Distance(objectToMove.position, destination);
		float totalTime = distance / objectMoveSpeed;
		float ratio = 0;
	
		// Store the inital value as the start location and rotation
		Vector3 initPos = objectToMove.position;
	
		while (ratio < 1) 
		{
			ratio = startTime / totalTime;
			startTime += Time.deltaTime;
		
			// Move and rotate the camera with a smooth curve
			objectToMove.position = Vector3.Slerp (initPos, destination, Mathf.SmoothStep (0f, 1f, Mathf.SmoothStep (0f, 1f, ratio)));
			yield return null;
		}
		hPHudItems.RemoveAt(0);

	}
}
