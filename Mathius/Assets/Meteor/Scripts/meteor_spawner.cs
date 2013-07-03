using UnityEngine;
using System.Collections;

public class meteor_spawner : MonoBehaviour {
	

	public float cooldown = 1;
	public float timer = 0;
	
	public GameObject meteor;
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > cooldown){
			
			Vector3 pos = new Vector3(400 ,Random.Range(-150,150) ,10);
			Instantiate(meteor,pos,Quaternion.identity);
			timer = Random.Range(0.0f, 1.0f);
		}
	}
}
