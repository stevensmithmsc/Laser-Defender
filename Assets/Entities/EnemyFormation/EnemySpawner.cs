using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;
	public int waveBonusPoints = 225;
	
	private Vector3 direction = Vector3.left;
	private float maxLeft = -5f;
	private float maxRight = 5f;
	private ScoreKeeper scoreKeeper;

	// Use this for initialization
	void Start () {		
		spawnUntilFull();
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
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
			spawnUntilFull();
			scoreKeeper.Score(waveBonusPoints);
		}	
	}
	
	private void spawnEnemies() {
		foreach(Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	void spawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if (freePosition) {
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;	
		}
		if (NextFreePosition()) {Invoke ("spawnUntilFull", spawnDelay); }
	}
	
	private Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount == 0) {return childPositionGameObject;}
		}
		return null;
	}
	
	private bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0) {return false;}
		}
		return true;
	}
}
