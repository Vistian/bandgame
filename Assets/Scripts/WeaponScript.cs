using UnityEngine;
using System.Collections;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour {
	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 0.25f;
	
	private float shootCooldown;
	
	void Start()
	{
		shootCooldown = 0f;
	}
	
	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}
	
	/// <summary>
	/// Create a new projectile if possible
	/// </summary>
	public void Attack(int entityType)
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;

			GameObject shotPrefab;
			if (entityType == Globals.typePlayer)
			{
				shotPrefab = (GameObject)Instantiate(Resources.Load ("Laser"));
			}
			else
			{
				shotPrefab = (GameObject)Instantiate(Resources.Load ("Enemy_Laser"));
			}

			// Assign position
			shotPrefab.transform.position = this.transform.position;

			// The is enemy property
			ShotScript shot = shotPrefab.gameObject.GetComponent<ShotScript>();
			if (shot != null)
			{
				shot.entityType = entityType;
			}
			
			// Make the weapon shot always towards it
			MoveScript move = shotPrefab.gameObject.GetComponent<MoveScript>();
			if (move != null)
			{
				if(entityType == 0)
				{
					move.direction = new Vector2(1, 0);
				}
				else
				{
					move.direction = new Vector2(-1, 0);
				}
			}
		}
	}
	
	/// <summary>
	/// Is the weapon ready to create a new projectile?
	/// </summary>
	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}
