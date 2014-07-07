using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	private bool hasSpawn;
	private WeaponScript weapon;
	private HealthScript entityHealth;
	private MoveScript moveScript;

	// Use this for initialization
	void Start () {
		hasSpawn = false;
		collider2D.enabled = false;
		moveScript.enabled = false;
		weapon.enabled = false;
	}

	void Awake()
	{
		weapon = GetComponent<WeaponScript>();
		entityHealth = GetComponent<HealthScript>();
		moveScript = GetComponent<MoveScript>();

		if (entityHealth != null)
		{
			// false because the player is not an enemy
			entityHealth.entityType = Globals.typeEnemy;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (hasSpawn == false)
		{
			if (renderer.IsVisibleFrom(Camera.main) == true)
			{
				spawn();
			}
		}
		else
		{
			if ((weapon != null) && (weapon.CanAttack == true))
			{
				weapon.Attack(Globals.typeEnemy);
				SoundEffectsHelper.Instance.MakeEnemyShotSound();
			}

			// 4 - Out of the camera ? Destroy the game object.
			if (renderer.IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}
	}

	// 3 - Activate itself.
	private void spawn()
	{
		hasSpawn = true;
		// Enable everything
		// -- Collider
		collider2D.enabled = true;
		// -- Moving
		moveScript.enabled = true;
		// -- Shooting
		weapon.enabled = true;
	}
}
