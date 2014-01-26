using UnityEngine;
using System.Collections;

public class TeamHealth : MonoBehaviour {

	public int startingHP;
	int currentHP;

	bool isInvicible = false;
	public float invicibilityTimer;

	public HUD3D hud;

	public AudioSource impactSource;
	public AudioClip impactSound;
	// Use this for initialization
	void Start () 
	{
		currentHP = startingHP;
	}

	public void TakeDamage(int damage)
	{
		if (!isInvicible) 
		{
			impactSource.clip = impactSound;
			impactSource.Play();
			currentHP -= damage;
			Debug.Log(currentHP);
			isInvicible = true;
			hud.MoveHPHudItem(startingHP - currentHP);
			StartCoroutine(invicibilityCooldown(invicibilityTimer));
		}
	}

	IEnumerator invicibilityCooldown(float timer)
	{
		yield return new WaitForSeconds (timer);
		isInvicible = false;
	}

}
