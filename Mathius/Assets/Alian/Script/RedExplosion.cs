using UnityEngine;
using System.Collections;

public class RedExplosion : MonoBehaviour {

	float life = 0.7f;
	float timer = 0;
	
	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;
		if (timer > life)
			Destroy(gameObject);
	}
}
