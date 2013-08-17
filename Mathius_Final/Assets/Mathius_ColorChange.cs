using UnityEngine;
using System.Collections;

public class Mathius_ColorChange : MonoBehaviour {
	public Texture[] textureArray;
	private Selector<Texture> Textures;

	// Use this for initialization
	void Start () {
	Textures = new Selector<Texture>(textureArray);
	Textures.prev();
	}
	
	// Update is called once per frame
	void Update () {
		//MasterController.BRAIN.m().set_texture(Textures.selected());
		gameObject.renderer.material.SetTexture("_MainTex",Textures.selected());
	}
	public Selector<Texture> getTexture(){
		return Textures;
	}
}
