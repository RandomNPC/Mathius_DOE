// 41 Post - Created by DimasTheDriver on Dec/12/2011 . Part of the 'Unity: How to create a speech balloon' post. Available at: http://www.41post.com/?p=4545 
using UnityEngine; 
using System.Collections;

public class Target : MonoBehaviour 
{
	private Transform goTransform;
	private Vector3 goScreenPos;
	private Vector3 goViewportPos;
	public int bubbleWidth = 200;
	public int bubbleHeight = 100;
	public float offsetX = 0;
	public float offsetY = 150;
	private int centerOffsetX;
	private int centerOffsetY;
	public Material mat;
	public GUISkin guiSkin;
	
	public static Vector3 TARGET;
	
	void Awake() 
	{
		TARGET = gameObject.transform.position;
		goTransform = this.GetComponent<Transform>();
	}
	
	//use this for initialization
	void Start()
	{
		TARGET = gameObject.transform.position;
		centerOffsetX = bubbleWidth/2;
		centerOffsetY = bubbleHeight/2;
	}
	
	
	void LateUpdate() 
	{
		TARGET = gameObject.transform.position;
		goScreenPos = Camera.main.WorldToScreenPoint(goTransform.position);	
		goViewportPos.x = goScreenPos.x/(float)Screen.width;
		goViewportPos.y = goScreenPos.y/(float)Screen.height;
	}
	
	void OnGUI()
	{
		TARGET = gameObject.transform.position;
		GUI.BeginGroup(new Rect(goScreenPos.x-centerOffsetX-offsetX,Screen.height-goScreenPos.y-centerOffsetY-offsetY,bubbleWidth,bubbleHeight));
		GUI.Label(new Rect(0,0,100,50),"",guiSkin.customStyles[0]);
		GUI.EndGroup();
	}
}