using UnityEngine;
using System.Collections;

public class DinoSelector : MonoBehaviour {

	public Dino ChooseRandomDino(){
		Dino dinosaur = null;
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
		return dinosaur;
	}
}
