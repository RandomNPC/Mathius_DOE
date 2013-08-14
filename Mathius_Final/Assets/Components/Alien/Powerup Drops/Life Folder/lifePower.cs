using UnityEngine;
using System.Collections;

public class lifePower : MonoBehaviour {

	void Start () {
		
		MasterController.BRAIN.m().set_lives(MasterController.BRAIN.m().get_lives() + 1);
		
	
	}

}
