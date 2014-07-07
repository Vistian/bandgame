using UnityEngine;
using System.Collections;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour {

	/// <summary>
	/// The speed of the ship
	/// </summary>
	public Vector2 speed = new Vector2(15, 15);

	/// <summary>
	/// The movement.
	/// </summary>
	private Vector2 movement;

	/// <summary>
	/// The weapon.
	/// </summary>
	private WeaponScript weapon;

	/// <summary>
	/// The entity health.
	/// </summary>
	private HealthScript entityHealth;

	void Awake()
	{
		weapon = GetComponent<WeaponScript>();
		entityHealth = GetComponent<HealthScript>();
		
		if (entityHealth != null)
		{
			entityHealth.entityType = Globals.typePlayer;
		}
	}


	void Update()
	{
		// Movement
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		movement = new Vector2(speed.x * inputX,
		                       speed.y * inputY);

		//Shooting
		bool shoot = Input.GetButton("Fire1");
		shoot |= Input.GetButtonDown("Fire2");
		
		if (shoot)
		{
			if ((weapon != null) && (weapon.CanAttack == true))
			{
				weapon.Attack(Globals.typePlayer);
				SoundEffectsHelper.Instance.MakePlayerShotSound();
			}
		}

		// ...
		
		// 6 - Make sure we are not outside the camera bounds
		float dist = (transform.position - Camera.main.transform.position).z;
		
		float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
		leftBorder += (this.collider2D.bounds.size.x)/2;


		float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
		rightBorder -= (this.collider2D.bounds.size.x)/2;


		float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
		bottomBorder += (this.collider2D.bounds.size.y)/2;


		float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
		topBorder -= (this.collider2D.bounds.size.y)/2;


		this.transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, bottomBorder, topBorder),
			transform.position.z
			);
		
		// End of the update method
	}
	
	void FixedUpdate()
	{
		//Move 
		rigidbody2D.velocity = movement;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Entering OnCollisionEnter2D");

		bool damagePlayer = false;
		
		// Collision with enemy
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
		if (enemy != null)
		{
			// Kill the enemy
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);
			
			damagePlayer = true;
		}
		
		// Damage the player
		if (damagePlayer)
		{
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if (playerHealth != null) playerHealth.Damage(1);
		}
	}
}
