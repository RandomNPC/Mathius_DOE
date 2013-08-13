using UnityEngine;
using System.Collections;

public class HighScoreUI : MonoBehaviour {
	
	public GUISkin thisMetalGUISkin;
	
	void OnGUI(){
		float intDivider = Screen.height/100;
		GUI.skin = thisMetalGUISkin;
		GUI.Label(new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider)), ("Mathius: Defender of Earth!"),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect((Screen.width/5),(25*intDivider),(50*(Screen.width/100)),(10*intDivider)), ("HIGH Scores!"),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/55),(35*intDivider),(50*(Screen.width/100)),(9*intDivider)), ("1:\t\t\t" + PlayerPrefs.GetInt("Player H0") +" "+ PlayerPrefs.GetString("Player 0","A")),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/55),(45*intDivider),(50*(Screen.width/100)),(9*intDivider)), ("2:\t\t\t" + PlayerPrefs.GetInt("Player H1",1)+" "+ PlayerPrefs.GetString("Player 1","B")),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/55),(55*intDivider),(50*(Screen.width/100)),(9*intDivider)), ("3:\t\t\t" + PlayerPrefs.GetInt("Player H2",2) +" "+ PlayerPrefs.GetString("Player 2","C")),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/55),(65*intDivider),(50*(Screen.width/100)),(9*intDivider)), ("4:\t\t\t" + PlayerPrefs.GetInt("Player H3",3)+" "+ PlayerPrefs.GetString("Player 3","D")),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/55),(75*intDivider),(50*(Screen.width/100)),(9*intDivider)), ("5:\t\t\t" + PlayerPrefs.GetInt("Player H4",4) +" "+ PlayerPrefs.GetString("Player 4","E")),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/10)*5,(35*intDivider),(50*(Screen.width/100)),(9*intDivider)), ("6:\t\t\t" + PlayerPrefs.GetInt("Player H5",5)+" "+ PlayerPrefs.GetString("Player 5","F")),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/10)*5,(45*intDivider),(50*(Screen.width/100)),(9*intDivider)), ("7:\t\t\t" + PlayerPrefs.GetInt("Player H6",6) +" "+ PlayerPrefs.GetString("Player 6","G")),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/10)*5,(55*intDivider),(50*(Screen.width/100)),(9*intDivider)), ("8:\t\t\t" + PlayerPrefs.GetInt("Player H7",7)+" "+ PlayerPrefs.GetString("Player 7","H")),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/10)*5,(65*intDivider),(50*(Screen.width/100)),(9*intDivider)), ("9:\t\t\t" + PlayerPrefs.GetInt("Player H8",8) +" "+ PlayerPrefs.GetString("Player 8","I")),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/10)*5,(75*intDivider),(50*(Screen.width/100)),(9*intDivider)), ("10:\t\t\t" + PlayerPrefs.GetInt("Player H9",9)+" "+ PlayerPrefs.GetString("Player 9","J")),GUI.skin.GetStyle("box"));
		if(GUI.Button (new Rect(5*(Screen.width/10) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Main Menu") ,GUI.skin.GetStyle("box") ) ){
				Debug.Log("Mathius Clicked");
				Application.LoadLevel("MainMenu");}
		if(GUI.Button (new Rect((Screen.width/20) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("New Game") ,GUI.skin.GetStyle("box") ) ){
				Debug.Log("Replay Clicked");
				Application.LoadLevel("Earth Scene");}
	}
}

