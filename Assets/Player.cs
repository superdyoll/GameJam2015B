using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed = 1;
	public int projectileRange = 100;
	public GameObject projectile;
	private float timer = 0f;

	public int health = 100;

	public int numberOfUpgrades = 5;
	protected int upgradeNumber = 0;

	public Dino dinosaur { get; set; }

	public float bloodScore = 0;
	public float bloodTarget;
	private GUIText bloodText;

	void Ascend () {
		DinoSelector chooseDino = new DinoSelector ();
		dinosaur = chooseDino.ChooseRandomDino (gameObject);

		Ascend (dinosaur);
	}

	void Ascend(Dino dinosaur) {
		dinosaur.playerControlled = true;
		dinosaur.Create ();

		bloodTarget = (int)Math.Pow ((20 - dinosaur.survivability) * 1000, Level.getLevel()+1);

		speed = dinosaur.speed;
		projectileRange = dinosaur.getRange();
	}

	void Start() {
		Ascend ();
		Level.LevelUp ();
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
				Kill();
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
		projScript.Go (projectileRange, 10, 1, 10, dinosaur.weapon.damage, GetVect3Rotation (), "Enemy");

	}

	private void GetMovementInput(){
		if (Input.GetKey ("w")) {
			if(gameObject.transform.position.y < 50f){
				gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.1f * speed);
			}
			else{
				gameObject.transform.position = new Vector2(gameObject.transform.position.x, 50f);
			}
		}
		if (Input.GetKey ("a")) {
			if(gameObject.transform.position.x > 0f){
				gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.1f * speed, gameObject.transform.position.y);
			}
			else{
				gameObject.transform.position = new Vector2(0f, gameObject.transform.position.y);
			}
		}
		if (Input.GetKey ("s")) {
			if(gameObject.transform.position.y > 0f){
				gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f * speed);
			}
			else{
				gameObject.transform.position = new Vector2(gameObject.transform.position.x, 0f);
			}
		}
		if (Input.GetKey ("d")) {
			if(gameObject.transform.position.x < 50f){
				gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.1f * speed, gameObject.transform.position.y);
			}
			else{
				gameObject.transform.position = new Vector2(50f, gameObject.transform.position.y);
			}
		}
	}

	public void Damage(int amount) {
		health -= amount;
				
		if (health <= 0) {
			Kill();
		}
	}

	private void Kill(){
		Destroy (this.gameObject);
	}
}
