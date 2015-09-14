using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float health = 150;
	public GameObject laserPrefab;
	public float projectileSpeed = 10;
	public float shotsPerSecond = 0.5f;
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			if(health <= 0) {
				Destroy(gameObject);
			}
			missile.Hit(); 				
		}
	}
	
	void Update () {
	
	}
}
