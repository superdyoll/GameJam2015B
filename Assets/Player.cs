using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed = 1;
	public int projectileRange = 100;
	public GameObject projectile;
	private float timer = 0f;

	public int health;
	public int startHealth;

	public int numberOfUpgrades = 5;
	protected int upgradeNumber = 0;

	public Dino dinosaur { get; set; }
	public Sprite fluff;

	public float bloodScore = 0;
	public float bloodTarget;

	private GUIText bloodText;
	private GUITexture healthbar;

	private GUIText gameOver;

	private Sprite spriteImage;

	private int cheatcode = 0;

	void Ascend () {
		DinoSelector chooseDino = new DinoSelector ();
		dinosaur = chooseDino.ChooseRandomDino (gameObject);

		Ascend (dinosaur);
	}

	void Ascend(Dino dinosaur) {
		dinosaur.playerControlled = true;
		dinosaur.Create ();

		bloodTarget = (int)Math.Pow ((20 - dinosaur.survivability) * 1000, Level.getLevel()+1);
		startHealth = health = dinosaur.health * 20;
		UpdateHealthbar ();

		spriteImage = GetComponent<SpriteRenderer> ().sprite;

		speed = dinosaur.speed;
		projectileRange = dinosaur.getRange();
	}

	void Start() {
		gameOver = GameObject.Find ("GameOver").GetComponent<GUIText> ();
		gameOver.enabled = false;

		Ascend ();
		Level.LevelUp ();
		Vector2 S = gameObject.GetComponent<SpriteRenderer> ().sprite.bounds.size;
		gameObject.GetComponent<BoxCollider2D> ().size = S;
		gameObject.GetComponent<BoxCollider2D> ().center = new Vector2 ((S.x / 2), 0);
	}

	void Update () {
		GetMovementInput ();
		GetMouseInput ();
		UpdateCameraPosition ();

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

	}

	public Vector2 GetPosition()
	{
		return new Vector2 (transform.position.x, transform.position.y);
	}

	private void UpdateCameraPosition(){
		Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -20f);
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
		if(timer + 0.25f < Time.time)
		{
			timer = 0f;
		}
	}

	private void Fire(){
		GameObject projInst = (GameObject)Instantiate (projectile, transform.position + GetVect3Rotation (), Quaternion.identity);
		Projectile projScript = projInst.GetComponent<Projectile> ();
		projScript.Go (projectileRange, 10, 1, 6, dinosaur.weapon.damage, GetVect3Rotation (), "Enemy", dinosaur);

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
				cheatcode = 0;
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
		if (bloodScore >= bloodTarget) {
			healthbar.transform.position = new Vector2 (-0.99f, 0); //Meant to make it vanish - the bugger won't go away

			DinoGenerator dinoThing = GameObject.Find ("Main Camera").GetComponent<DinoGenerator> ();
			dinoThing.ClearDinos ();

			Ascend (origin);
		} else {
			GameOver();
		}

		Debug.Log ("Dead");
	}

	private void GameOver() {
		Time.timeScale = 0;

		gameOver.enabled = true;
	}
}
