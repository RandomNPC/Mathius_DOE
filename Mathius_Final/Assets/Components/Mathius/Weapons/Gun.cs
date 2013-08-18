using System;
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public GameObject bulletNum;
	private float Timer = 0.0f, CoolDown = 0.25f;
	private bool onCD = false;
	private PCInterface pc;
	private PerCVoice PV;

	void Start() {
		PV = GameObject.Find("Brain").GetComponent<PerCVoice>();
		pc = MasterController.BRAIN.pci();
	}

	void Update() {
		int voiceNum = PV.getNumberVoiced();
		if(!onCD){
			if(pc.get_using_PCI()){
				//fire bullet
			}
			else{
				if(Input.GetKeyDown(KeyCode.Alpha1) || (Input.GetKeyDown(KeyCode.Keypad1) || 1==voiceNum)){fireNum("1");}
				if(Input.GetKeyDown(KeyCode.Alpha2) || (Input.GetKeyDown(KeyCode.Keypad2) || 2==voiceNum)) {fireNum("2");}
				if(Input.GetKeyDown(KeyCode.Alpha3) || (Input.GetKeyDown(KeyCode.Keypad3) || 3==voiceNum)) {fireNum("3");}
				if(Input.GetKeyDown(KeyCode.Alpha4) || (Input.GetKeyDown(KeyCode.Keypad4) || 4==voiceNum)) {fireNum("4");}
				if(Input.GetKeyDown(KeyCode.Alpha5) || (Input.GetKeyDown(KeyCode.Keypad5) || 5==voiceNum)) {fireNum("5");}
				if(Input.GetKeyDown(KeyCode.Alpha6) || (Input.GetKeyDown(KeyCode.Keypad6) || 6==voiceNum)) {fireNum("6");}
				if(Input.GetKeyDown(KeyCode.Alpha7) || (Input.GetKeyDown(KeyCode.Keypad7) || 7==voiceNum)) {fireNum("7");}
				if(Input.GetKeyDown(KeyCode.Alpha8) || (Input.GetKeyDown(KeyCode.Keypad8) || 8==voiceNum)) {fireNum("8");}
				if(Input.GetKeyDown(KeyCode.Alpha9) || (Input.GetKeyDown(KeyCode.Keypad9) || 9==voiceNum)) {fireNum("9");}
				if(Input.GetKeyDown(KeyCode.Alpha0) || (Input.GetKeyDown(KeyCode.Keypad0) || 0==voiceNum)) {fireNum("0");}
			}
		}
		else{
			Timer += Time.deltaTime;
			if(Timer >= CoolDown){Timer = 0.0f; onCD = false;}
		}
	}

	private void fireNum(string v) {
		GameObject proj = (GameObject)Instantiate(bulletNum, transform.position, transform.rotation);
		proj.GetComponent<NumBullet>().variable = v[0];
		proj.GetComponent<NumBullet>().updateTexture();
		onCD = true;
		Timer = 0.0f;
	}
}
