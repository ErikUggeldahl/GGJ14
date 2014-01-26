using UnityEngine;
using System.Collections;

public class TeamHealth : MonoBehaviour {

    public static long score;

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
        StartCoroutine(ScoreTicker());
	}

    IEnumerator ScoreTicker()
    {
        while (currentHP > 0)
        {
            score ++;
            yield return null;
        }
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
			if (currentHP <= 0)
				Application.LoadLevel("EndMenu");
		}
	}

	IEnumerator invicibilityCooldown(float timer)
	{
		yield return new WaitForSeconds (timer);
		isInvicible = false;
	}

}
