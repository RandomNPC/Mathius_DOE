using UnityEngine;
using System.Collections;

public class Menu_UI : MonoBehaviour {
	public GUISkin thisMetalGUISkin;

	#pragma warning disable 0168 // variable declared but not used.
	#pragma warning disable 0219 // variable assigned but not used.
	#pragma warning disable 0414 
	
	void OnGUI () {

		float intDivider = Screen.height/100;
		float widthDivider = Screen.width/100;
		Rect titleRect = new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider));
		GUI.skin = thisMetalGUISkin;
		GUI.Label(titleRect, ("Mathius: Defender of Earth!"),GUI.skin.GetStyle("label"));
		if(GUI.Button (new Rect(Screen.width/3 ,(23*intDivider) ,(2*(Screen.width/5)) ,(10*intDivider) ) ,("START GAME") ,GUI.skin.GetStyle("box") ) ){
				Debug.Log("Mathius Clicked");
				Application.LoadLevel("Earth Scene");}
		if(GUI.Button (new Rect((Screen.width/3),(35*intDivider),(2*(Screen.width/5)),(10*intDivider)), ("Level Editor"),GUI.skin.GetStyle("box"))){
				Debug.Log("level Editor Clicked");
				Application.LoadLevel("Settings");
		}
		if(GUI.Button (new Rect((Screen.width/3),(46*intDivider),(2*(Screen.width/5)),(10*intDivider)), ("Options"),GUI.skin.GetStyle("box"))){
				Debug.Log("Options Clicked");
		}
		
		if(GUI.Button (new Rect(Screen.width/3,(57*intDivider), (2*(Screen.width/5)), (10*intDivider)), ("High Score"),GUI.skin.GetStyle("box"))){
				Debug.Log("HighScore Clicked");
				Application.LoadLevel("HighScores");}
		if(GUI.Button(new Rect(Screen.width/3,(68*intDivider),(2*(Screen.width/5)),(10*intDivider)),("Credits"),GUI.skin.GetStyle("box"))){
				Debug.Log("Credits Clicked");
				Application.LoadLevel("Credits");}
		if(GUI.Button(new Rect(Screen.width/3,(79*intDivider), (2*(Screen.width/5)), (10*intDivider)), ("Exit"),GUI.skin.GetStyle("box"))){
				Debug.Log("Exit Clicked");
				Application.Quit();}
		//if(GUI.Button (new Rect(Screen.width/3,(35*intDivider),(2*(Screen.width/5)),(10*intDivider)), ("Tutorial"),GUI.skin.GetStyle("box"))){
		//		Debug.Log("Tutorial Clicked");
		//} Tutorial Button
}	
}