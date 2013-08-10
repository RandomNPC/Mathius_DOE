using UnityEngine;
using System.Collections;

public class Mathius : Object{
	
	private GameObject _mathius;
	private Vector3 _mpos;
	private Vector3 _bounds;
	private int _lives;
	private int _answer;
	
	public Mathius(GameObject mathius){
		_mathius = mathius;
		_mpos = new Vector3(0.0f,0.0f,0.0f);
		_bounds = new Vector3(0.0f,0.0f,0.0f);
		_lives = 0;
		_answer = Random.Range(0,9);
	}
	
	public void set_mpos(Vector3 mpos){_mpos = mpos;}
	public Vector3 get_mpos(){return _mpos;}
	public void set_lives(int lives){
		if(lives>=0){
			_lives = lives;
		}else MasterController.BRAIN.onGameEnd();
	
	}
	public int get_lives(){return _lives;}
	public void set_answer(){_answer = Random.Range(0,9);}
	public int get_answer(){return _answer;}
	public void spawn_mathius(float x, float y, float z){
		GameObject _m = (GameObject) GameObject.Instantiate(_mathius,new Vector3(x,y,z),Quaternion.identity);
		_m.name = "Mathius";
		_m.AddComponent<SpawnAnimation>();
		_m.transform.parent = GameObject.Find("MathiusEarthCam").transform;
	}
	public void set_bounds(Vector3 bounds){_bounds = bounds;}
	public Vector3 get_bounds(){return _bounds;}
	public void set_texture(Texture texture){_mathius.renderer.material.mainTexture = texture;}
}
