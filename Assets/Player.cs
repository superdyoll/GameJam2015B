﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed { get; set; }
	public int projectileRange { get; set; }
	public GameObject projectile;
	private float timer = 0f;

	public Dino dinosaur { get; set; }

	void Ascend () {
		System.Random rnd = new System.Random ();
		int num = rnd.Next (5);

		Debug.Log (num + "");

		if (dinosaur == null) {
			switch (num) {
			case 0:
				dinosaur = gameObject.AddComponent<Diplodofortress> ();
				break;
			case 1:
				dinosaur = gameObject.AddComponent<Triceratank> ();
				break;
			case 2:
				dinosaur = gameObject.AddComponent<Assaultosaurus> ();
				break;
			case 3:
				dinosaur = gameObject.AddComponent<Horroraptor> ();
				break;
			case 4:
				dinosaur = gameObject.AddComponent<SatanasaurusRex> ();
				break;
			default:
				dinosaur = gameObject.AddComponent<Terrordactyl> ();
				break;
			}
		}
		dinosaur.playerControlled = true;
		dinosaur.Create ();



		speed = dinosaur.speed;
		projectileRange = 1;//dinosaur.weapon.range;
	}

	void Start() {
		Ascend ();
	}

	void Update () {
		GetMovementInput ();
		GetMouseInput ();
		UpdateCameraPosition ();
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
		projScript.Go (projectileRange, 10, 1, 10, GetVect3Rotation ());
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
}
