using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

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
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * moveRate * Time.deltaTime;
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * moveRate * Time.deltaTime;
		}
		// restirct the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, maxLeft, maxRight);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
