using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class OptionsUI : MonoBehaviour {
	public GUISkin thisMetalGUISkin;
	private static string[] colorArray = {"Grey", "Green", "Blue", "Pink", "Red", "Yellow", "Camo", "Pwny"};
	private Texture[] textureArray;
	private int colorInt;
	private string[] soundArray;
	private int soundInt;

	private bool toggleTxt1;
	private bool toggleTxt2;
	public float musicSliderValue;
	public float effectSliderValue;
	private Selector<Texture> textures;
	private Dictionary<Texture,string> textMapper;
	private PreferencesManager pref;
	

	void Start () {
		pref = MasterController.BRAIN.pm();
		textureArray = MasterController.BRAIN._mathiusTextures;
		textures = MasterController.BRAIN.st();
		textMapper = new Dictionary<Texture, string>();
		for(int i=0,  j=0; i<textureArray.Length && j<colorArray.Length; i++,j++){
			textMapper.Add(textureArray[i],colorArray[j]);		
		}
		toggleTxt1 = pref.get_usePerceptual();
		colorInt = MasterController.BRAIN.pm().get_mathiusTexture();
		MasterController.BRAIN.m().set_texture(textureArray[colorInt]);
		textures.set_position(colorInt);
		musicSliderValue = pref.get_musicVolume();
		effectSliderValue = pref.get_SFXVolume();
		SoundManager.SOUNDS.playSound(SoundManager.UI_CLICK,MasterController.UI_CAMERA_ALT);
	}
	
	void OnGUI(){
		float intDivider = Screen.height/100;
		float widthDivider = Screen.width/100;
		soundArray = new string [3];
		soundArray[0] = "Low";
		soundArray[1] = "Med";
		soundArray[2] = "High";

		//hSliderValue = 0.0F;
		Rect titleRect = new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider));
		GUI.skin = thisMetalGUISkin;
		//Title
		GUI.Label(titleRect, ("Options"),GUI.skin.GetStyle("label"));
		
		GUI.Label(new Rect(widthDivider*20, intDivider*25,widthDivider*50, intDivider*5), ("Mathius Color: "),GUI.skin.GetStyle("button"));
		//Equation Decrementer
		if(GUI.Button(new Rect(widthDivider*50, intDivider*25,widthDivider*5, intDivider*5),("-"),GUI.skin.GetStyle("button"))){
			textures.prev();
			MasterController.BRAIN.pm().set_mathiusTexture(textures.get_position());
			MasterController.BRAIN.m ().set_texture(textures.selected());
		}
		//Equation int
		GUI.Label(new Rect(widthDivider*55, intDivider*26,widthDivider*50, intDivider*5), ("" +textMapper[textures.selected()]),GUI.skin.GetStyle("toggle"));
		//Equation Incrementer
		if(GUI.Button(new Rect(widthDivider*65, intDivider*25,widthDivider*5, intDivider*5),("+"),GUI.skin.GetStyle("button"))){
			textures.next();
			MasterController.BRAIN.pm().set_mathiusTexture(textures.get_position());
			MasterController.BRAIN.m ().set_texture(textures.selected());
		}
		
		GUI.Label(new Rect(widthDivider*20, intDivider*30,widthDivider*50, intDivider*5), ("Perceptual: "),GUI.skin.GetStyle("button"));
		toggleTxt1 = GUI.Toggle(new Rect(widthDivider*55, intDivider*31, 100, 30), toggleTxt1, "");
		
		GUI.Label(new Rect(widthDivider*20, intDivider*34,widthDivider*50, intDivider*5), ("Voice Volume: "),GUI.skin.GetStyle("button"));

		//Equation Decrementer
		if(GUI.Button(new Rect(widthDivider*50, intDivider*34,widthDivider*5, intDivider*5),("-"),GUI.skin.GetStyle("button"))){
			if(soundInt>0){
				soundInt --;
			}
		}
		//Equation int
		GUI.Label(new Rect(widthDivider*55, intDivider*35,widthDivider*50, intDivider*5), ("" +soundArray[soundInt]),GUI.skin.GetStyle("toggle"));
		//Equation Incrementer
		if(GUI.Button(new Rect(widthDivider*65, intDivider*34,widthDivider*5, intDivider*5),("+"),GUI.skin.GetStyle("button"))){
			if( soundInt<2){
				soundInt ++;
			}
		}
		
		GUI.Label(new Rect(widthDivider*20, intDivider*38,widthDivider*50, intDivider*5), ("Sound: "),GUI.skin.GetStyle("button"));
		GUI.Label(new Rect(widthDivider*20, intDivider*42,widthDivider*50, intDivider*5), ("Mute: "),GUI.skin.GetStyle("button"));
		toggleTxt2 = GUI.Toggle(new Rect(widthDivider*55, intDivider*43, 100, 30), toggleTxt2, "");
		
		GUI.Label(new Rect(widthDivider*20, intDivider*46,widthDivider*50, intDivider*5), ("Music: " + (Mathf.Round(musicSliderValue *100f)/100f)),GUI.skin.GetStyle("button"));
		Rect slider1 = new Rect (widthDivider*50, intDivider*47, widthDivider*20, intDivider*2);
		musicSliderValue = GUI.HorizontalSlider(slider1, musicSliderValue, 0.0F, 100.0f);
		
		GUI.Label(new Rect(widthDivider*20, intDivider*50,widthDivider*50, intDivider*5), ("Effects: " + (Mathf.Round(effectSliderValue*100f)/100f)),GUI.skin.GetStyle("button"));
		Rect slider = new Rect (widthDivider*50, intDivider*51, widthDivider*20, intDivider*2);
		effectSliderValue = GUI.HorizontalSlider(slider, effectSliderValue, 0.0F, 100.0f);
		
		if(GUI.Button (new Rect(5*(Screen.width/10) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Main Menu") ,GUI.skin.GetStyle("box") ) ){
			pref.set_usePerceptual(toggleTxt1);
			pref.set_SFXVolume(effectSliderValue);
			pref.set_musicVolume(musicSliderValue);
			Application.LoadLevel("MainMenu");
		}

	}
}
