using UnityEngine;
using System.Collections;

/// <summary>
/// Projectile behavior
/// </summary>
public class ShotScript : MonoBehaviour {

	/// <summary>
	/// Damage inflicted
	/// </summary>
	public int damage = 1;

	/// <summary>
	/// 0 = Player
	/// 1 = Enemy
	/// </summary>
	public int entityType; 
	
	void Update()
	{
		if (renderer.IsVisibleFrom(Camera.main) == false)
		{
			Destroy(this.gameObject);
		}
	}

}
