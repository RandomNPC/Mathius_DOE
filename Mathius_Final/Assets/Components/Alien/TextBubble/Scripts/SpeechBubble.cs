// 41 Post - Created by DimasTheDriver on Dec/12/2011 . Part of the 'Unity: How to create a speech balloon' post. Available at: http://www.41post.com/?p=4545 
using UnityEngine; 
using System.Collections;

public class SpeechBubble : MonoBehaviour 
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
	
	void Awake() 
	{
		goTransform = this.GetComponent<Transform>();
	}
	
	//use this for initialization
	void Start()
	{
		centerOffsetX = bubbleWidth/2;
		centerOffsetY = bubbleHeight/2;
	}
	
	
	void LateUpdate() 
	{
		goScreenPos = Camera.main.WorldToScreenPoint(goTransform.position);	
		
		goViewportPos.x = goScreenPos.x/(float)Screen.width;
		goViewportPos.y = goScreenPos.y/(float)Screen.height;
	}
	
	void OnGUI()
	{
		GUI.BeginGroup(new Rect(goScreenPos.x-centerOffsetX-offsetX,Screen.height-goScreenPos.y-centerOffsetY-offsetY,bubbleWidth,bubbleHeight));
			
			GUI.Label(new Rect(0,0,200,50),"",guiSkin.customStyles[0]);
			
			//text! gameObject.transform.parent.GetComponent<AlienManager>().equation
			GUI.Label(new Rect(15,25,190,50),gameObject.transform.parent.GetComponent<AlienManager>().equation,guiSkin.label);
		
		
		GUI.EndGroup();
	}

	void OnRenderObject()
	{
		GL.PushMatrix();
		mat.SetPass(0);
		GL.LoadOrtho();
		GL.Begin(GL.TRIANGLES);
	
		GL.Color(Color.white);
			float offset = -0.02f;
		GL.Vertex3(goViewportPos.x + offset, goViewportPos.y+(-10.0f)/Screen.height, 0.1f); //bottom 
		GL.Vertex3(goViewportPos.x + offset - (bubbleWidth/3)/(float)Screen.width, goViewportPos.y+((offsetY)+10.0f)/Screen.height, 0.1f); //left
		GL.Vertex3(goViewportPos.x + offset - (bubbleWidth/8)/(float)Screen.width, goViewportPos.y+((offsetY)+10.0f)/Screen.height, 0.1f); //right
		
		GL.End();

		GL.PopMatrix();
	}
}