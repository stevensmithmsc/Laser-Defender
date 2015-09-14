using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject laserPrefab;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float moveRate = 5f;
	public float padding = 1f;
	public float health = 250;
	
	private float maxLeft = -5f;
	private float maxRight = 5f;
	
	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		maxLeft = leftmost.x + padding;
		maxRight = rightmost.x - padding;
	}
	
	void Fire(){
		Vector3 offset = new Vector3(0, 1, 0);
		GameObject beam = Instantiate(laserPrefab, this.transform.position + offset, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0,projectileSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, firingRate);
		} else if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke ("Fire");
		} else if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * moveRate * Time.deltaTime;
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * moveRate * Time.deltaTime;
		}
		// restirct the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, maxLeft, maxRight);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
//			Debug.Log ("player Collided with Missile");
			health -= missile.GetDamage();
			if(health <= 0) {
				Destroy(gameObject);
			}
			missile.Hit(); 		
		}
	}
}
