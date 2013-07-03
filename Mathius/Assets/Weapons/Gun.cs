using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public GameObject bulletNum, missile, lazor;
	private float Timer = 0.0f, CoolDown = 0.25f;
	private bool onCD = false;
	public AudioClip shootClip;

	void Start() {
		shootClip = Resources.Load("explosion_large_rnd_01") as AudioClip;
	}

	void Update() {
	//	print(shootClip);
		if(!onCD){
			if(Input.GetKeyDown(KeyCode.Alpha1) || (Input.GetKeyDown(KeyCode.Keypad1))){fireNum("1"); onCD = true; Timer = 0.0f;audio.PlayOneShot(shootClip,0.05F);}
			if(Input.GetKeyDown(KeyCode.Alpha2) || (Input.GetKeyDown(KeyCode.Keypad2))) {fireNum("2");onCD = true; Timer = 0.0f;audio.PlayOneShot(shootClip,0.05F);}
			if(Input.GetKeyDown(KeyCode.Alpha3) || (Input.GetKeyDown(KeyCode.Keypad3))) {fireNum("3");onCD = true; Timer = 0.0f;audio.PlayOneShot(shootClip,0.05F);}
			if(Input.GetKeyDown(KeyCode.Alpha4) || (Input.GetKeyDown(KeyCode.Keypad4))) {fireNum("4");onCD = true; Timer = 0.0f;audio.PlayOneShot(shootClip,0.05F);}
			if(Input.GetKeyDown(KeyCode.Alpha5) || (Input.GetKeyDown(KeyCode.Keypad5))) {fireNum("5");onCD = true; Timer = 0.0f;audio.PlayOneShot(shootClip,0.05F);}
			if(Input.GetKeyDown(KeyCode.Alpha6) || (Input.GetKeyDown(KeyCode.Keypad6))) {fireNum("6");onCD = true; Timer = 0.0f;audio.PlayOneShot(shootClip,0.05F);}
			if(Input.GetKeyDown(KeyCode.Alpha7) || (Input.GetKeyDown(KeyCode.Keypad7))) {fireNum("7");onCD = true; Timer = 0.0f;audio.PlayOneShot(shootClip,0.05F);}
			if(Input.GetKeyDown(KeyCode.Alpha8) || (Input.GetKeyDown(KeyCode.Keypad8))) {fireNum("8");onCD = true; Timer = 0.0f;audio.PlayOneShot(shootClip,0.05F);}
			if(Input.GetKeyDown(KeyCode.Alpha9) || (Input.GetKeyDown(KeyCode.Keypad9))) {fireNum("9");onCD = true; Timer = 0.0f;audio.PlayOneShot(shootClip,0.05F);}
			if(Input.GetKeyDown(KeyCode.Alpha0) || (Input.GetKeyDown(KeyCode.Keypad0))) {fireNum("0");onCD = true; Timer = 0.0f;audio.PlayOneShot(shootClip,0.05F);}
		}
		else{
			Timer += Time.deltaTime;
			if(Timer >= CoolDown){Timer = 0.0f; onCD = false;}
		}
		if(Input.GetKey("space")) {
			//fireMissile();
		}
		
		if(Input.GetKey(KeyCode.L)){
			//fireLazor();	
		}
	}

	void fireMissile() {
		Instantiate(missile, transform.position, transform.rotation);
	}
	void fireNum(string v) {
		GameObject proj = (GameObject)Instantiate(bulletNum, transform.position, transform.rotation);
		proj.GetComponent<NumBullet>().variable = v[0];
		proj.GetComponent<NumBullet>().updateTexture();
	}
	void fireLazor()
	{
		Instantiate(lazor, transform.position, transform.rotation);
	}
}
