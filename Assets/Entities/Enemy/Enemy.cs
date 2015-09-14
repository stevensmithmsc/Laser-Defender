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
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {Fire ();}
	}
	
	void Fire() {
		Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
		GameObject beam = Instantiate(laserPrefab, startPosition, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0, -projectileSpeed, 0);
	}
}
