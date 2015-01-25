using UnityEngine;
using System.Collections;

public class Bloodsplosion : MonoBehaviour {

	float timer = 0f;

	// Use this for initialization
	void Start () {
		if(timer == 0f)	{
			timer = Time.time;
		}

		if(timer + 25 < Time.time){
			Destroy(gameObject);
		}
	}
}
