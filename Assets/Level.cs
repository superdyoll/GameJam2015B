using UnityEngine;
using System.Collections;

public static class Level{
	private static int level = 0;

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
