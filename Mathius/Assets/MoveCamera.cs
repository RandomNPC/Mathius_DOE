using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	
	private GameObject prev;
	private GameObject current;
	private GameObject mathius;
	private GameObject Pmathius;
	private static int num;
	private GameObject landgen;
	private Terrain td;
	private Collider now;
	private bool boom;
	private float pos;
	
	
	// Use this for initialization
	void Start () {
		Vector3 camera = gameObject.transform.position; 
		mathius = Resources.Load("Mathius") as GameObject;
		Pmathius = Instantiate(mathius,new Vector3(camera.x,camera.y+25,camera.z+100),Quaternion.identity) as GameObject;
		Pmathius.name = "Mathius";
		Rigidbody rigidMathius = mathius.GetComponent(typeof(Rigidbody)) as Rigidbody;
		rigidMathius.mass = 1;
		rigidMathius.useGravity = false;
		
		num = 0;
		pos = 0;
		landgen = Resources.Load("Mathius-Desert") as GameObject;
		td = landgen.GetComponent(typeof(Terrain)) as Terrain;
		current = Instantiate(landgen,new Vector3(pos,0.0f,100.0f),Quaternion.identity) as GameObject;
		current.name = "Surface " + num;
		add_allocator(current);
	
	}
		
	void add_allocator(GameObject obj){
		Terrain t = obj.GetComponent(typeof(Terrain)) as Terrain;
		Vector3 coords = t.terrainData.size;
		
		GameObject allocator = new GameObject("Allocator");
		allocator.transform.position = new Vector3(pos+coords.x/2,0.0f,100.0f+coords.z/2);
		
		BoxCollider bc = allocator.AddComponent("BoxCollider") as BoxCollider;
		bc.size = new Vector3(10.0f,300.0f,coords.z);
		bc.isTrigger = true;
		
		bc.transform.parent = obj.transform;
		
		TerrainCollider tc = obj.GetComponent(typeof(TerrainCollider)) as TerrainCollider;
		tc.enabled =true;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if(boom){
			onTrigger_GenerateLayout(now);
			boom = false;
		}
	}
	
	void onTrigger_GenerateLayout(Collider obj){
		//get the instance of the obj the collider is connected to
		current = GameObject.Find(obj.transform.parent.name);

		//destroy the prev
		Destroy(prev);
		
		//prev = instance of the obj the collider is connected to
		prev = current;
		
		SpawnAlien sa = prev.GetComponent("SpawnAlien") as SpawnAlien;
		sa.start_spawn = false;
		
		//current = new instance created
		pos += td.terrainData.size.x/2;
		
		//Generate a number to determine the new layout
		switch(Random.Range (0,2)){
			case 0:
				landgen = Resources.Load("Mathius-Earth") as GameObject;
				break;
			case 1:
			case 2:
				landgen = Resources.Load("Mathius-Desert") as GameObject;
				break;
			default:
				break;
		}
		td = landgen.GetComponent(typeof(Terrain)) as Terrain;
		pos += td.terrainData.size.x/2;
		
		current = Instantiate(landgen,new Vector3(pos,0.0f,100.0f),Quaternion.identity) as GameObject;
		current.name = "Surface " + ++num;
		
		add_allocator(current);
		
	}
	
	void OnTriggerEnter(Collider obj){
		if(obj.name.Contains("Allocator")){
			boom = true;
			now = obj;
		}
	}
	
	public void destroy_mathius(){
		Rigidbody mathiusBody = Pmathius.GetComponent(typeof(Rigidbody)) as Rigidbody;
		mathiusBody.useGravity = true;
		mathiusBody.AddForce(0.0f,-3000.0f,0.0f);
	}
	
	public void spawn_mathius(){
		Vector3 camera = gameObject.transform.position;
		Pmathius = Instantiate(mathius,new Vector3(camera.x,camera.y+25,camera.z+100),Quaternion.identity) as GameObject;
		Rigidbody rigidMathius =  Pmathius.GetComponent(typeof(Rigidbody)) as Rigidbody;
		rigidMathius.useGravity =false;
		Pmathius.name = "Mathius";
		rigidMathius.mass = 1.0f;
	}
	
	public void kill_mathius(){
		Destroy(Pmathius);
		spawn_mathius();
	}
	
}