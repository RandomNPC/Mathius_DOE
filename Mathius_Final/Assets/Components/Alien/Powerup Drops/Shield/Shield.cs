using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	void Start () {
		SoundManager.SOUNDS.playSound(SoundManager.PU_SHIELD,CameraCollider.MATHIUS_EARTH_CAM);
		GameObject pshield = (GameObject)Resources.Load("Mathius-Shield");
		Vector3 pos = gameObject.transform.position;
		GameObject shield = (GameObject)Instantiate(pshield,new Vector3(pos.x,pos.y,pos.z),Quaternion.identity);
		shield.name = "Mathius-Shield";
		shield.transform.parent = gameObject.transform;
	}

}
