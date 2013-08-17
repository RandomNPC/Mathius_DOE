using UnityEngine;
using System.Collections;

public class NumBullet : MonoBehaviour {

	public char variable = '0';

	public Texture TOne;
	public Texture TTwo;
	public Texture TThree;
	public Texture TFour;
	public Texture TFive;
	public Texture TSix;
	public Texture TSeven;
	public Texture TEight;
	public Texture TNine;
	public Texture TZero;

	void Start() {
		SoundManager.SOUNDS.playSound(SoundManager.SFX_SHOOT_NUM,CameraCollider.MATHIUS_EARTH_CAM);
		float x, y;
		x = 200 * Mathf.Cos(transform.rotation.z);
		y = 200 * Mathf.Sin(transform.rotation.z);
		rigidbody.velocity = new Vector3(x, y);
	}

	void Update() {
		Vector3 vel = rigidbody.velocity;
		float mag = 0;
		mag += (vel.x * vel.x);
		mag += (vel.y * vel.y);
		mag += (vel.z * vel.z);
		mag = Mathf.Sqrt(mag);
		rigidbody.velocity=	200 / mag * vel;
	}

	void OnTriggerEnter(Collider collision) {
		if(collision.gameObject.name.Contains("Alian")){
			MasterController.BRAIN.onAlienShot(variable,collision.gameObject);
		}
		Destroy(gameObject);
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}

	public void updateTexture() {
		switch(variable) {
			case '0': gameObject.renderer.material.mainTexture = TZero; break;
			case '1': gameObject.renderer.material.mainTexture = TOne; break;
			case '2': gameObject.renderer.material.mainTexture = TTwo; break;
			case '3': gameObject.renderer.material.mainTexture = TThree; break;
			case '4': gameObject.renderer.material.mainTexture = TFour; break;
			case '5': gameObject.renderer.material.mainTexture = TFive; break;
			case '6': gameObject.renderer.material.mainTexture = TSix; break;
			case '7': gameObject.renderer.material.mainTexture = TSeven; break;
			case '8': gameObject.renderer.material.mainTexture = TEight; break;
			case '9': gameObject.renderer.material.mainTexture = TNine; break;
		}
	}
}
