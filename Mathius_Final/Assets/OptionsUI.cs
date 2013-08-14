using UnityEngine;
using System.Collections;

public class OptionsUI : MonoBehaviour {
	public GUISkin thisMetalGUISkin;
	private string[] colorArray;
	private int colorInt;
	private bool toggleTxt1;
	private bool toggleTxt2;
	public float musicSliderValue = 0.0f;
	public float effectSliderValue = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI(){
		float intDivider = Screen.height/100;
		float widthDivider = Screen.width/100;
		colorArray =new string [8];
		colorArray[0] ="Grey";
		colorArray[1] ="Green";
		colorArray[2] ="Blue";
		colorArray[3] ="Pink";
		colorArray[4] ="Red";
		colorArray[5] ="Yellow";
		colorArray[6] ="Camo";
		colorArray[7] ="Pwny";

		//hSliderValue = 0.0F;
		Rect titleRect = new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider));
		GUI.skin = thisMetalGUISkin;
		//Title
		GUI.Label(titleRect, ("Options"),GUI.skin.GetStyle("label"));
		
		GUI.Label(new Rect(widthDivider*20, intDivider*25,widthDivider*50, intDivider*5), ("Mathius Color: "),GUI.skin.GetStyle("button"));
		//Equation Decrementer
		if(GUI.Button(new Rect(widthDivider*50, intDivider*25,widthDivider*5, intDivider*5),("-"),GUI.skin.GetStyle("button"))){
			if(colorInt>0){
				colorInt --;
			}
		}
		//Equation int
		GUI.Label(new Rect(widthDivider*55, intDivider*26,widthDivider*50, intDivider*5), ("" +colorArray[colorInt]),GUI.skin.GetStyle("toggle"));
		//Equation Incrementer
		if(GUI.Button(new Rect(widthDivider*65, intDivider*25,widthDivider*5, intDivider*5),("+"),GUI.skin.GetStyle("button"))){
			if( colorInt<7){
				colorInt ++;
			}
		}
		
		GUI.Label(new Rect(widthDivider*20, intDivider*30,widthDivider*50, intDivider*5), ("Perceptual: "),GUI.skin.GetStyle("button"));
		toggleTxt1 = GUI.Toggle(new Rect(widthDivider*55, intDivider*31, 100, 30), toggleTxt1, "");
		
		GUI.Label(new Rect(widthDivider*20, intDivider*34,widthDivider*50, intDivider*5), ("Sound: "),GUI.skin.GetStyle("button"));
		GUI.Label(new Rect(widthDivider*20, intDivider*38,widthDivider*50, intDivider*5), ("Mute: "),GUI.skin.GetStyle("button"));
		toggleTxt2 = GUI.Toggle(new Rect(widthDivider*55, intDivider*39, 100, 30), toggleTxt2, "");
		
		GUI.Label(new Rect(widthDivider*20, intDivider*42,widthDivider*50, intDivider*5), ("Music: " + (Mathf.Round(musicSliderValue *100f)/100f)),GUI.skin.GetStyle("button"));
		Rect slider1 = new Rect (widthDivider*50, intDivider*43, widthDivider*20, intDivider*2);
		musicSliderValue = GUI.HorizontalSlider(slider1, musicSliderValue, 0.0F, 100.0F);
		
		GUI.Label(new Rect(widthDivider*20, intDivider*46,widthDivider*50, intDivider*5), ("Effects: " + (Mathf.Round(effectSliderValue*100f)/100f)),GUI.skin.GetStyle("button"));
		Rect slider = new Rect (widthDivider*50, intDivider*47, widthDivider*20, intDivider*2);
		effectSliderValue = GUI.HorizontalSlider(slider, effectSliderValue, 0.0F, 100.0F);
		
		
		if(GUI.Button (new Rect(5*(Screen.width/10) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Main Menu") ,GUI.skin.GetStyle("box") ) ){
			Application.LoadLevel("MainMenu");
		}

	}
}
