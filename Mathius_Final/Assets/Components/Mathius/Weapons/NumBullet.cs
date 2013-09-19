using UnityEngine;
using System.Collections;

public class NumBullet : MonoBehaviour {

	public int variable;

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
		
		GameObject camera = GameObject.Find("MathiusEarthCam");
		if(Mathf.Abs((camera.transform.position - gameObject.transform.position).x) > MasterController.BRAIN.m().get_bounds().x) Destroy(gameObject);
	}

	void OnTriggerEnter(Collider collision) {
		if(collision.gameObject.name.Contains("Alian")){
			MasterController.BRAIN.onAlienShot(variable,collision.gameObject);
			Destroy(gameObject);
		}
	}

}
