﻿using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public Texture2D crosshairImage;

	public float speed = 1;
	public int projectileRange = 100;
	public GameObject projectile;
	public Material sovietRed, isaBlue;
	private float timer = 0f;

	public int health, startHealth;

	public int numberOfUpgrades = 5;
	protected int upgradeNumber = 0;

	public Dino dinosaur { get; set; }
	public Sprite fluff;

	public float bloodScore = 0;
	public float bloodTarget;

	private GUIText bloodText;
	private GUITexture healthbar;

	private GUIText gameOver;
	private GUIText endScore;
	private GUIText restartNote;

	private Sprite spriteImage;

	private int cheatcode = 0;

	public GameObject overlay, pauseOverlay;
	private Boolean onPause = false, enterPause = true;

	Texture2D cursorTexture;
	CursorMode cursorMode  = CursorMode.Auto;
	Vector2 hotSpot = Vector2.zero;

	void Ascend () {
		DinoSelector chooseDino = new DinoSelector ();
		dinosaur = chooseDino.ChooseRandomDino (gameObject);
		Debug.Log ("Dino to be passed:" + dinosaur.gameObject.name);
		Ascend (dinosaur);
	}

	void Ascend(Dino dinosaur) {
		Debug.Log ("New Dino: " + dinosaur.gameObject.name);
		dinosaur.playerControlled = true;
		dinosaur.Create ();

		bloodTarget = ((20 - dinosaur.survivability) * 15 * (float)Math.Pow (Level.getLevel() + 1, 2f)) * 666;
		startHealth = health = dinosaur.health * 20;
		UpdateHealthbar ();

		spriteImage = dinosaur.GetComponent<SpriteRenderer> ().sprite;
		SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
		sr.sprite = spriteImage;
		
		speed = dinosaur.speed;
		projectileRange = dinosaur.getRange();
	}

	void Start() {
		Screen.showCursor = false;

		overlay.SetActive (true);
		pauseOverlay.SetActive (true);

		gameOver = GameObject.Find ("GameOver").GetComponent<GUIText> ();
		gameOver.enabled = false;
		endScore = GameObject.Find ("EndScore").GetComponent<GUIText> ();
		endScore.enabled = false;
		restartNote = GameObject.Find ("RestartNote").GetComponent<GUIText> ();
		restartNote.enabled = false;

		Ascend ();
		Level.LevelUp ();

		/*Vector2 S = gameObject.GetComponent<SpriteRenderer> ().sprite.bounds.size;
		gameObject.GetComponent<BoxCollider2D> ().size = S;
		gameObject.GetComponent<BoxCollider2D> ().center = new Vector2 ((S.x / 2), 0);*/
	}

	void Update () {
		GetMovementInput ();
		GetMouseInput ();
		UpdateCameraPosition ();
		CheckInBounds ();

		bloodText = GameObject.Find("BloodScore").GetComponent<GUIText> ();

		Level.Tick ();

		bloodText.text = bloodScore + " BUCKETS OF BLOOD SPILLED / " + bloodTarget;

		if (health <= 0) {
			if(upgradeNumber <= numberOfUpgrades){
				Ascend();
			}else{
				GameOver();
			}
		}

		if (enterPause) {
			if (onPause) {
				Instantiate (pauseOverlay);
			} else {
				Instantiate (overlay);
			}
			enterPause = false;
		}
	}

	void OnGUI()
	{
		//Draw on current mouse position
		float xMin = Screen.width - (Screen.width - Input.mousePosition.x) - (crosshairImage.width / 2);
		float yMin = (Screen.height - Input.mousePosition.y) - (crosshairImage.height / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
	}

	public Vector2 GetPosition()
	{
		return new Vector2 (transform.position.x, transform.position.y);
	}

	private void UpdateCameraPosition(){
		Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -15f);
	}

	private Vector3 GetVect3Rotation(){
		Vector3 mousePosition = new Vector3(Input.mousePosition.x - Screen.width / 2f, Input.mousePosition.y - Screen.height / 2f, 0f);
		mousePosition.Normalize ();
		return mousePosition;
	}

	private void GetMouseInput(){
		Vector3 mousePosition = GetVect3Rotation ();
		float rotZ = Mathf.Atan2 (mousePosition.y, mousePosition.x) * Mathf.Rad2Deg - 90; // find the angle in degrees
		transform.rotation = Quaternion.Euler (0f, 0f, rotZ + 90f);

		if (Input.GetMouseButton (0) && timer == 0f) {
			timer = Time.time;
			Fire();
		}

		if(timer + 0.2f < Time.time)
		{
			timer = 0f;
		}
	}

	private void Fire(){
		for(int i = 0; i < dinosaur.weapon.numberOfProjectiles; ++i)
		{
			GameObject projInst = (GameObject)Instantiate (projectile, transform.position + GetVect3Rotation (), Quaternion.identity);
			Projectile projScript = projInst.GetComponent<Projectile> ();
			Vector3 pos = new Vector3 (UnityEngine.Random.Range (GetVect3Rotation().x - 0.2f, GetVect3Rotation().x + 0.2f), UnityEngine.Random.Range (GetVect3Rotation().y -0.2f, GetVect3Rotation().y +0.2f), 0f);
			projScript.Go (projectileRange, 10, 1, dinosaur.weapon.rof, dinosaur.weapon.damage, pos.normalized, "Enemy", dinosaur);
			projInst.GetComponent<TrailRenderer> ().material = isaBlue;
		}
	}

	private void CheckInBounds(){
		if(transform.position.y > 50f){
			transform.position = new Vector3(transform.position.x, 50f, 0f);
		}
		if(transform.position.y < 0f){
			transform.position = new Vector3(transform.position.x, 0f, 0f);			
		}
		if(transform.position.x > 50f){
			transform.position = new Vector3(50f, transform.position.y, 0f);			
		}
		if(transform.position.x < 0f){
			transform.position = new Vector3(0f, transform.position.y, 0f);			
		}
	}
	
	private void GetMovementInput(){
		if (Input.GetKey ("w") || Input.GetKey ("up")) {
			if(gameObject.transform.position.y < 50f){
				gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.1f * speed);
			}
			else{
				gameObject.transform.position = new Vector2(gameObject.transform.position.x, 50f);
			}
			if(cheatcode == 1 || cheatcode == 0){
				cheatcode ++;
			}
		}
		if (Input.GetKey ("a") || Input.GetKey ("left")) {
			if(gameObject.transform.position.x > 0f){
				gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.1f * speed, gameObject.transform.position.y);
			}
			else{
				gameObject.transform.position = new Vector2(0f, gameObject.transform.position.y);
			}
			if (cheatcode == 4 || cheatcode == 6){
				cheatcode ++;
			}
		}
		if (Input.GetKey ("s") || Input.GetKey ("down")) {
			if(gameObject.transform.position.y > 0f){
				gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f * speed);
			}
			else{
				gameObject.transform.position = new Vector2(gameObject.transform.position.x, 0f);
			}
			if (cheatcode == 2 || cheatcode == 3){
				cheatcode ++;
			}
		}
		if (Input.GetKey ("d") || Input.GetKey ("right")) {
			if(gameObject.transform.position.x < 50f){
				gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.1f * speed, gameObject.transform.position.y);
			}
			else{
				gameObject.transform.position = new Vector2(50f, gameObject.transform.position.y);
			}
			if (cheatcode == 5 || cheatcode == 7){
				cheatcode ++;
			}
		}
		if (Input.GetKey ("b")) {
			if (cheatcode == 8){
				cheatcode ++;
			}
		}

		if (Input.GetKey ("a")) {
			if (cheatcode == 9){
				Debug.Log("Konami Code FTW");
				SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
				sr.sprite = fluff;
				cheatcode = 0;
			}
		}

		if (Input.GetKey ("escape")) {
			Application.Quit();
		}

		if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey ("enter") || Input.GetKey ("return")){ 
			if (onPause){
				Time.timeScale = 1;
				Level.setLevel(0);

				bloodScore = bloodTarget = 0;

				Application.LoadLevel (Application.loadedLevelName);
			}
		}
	}

	private void UpdateHealthbar() {
		healthbar = GameObject.Find ("HealthBar").GetComponent<GUITexture> ();

		float percentage = (float) health / startHealth;
		float move = percentage - 1;

		healthbar.transform.position = new Vector2(move, healthbar.transform.position.y);
	}

	public void Damage(int amount, Dino origin) {
		health -= amount;

		if (health <= 0) {
			Kill (origin);
		} else {
			UpdateHealthbar ();
		}
	}

	private void Kill(Dino origin){
		Debug.Log ("orgin: " + origin.gameObject.name);
		if (bloodScore >= bloodTarget) {
			healthbar.transform.position = new Vector2 (-0.99f, 0); //Meant to make it vanish - the bugger won't go away

			//DinoGenerator dinoThing = GameObject.Find ("Main Camera").GetComponent<DinoGenerator> ();
			//dinoThing.ClearDinos ();

			Ascend (origin);
		} else {
			GameOver();
		}

		Debug.Log ("Dead");
	}

	private void GameOver() {
		onPause = true;
		enterPause = true;

		Time.timeScale = 0;
		gameOver.enabled = true;
		endScore.enabled = true;
		restartNote.enabled = true;
		endScore.text = "SCORE: " + bloodScore;
	}
}
