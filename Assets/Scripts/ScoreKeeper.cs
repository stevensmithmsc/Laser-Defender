using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static int theScore = 0;
	private Text scoreTextControl;
	
	void Start(){
		scoreTextControl = GetComponent<Text>();
		Reset();
		UpdateScore();
	}
	
	public void Score(int points){
		theScore += points;
		UpdateScore();
	}
	
	public static void Reset(){
		theScore = 0;
	}
	
	// Update is called once per frame
	private void UpdateScore () {
		scoreTextControl.text = theScore.ToString();
	}
}
