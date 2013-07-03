using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	public GameObject clone;
	
	Vector3 start = new Vector3(1000.0f, 16.0f, 344.0f);
	
	float spawnTime = 3;
	float timer = 0;
	
	void Update () {
		timer += Time.deltaTime;
		if (timer > spawnTime)
		{
			start.y = Random.Range(20, 100);
			Instantiate(clone, start, transform.rotation);
			timer = 0;
		}
	
	}
}