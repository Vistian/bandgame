using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	/// <summary>
	/// Total hitpoints
	/// </summary>
	public int hp = 1;

	//test
	
	/// <summary>
	/// 0 = player  
	/// 1= enemy
	/// </summary>
	public int entityType;
	
	/// <summary>
	/// Inflicts damage and check if the object should be destroyed
	/// </summary>
	/// <param name="damageCount"></param>
	public void Damage(int damageCount)
	{
		hp -= damageCount;
		
		if (hp <= 0)
		{
			// Dead!
			Destroy(gameObject);
			SoundEffectsHelper.Instance.MakeExplosionSound();
			GameObject fireEffect = (GameObject)Instantiate(Resources.Load ("Fire Effect"));
			GameObject smokeEffect = (GameObject)Instantiate(Resources.Load ("Smoke Effect"));
			fireEffect.transform.position = this.transform.position;
			smokeEffect.transform.position = this.transform.position;
			Destroy (fireEffect, fireEffect.particleSystem.startLifetime);
			Destroy(smokeEffect, smokeEffect.particleSystem.startLifetime);
		}
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.entityType != entityType)
			{
				Damage(shot.damage);
				
				// Destroy the shot
				Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
			}
		}
	}
}
