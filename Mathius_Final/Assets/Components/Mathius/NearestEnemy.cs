using UnityEngine;
using System.Collections;

public class NearestEnemy : MonoBehaviour {
	
	private GameObject nearest_enemy;
	
	void Update () {
		GameObject[] gos;
		GameObject closest = null;
		try{
        	gos = GameObject.FindGameObjectsWithTag("Alian");
		} catch{ 
			return;
		}
		if(gos.Length<=0) return;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
		nearest_enemy = closest;
		MasterController.BRAIN.sm().set_equation(nearest_enemy.GetComponent<AlienManager>().equation);
	}
}
