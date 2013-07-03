using UnityEngine;
using System.Collections;

public class AlienSpawner : MonoBehaviour {
	
	private float spawnTime;
	private float timer;
	private Vector3 start;
	private float boxY;
	private float boxSizeY;
	private bool hasCollide;
	
	public GameObject clone;
	
	// Use this for initialization
	void Start () {
		spawnTime = 3;
		timer = 0;
		hasCollide = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(hasCollide){
			timer += Time.deltaTime;
			if (timer > spawnTime)
			{
				start.y = Random.Range(17,boxY+boxSizeY);
				Instantiate(clone, start, transform.rotation);
				timer = 0;
			}
		}
	}
	
	void OnTriggerEnter (Collider obj){
		if(obj.tag == "Player"){
			BoxCollider spawner = gameObject.GetComponent<BoxCollider>();
		
			boxY = spawner.center.y;
			boxSizeY = spawner.size.y/2;
		
			float boxX = spawner.center.x;
			float boxSizeX = spawner.size.x/2;
		
			start = new Vector3(boxX+boxSizeX,0.0f,344.0f);
			hasCollide = true;
		}
	}
}
