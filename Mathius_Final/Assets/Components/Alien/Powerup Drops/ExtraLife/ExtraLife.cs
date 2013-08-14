using UnityEngine;
using System.Collections;

public class ExtraLife : MonoBehaviour {

	void Start () {
		Mathius life = MasterController.BRAIN.m();
		life.set_lives(life.get_lives()+1);
	}

}
