using UnityEngine;
using System.Collections;

public static class Level{
	private static int level = 0;
	private static int timer = 600;
	private static GUIText levelText = GameObject.Find("Level").GetComponent<GUIText> ();

	public static void Tick() {
		timer--;
		if (timer == 0) {
			LevelUp ();
		}
	}

	public static void LevelUp() {
		level++;
		timer = 600;
		levelText.text = "Annihilation Level: " + level;
	}

	public static int getLevel (){
		return level;
	}

	public static void setLevel (int newLevel){
		level = newLevel;
	}

	public static void increaseLevel(){
		level ++;
	}

	public static void increaseLevel(int increaseAmount){
		level = level + increaseAmount;
	}
}
