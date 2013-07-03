using UnityEngine;
using System.Collections;

public class Menu_UI : MonoBehaviour {
	public GUISkin thisMetalGUISkin;
	public AudioClip onHoverGuiSound;
/*bool boolVar = false;//Toggle
	public int selGridInt;
	public string[] selStrings = new string[] {"one","two","two","two","two","two","two","two","two","two","two","two","two","two","two"};
	public int toolbarInt = 0;//Toolbar
    public string[] toolbarStrings = new string[] {"Toolbar1", "Toolbar2", "Toolbar3"};//ToolBar
	*/
	void OnGUI () {
	/*	toolbarInt = GUI.Toolbar(new Rect(25, 25, 250, 30), toolbarInt, toolbarStrings);
		boolVar = GUILayout.Toggle(boolVar, "Tick Box");
		GUI.skin = thisMetalGUISkin;
		selGridInt = GUI.SelectionGrid(new Rect(250, 250, 100, 30), selGridInt, selStrings, 2); 
		if ((GUI.changed)||(Input.GetMouseButtonDown(0))){

        	switch(toolbarInt){

        	case 0:
					Debug.Log("tool bar 0");
               		// Application.LoadLevel(0);

            	break;

        	case 1:
					Debug.Log("tool bar 1");
              		//  Application.LoadLevel(1);

            	break;
			case 2:
					Debug.Log("tool bar 2");
				break;
        	}
		}*/
		float intDivider = Screen.height/100;
		Rect titleRect = new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider));
		GUI.skin = thisMetalGUISkin;
		GUI.Label(titleRect, ("Mathius: Defender of Earth!"),GUI.skin.GetStyle("label"));
		if(GUI.Button (new Rect(Screen.width/3 ,(23*intDivider) ,(2*(Screen.width/5)) ,(15*intDivider) ) ,("START GAME") ,GUI.skin.GetStyle("box") ) ){
				audio.clip = onHoverGuiSound;
				audio.PlayOneShot(onHoverGuiSound);
				Debug.Log("Mathius Clicked");
			    //yield new WaitForSeconds(audio.clip.length);
				Application.LoadLevel("Earth Scene");}
		if(GUI.Button (new Rect(Screen.width/3,(39*intDivider),(2*(Screen.width/5)),(15*intDivider)), ("Tutorial"),GUI.skin.GetStyle("box"))){
				audio.PlayOneShot(onHoverGuiSound);
				Debug.Log("Tutorial Clicked");
		}
				//GUI.skin.button.hover(Audio.PlayOneShot(onHoverGuiSound));
		
		if(GUI.Button (new Rect(Screen.width/3,(55*intDivider), (2*(Screen.width/5)), (15*intDivider)), ("High Score"),GUI.skin.GetStyle("box"))){
				audio.PlayOneShot(onHoverGuiSound);
				Debug.Log("HighScore Clicked");
				Application.LoadLevel("HighScores");}
		if(GUI.Button(new Rect(Screen.width/3,(72*intDivider),(2*(Screen.width/5)),(15*intDivider)),("Credits"),GUI.skin.GetStyle("box"))){
				audio.PlayOneShot(onHoverGuiSound);
				Debug.Log("Credits Clicked");
				Application.LoadLevel("Credits");}
		if(GUI.Button(new Rect(Screen.width/3,(88*intDivider), (2*(Screen.width/5)), (15*intDivider)), ("Exit"),GUI.skin.GetStyle("box"))){
				audio.PlayOneShot(onHoverGuiSound);
				Debug.Log("Exit Clicked");
				Application.Quit();}
		/*if (titleRect.Contains(Event.current.mousePosition)) if hover
				audio.PlayOneShot(onHoverGuiSound);*/
		//if(GUI.skin.box.hover)Audio.PlayOneShot(onHoverGuiSound);
		// if(GUI.skin.customStyles.Length > 0)
          //  Debug.Log(GUI.skin.customStyles[0].hover.textColor);

/*	 if (GUI.Button(new Rect(10, 10, 50, 50), "HI"))
            Debug.Log("Clicked the button with an image");
        
        if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
            Debug.Log("Clicked the button with text");
	
	*/}	
	void Update () {
		if(Input.GetButtonUp("Fire1")){
		//	Score += 100;
		}
		if(Input.GetButtonUp("Fire2")){
			//Score -= 100;
		}
		
		
	}
}