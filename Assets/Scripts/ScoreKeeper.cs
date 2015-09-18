using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private int theScore = 0;
	private Text scoreTextControl;
	
	void Start(){
		scoreTextControl = GetComponent<Text>();
		Reset();
	}
	
	public void Score(int points){
		theScore += points;
		UpdateScore();
	}
	
	public void Reset(){
		theScore = 0;
		UpdateScore();
	}
	
	// Update is called once per frame
	private void UpdateScore () {
		scoreTextControl.text = theScore.ToString();
	}
}
