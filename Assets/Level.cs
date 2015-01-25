using UnityEngine;
using System.Collections;

public static class Level{
	private static int level = 0;
	private static int timer = 1200;
	private static GUIText levelText;

	public static void Tick() {
		timer--;
		if (timer == 0) {
			LevelUp ();
		}
	}

	public static void LevelUp() {
		levelText = GameObject.Find ("Level").GetComponent<GUIText> ();
		level++;
		timer = 1200;
		levelText.text = "Annihilation Level: " + level;

		if (level % 2 == 0) {
			levelText.color = new Color (0.15f, 0.5f, 0.8f);
		} else {
			levelText.color = new Color (0.8f, 1f, 0f);
		}
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
