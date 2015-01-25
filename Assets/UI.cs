using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {
	private static GUIText bloodText;
	private static GUITexture healthbar;
	
	private static GUIText gameOver;
	private static GUIText endScore;


	// Use this for initialization
	void Start () {
		gameOver = GameObject.Find ("GameOver").GetComponent<GUIText> ();
		gameOver.enabled = false;
		endScore = GameObject.Find ("EndScore").GetComponent<GUIText> ();
		endScore.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
