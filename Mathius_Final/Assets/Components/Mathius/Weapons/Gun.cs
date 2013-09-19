using System;
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public GameObject[] bullets;
	private GameObject nearest_bullet = null;
	
	void Update() {
			if(MasterController.BRAIN.pci().get_using_PCI()){
				fireNum(PerCVoice.PV.getNumberVoiced());
			}
			else{
				if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)){fireNum(1);}
				if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {fireNum(2);}
				if(Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) {fireNum(3);}
				if(Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) {fireNum(4);}
				if(Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) {fireNum(5);}
				if(Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) {fireNum(6);}
				if(Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) {fireNum(7);}
				if(Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) {fireNum(8);}
				if(Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) {fireNum(9);}
				if(Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) {fireNum(0);}
			}
	}

	public void fireNum(int v) {
		//check the distance of game object to the nearest bullet
		if(v < 0 && v > 9) return;
		
		if(nearest_bullet)if(Mathf.Abs((gameObject.transform.position - nearest_bullet.transform.position).x)<=10.0f) return;
		
		if(GameObject.FindGameObjectsWithTag("Bullet").Length > 3) return;
		
		GameObject proj = (GameObject)Instantiate(bullets[v], transform.position, transform.rotation);
		proj.name = "Bullet";
		proj.GetComponent<NumBullet>().variable = v;
		nearest_bullet = proj;
	}
}
