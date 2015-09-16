using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	
	private Vector3 direction = Vector3.left;
	private float maxLeft = -5f;
	private float maxRight = 5f;

	// Use this for initialization
	void Start () {		
		spawnEnemies();
		
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		maxLeft = leftmost.x + (width/2);
		maxRight = rightmost.x - (width/2);
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += direction * speed * Time.deltaTime;
		
		if (transform.position.x > maxRight) {
			direction = Vector3.left;
		} else if (transform.position.x < maxLeft) {
			direction = Vector3.right;
		}
		
		if(AllMembersDead()){
			spawnEnemies();
		}	
	}
	
	private void spawnEnemies() {
		foreach(Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0) {return false;}
		}
		return true;
	}
}
