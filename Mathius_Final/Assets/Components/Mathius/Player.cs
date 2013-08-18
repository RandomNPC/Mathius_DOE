using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public const byte MATHIUS_NO_MOVE = 0x0;
	public const byte MATHIUS_LEFT = 0x01;
	public const byte MATHIUS_RIGHT = 0x02;
	public const byte MATHIUS_UP = 0x04;
	public const byte MATHIUS_DOWN = 0x08;
	
	private float centerX = 157.0f;
	private float centerY = 121.0f;
	private float deadZone = 40.0f;
	
	
	public GameObject explosion;
	private Vector3 delta;
	private PCInterface pc;
	private PerCGesture Gest;
	
	// Use this for initialization
	void Start () {
		Gest = GameObject.Find("Brain").GetComponent<PerCGesture>();
		delta = new Vector3(0.0f,0.0f,0.0f);
		pc = MasterController.BRAIN.pci();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 cpos = Camera.main.transform.position;
		Vector3 mpos = gameObject.transform.position;
		
		delta.z = mpos.z-cpos.z;
		delta.y = delta.z*Mathf.Tan(Camera.main.fov*Mathf.PI/360);
		delta.x = delta.y*Camera.main.aspect;
		
		if(mpos.y>cpos.y+delta.y){moveMathius(MATHIUS_DOWN);}
		else if(mpos.y < cpos.y-delta.y){moveMathius(MATHIUS_UP);}
		
		if(mpos.x>cpos.x+delta.x){moveMathius(MATHIUS_LEFT);}
		else if(mpos.x < cpos.x-delta.x){moveMathius(MATHIUS_RIGHT);}
		
		if(pc.get_using_PCI()){//move mathius
			float[] xy = Gest.getHandLocation();
			if(xy[1]<centerY-deadZone) moveMathius(MATHIUS_UP);
			if(xy[0]>centerX+deadZone) moveMathius(MATHIUS_LEFT);
			if(xy[1]>centerY+deadZone) moveMathius(MATHIUS_DOWN);
			if(xy[0]<centerX-deadZone) moveMathius(MATHIUS_RIGHT);
		} else{
			if(Input.GetKey("w")){moveMathius(MATHIUS_UP);}
			if(Input.GetKey("a")){moveMathius(MATHIUS_LEFT);}
			if(Input.GetKey("s")){moveMathius(MATHIUS_DOWN);}
			if(Input.GetKey("d")){moveMathius(MATHIUS_RIGHT);}
		}
		
		gameObject.transform.localRotation.Set(0.0f,0.0f,0.0f,0.0f);
		MasterController.BRAIN.m().set_bounds(delta);
	}
	
	private void moveMathius(byte direction){
		if((direction & MATHIUS_UP)== MATHIUS_UP){gameObject.transform.Translate(0.0f,1.0f,0.0f);}
		if((direction & MATHIUS_LEFT)== MATHIUS_LEFT){gameObject.transform.Translate(-1.0f,0.0f,0.0f);}
		if((direction & MATHIUS_DOWN)== MATHIUS_DOWN){gameObject.transform.Translate(0.0f,-1.0f,0.0f);}
		if((direction & MATHIUS_RIGHT)== MATHIUS_RIGHT){gameObject.transform.Translate(1.0f,0.0f,0.0f);}
	}
	
	public void crashTrajectory_mathius(){
		gameObject.GetComponent<Rigidbody>().useGravity = true;
		gameObject.GetComponent<Rigidbody>().AddForce(0.0f,-10.0f,0.0f);
	}
	
	public void destroy_mathius(){
		Vector3 explode = gameObject.transform.position;
		Instantiate(explosion,new Vector3(explode.x,explode.y,explode.z),Quaternion.identity);
		Destroy(gameObject);
		SoundManager.SOUNDS.playSound(SoundManager.MATH_CRASH, CameraCollider.MATHIUS_EARTH_CAM);
	}
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.name.Contains("Powerup")) return;
		MasterController.BRAIN.onMathiusCollision(col);
	}
}
