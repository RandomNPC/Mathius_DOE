using System;
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	private float Timer = 0.0f, CoolDown = 0.25f;
	private bool onCD = false;
	
	public GameObject[] bullets;
	
	void Update() {
		
		if(!onCD){
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
		else{
			Timer += Time.deltaTime;
			if(Timer >= CoolDown){Timer = 0.0f; onCD = false;}
		}
	}

	public void fireNum(int v) {
		if(v < 0 && v > 9) return; 
		GameObject proj = (GameObject)Instantiate(bullets[v], transform.position, transform.rotation);
		proj.name = "Bullet";
		proj.GetComponent<NumBullet>().variable = v;
		onCD = true;
		Timer = 0.0f;
	}
}
