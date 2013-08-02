using UnityEngine;
using System.Collections;

public class HighScoreUI : MonoBehaviour {
	
	public GUISkin thisMetalGUISkin;
	
	void OnGUI(){
		float intDivider = Screen.height/100;
		GUI.skin = thisMetalGUISkin;
		GUI.Label(new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider)), ("Mathius: Defender of Earth!"),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect((Screen.width/5),(25*intDivider),(3*(Screen.width/5)),(9*intDivider)), ("HIGH Scores!"),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/20),(35*intDivider),(2*(Screen.width/5)),(8*intDivider)), ("1:\t\t\t" + PlayerPrefs.GetInt("Player 1",0) +" "+ PlayerPrefs.GetString("Player 1","HAI")),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect((Screen.width/20),(45*intDivider),(2*(Screen.width/5)),(8*intDivider)), ("2:\t\t\t" + PlayerPrefs.GetInt("Player 2",0)+" "+ PlayerPrefs.GetString("Player 2","DER")),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect((Screen.width/20),(55*intDivider),(2*(Screen.width/5)),(8*intDivider)), ("3:\t\t\t" + PlayerPrefs.GetInt("Player 3",0) +" "+ PlayerPrefs.GetString("Player 3","WAT")),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect((Screen.width/20),(65*intDivider),(2*(Screen.width/5)),(8*intDivider)), ("4:\t\t\t" + PlayerPrefs.GetInt("Player 4",0)+" "+ PlayerPrefs.GetString("Player 4","TEH")),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect((Screen.width/20),(75*intDivider),(2*(Screen.width/5)),(8*intDivider)), ("5:\t\t\t" + PlayerPrefs.GetInt("Player 5",0) +" "+ PlayerPrefs.GetString("Player 5","HEK")),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect(((Screen.width/10)*5),(35*intDivider),(2*(Screen.width/5)),(8*intDivider)), ("6:\t\t\t" + PlayerPrefs.GetInt("Player 6",0)+" "+ PlayerPrefs.GetString("Player 6","ARR")),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect((Screen.width/10)*5,(45*intDivider),(2*(Screen.width/5)),(8*intDivider)), ("7:\t\t\t" + PlayerPrefs.GetInt("Player 7",0) +" "+ PlayerPrefs.GetString("Player 7","TEH")),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect((Screen.width/10)*5,(55*intDivider),(2*(Screen.width/5)),(8*intDivider)), ("8:\t\t\t" + PlayerPrefs.GetInt("Player 8",0)+" "+ PlayerPrefs.GetString("Player 8","STF")),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect((Screen.width/10)*5,(65*intDivider),(2*(Screen.width/5)),(8*intDivider)), ("9:\t\t\t" + PlayerPrefs.GetInt("Player 9",0) +" "+ PlayerPrefs.GetString("Player 9","PLZ")),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect((Screen.width/10)*5,(75*intDivider),(2*(Screen.width/5)),(8*intDivider)), ("10:\t\t\t" + PlayerPrefs.GetInt("Player 10",0)+" "+ PlayerPrefs.GetString("Player 10","LOL")),GUI.skin.GetStyle("label"));
		if(GUI.Button (new Rect(5*(Screen.width/10) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Main Menu") ,GUI.skin.GetStyle("box") ) ){
				Debug.Log("Mathius Clicked");
				Application.LoadLevel("MainMenu");}
		if(GUI.Button (new Rect((Screen.width/20) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Replay") ,GUI.skin.GetStyle("box") ) ){
				Debug.Log("Replay Clicked");
				Application.LoadLevel("Earth Scene");}
	}
}

