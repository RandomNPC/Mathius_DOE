using UnityEngine;
using System.Collections;

public class SkinMathius : MonoBehaviour {

	void Update () {
		gameObject.renderer.material.mainTexture = MasterController.BRAIN.m ().get_texture();	
	}
}
