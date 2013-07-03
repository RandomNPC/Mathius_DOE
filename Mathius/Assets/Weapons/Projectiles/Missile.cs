using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	GameObject ent_camera;
	Camera c_camera;

	void Start() {
		ent_camera = GameObject.FindGameObjectWithTag("MainCamera");
		c_camera = (Camera)ent_camera.GetComponent("Camera");

		float x, y, z;
		x = 200 * Mathf.Cos(transform.rotation.z);
		y = 200 * Mathf.Sin(transform.rotation.z);
		z = 0;
		rigidbody.velocity = new Vector3(x, y);
	}
	void Update() {
		float x, y;
		x = rigidbody.position.x;
		y = rigidbody.position.y;

		if(c_camera.isOrthoGraphic) { // Keep in camera bounds
			float offset_x, offset_y;

			offset_y = c_camera.orthographicSize + 100;
			offset_x = offset_y * c_camera.GetScreenWidth() / c_camera.GetScreenHeight();

			if((offset_x < x) || (x < -offset_x) || (offset_y < y) || (y < -offset_y)) die();
		} else { // Camera FOV/Aspect crap
			float dist, offset_x, offset_y;

			dist = Mathf.Abs(ent_camera.transform.position.z - transform.position.z);
			offset_y = dist * Mathf.Tan(rad(c_camera.fov / 2 + 5)); // the -5 is to create padding along the borders
			offset_x = offset_y * c_camera.aspect;

			if((offset_x < x) || (x < -offset_x) || (offset_y < y) || (y < -offset_y)) die();
		}
	}

	void OnCollisionEnter(Collision collision) {
		string name=collision.gameObject.name;
		switch(name) {
			case "Player":
				break;
			case "Missile(Clone)":
				break;
			case "Alian(Clone)":
				Destroy(collision.gameObject);
				die();
				break;
			default:
				die();
				break;
		}
	}

	void die() {
		Destroy(gameObject);
	}



	float rad(float deg) { return Mathf.PI * deg / 180; }
}
