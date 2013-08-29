using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionsUI : MonoBehaviour {
	
	public GUISkin thisMetalGUISkin;
	private GUIManager gui;
	private PreferencesManager prefs;
	
	private int _sound;
	private int _texture;
	private float _music;
	private float _sfx;
	private bool _mute;
	private bool _perceptual;
	
	private Texture[] _mathiusTextures;
	private Dictionary<string,Texture> _texturemap;
	
	private string[] colorArray = {"Grey", "Green", "Blue", "Pink", "Red", "Yellow", "Camo", "Pwny"};
	private string[] soundArray = {"Low","Med","High"};
	private const string OPTIONS = "Options";
	private const string MATHIUS_COLOR = "Mathius Color:";
	private const string MATHIUS_COLOR_DISPLAY = "Mathius Color Display";
	private const string PERCEPTUAL = "Perceptual";
	private const string PERCEPTUAL_DISPLAY = "Perceptual Display";
	private const string VOICE_VOLUME = "Voice Volume";
	private const string VOICE_VOLUME_DISPLAY = "Voice Volume Display";
	private const string SOUND = "Sound";
	private const string MUTE = "Mute";
	private const string MUTE_DISPLAY = "Mute Display";
	private const string MUSIC = "Music";
	private const string MUSIC_DISPLAY = "Music Display";
	private const string EFFECTS = "SFX";
	private const string PLUS1 = "+1";
	private const string MINUS1 = "-1";
	private const string PLUS2 = "+2";
	private const string MINUS2 = "-2";
	private const string EFFECTS_DISPLAY = "Effects Display";
	private const string MAIN_MENU = "Main Menu";
	
	void Start () {
		SoundManager.SOUNDS.playSound(SoundManager.UI_CLICK,MasterController.UI_CAMERA_ALT);
		prefs = MasterController.BRAIN.pm();
		gui = new GUIManager(thisMetalGUISkin);
		
		gui.OnClick += HandleGuiOnClick;
		gui.OnScroll += HandleGuiOnScroll;
		gui.OnToggle += HandleGuiOnToggle;
		
		float intDivider = Screen.height/100;
		float widthDivider = Screen.width/100;
		
		_sfx = prefs.get_SFXVolume();
		_music = prefs.get_musicVolume();
		_sound = prefs.get_perceptualVolumeMode ();
		_texture = prefs.get_mathiusTexture();
		_perceptual = prefs.get_usePerceptual();
		_mute = prefs.get_mute();
		_mathiusTextures = MasterController.BRAIN._mathiusTextures;
		
		_texturemap = new Dictionary<string, Texture>();
		for(int i=0,  j=0; i<_mathiusTextures.Length && j<colorArray.Length; i++,j++){
			_texturemap.Add(colorArray[j],_mathiusTextures[i]);		
		}
		
		MasterController.BRAIN.m().set_texture(_texturemap[colorArray[prefs.get_mathiusTexture()]]);
		//Options
		gui.CreateGUIObject(OPTIONS,
							"Options",
							new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider)),
							GUIType.Label,
							"label");
		//Mathius Color
		gui.CreateGUIObject(MATHIUS_COLOR,
							"Mathius Color: ",
							new Rect(widthDivider*20, intDivider*25,widthDivider*50, intDivider*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(MINUS1,
							"-",
							new Rect(widthDivider*50, intDivider*25,widthDivider*5, intDivider*5),
							GUIType.Button,
							"button");
		gui.CreateGUIObject(MATHIUS_COLOR_DISPLAY,
							colorArray[_texture],
							new Rect(widthDivider*55, intDivider*26,widthDivider*50, intDivider*5),
							GUIType.Label,
							"nobox");
		gui.CreateGUIObject(PLUS1,
							"+",
							new Rect(widthDivider*65, intDivider*25,widthDivider*5, intDivider*5),
							GUIType.Button,
							"button");
		//Perceptual
		gui.CreateGUIObject(PERCEPTUAL,
							"Perceptual: ",
							new Rect(widthDivider*20, intDivider*30,widthDivider*50, intDivider*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(PERCEPTUAL_DISPLAY,
							"",
							new Rect(widthDivider*55, intDivider*31, 100, 30),
							GUIType.Toggle,
							"toggle",
							_perceptual);
		//Volume
		gui.CreateGUIObject(VOICE_VOLUME,
							"Voice Volume: ",
							new Rect(widthDivider*20, intDivider*34,widthDivider*50, intDivider*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(MINUS2,
							"-",
							new Rect(widthDivider*50, intDivider*34,widthDivider*5, intDivider*5),
							GUIType.Button,
							"button");
		gui.CreateGUIObject(VOICE_VOLUME_DISPLAY,
							soundArray[_sound],
							new Rect(widthDivider*55, intDivider*35,widthDivider*50, intDivider*5),
							GUIType.Label,
							"nobox");
		gui.CreateGUIObject(PLUS2,
							"+",
							new Rect(widthDivider*65, intDivider*34,widthDivider*5, intDivider*5),
							GUIType.Button,
							"button");
		//Sound
		gui.CreateGUIObject(SOUND,
							"Sound: ",
							new Rect(widthDivider*20, intDivider*38,widthDivider*50, intDivider*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(MUTE,
							"Mute: ",
							new Rect(widthDivider*20, intDivider*42,widthDivider*50, intDivider*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(MUTE_DISPLAY,
							"",
							new Rect(widthDivider*55, intDivider*43, 100, 30),
							GUIType.Toggle,
							"toggle",
							_mute);
		//Music
		gui.CreateGUIObject(MUSIC,
							("Music: " + (Mathf.Round(_music *100f)/100f)),
							new Rect(widthDivider*20, intDivider*46,widthDivider*50, intDivider*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(MUSIC_DISPLAY,
							"",
							new Rect (widthDivider*50, intDivider*47, widthDivider*20, intDivider*2),
							GUIType.Slider,
							"",
							false,
							0.0f,
							100.0f,
							_music);
		//SFX
		gui.CreateGUIObject(EFFECTS,
							"Effects: " + (Mathf.Round(_sfx*100f)/100f),
							new Rect(widthDivider*20, intDivider*50,widthDivider*50, intDivider*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(EFFECTS_DISPLAY,
							"",
							new Rect (widthDivider*50, intDivider*51, widthDivider*20, intDivider*2),
							GUIType.Slider,
							"",
							false,
							0.0f,
							100.0f,
							_sfx);
		//Main Menu
		gui.CreateGUIObject(MAIN_MENU,
							"Main Menu",
							new Rect(5*(Screen.width/10) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ),
							GUIType.Button,
							"box");
		gui.connect(MATHIUS_COLOR,MINUS1,PLUS1,MAIN_MENU,PERCEPTUAL);
		gui.connect(PERCEPTUAL,"","",MATHIUS_COLOR,VOICE_VOLUME);
		gui.connect(VOICE_VOLUME,MINUS2,PLUS2,PERCEPTUAL,MUTE);
		gui.connect(MUTE,"","",VOICE_VOLUME,MUSIC);
		gui.connect(MUSIC,"","",MUTE,EFFECTS);
		gui.connect(EFFECTS,"","",MUSIC,MAIN_MENU);
		gui.connect(MAIN_MENU,"","",EFFECTS,MATHIUS_COLOR);
		
		gui.connect(MINUS1,"",MATHIUS_COLOR,"","");
		gui.connect(PLUS1,MATHIUS_COLOR,"","","");
		gui.connect(MINUS2,"",VOICE_VOLUME,"","");
		gui.connect(PLUS2,VOICE_VOLUME,"","","");
					
		gui.pointer = MATHIUS_COLOR;
		
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.W)){//up
			gui.swipe(Direction.Up);
		}
		if(Input.GetKeyDown(KeyCode.A)){//left
			gui.swipe(Direction.Left);
			switch(gui.pointer){
				case MINUS1:
				case MINUS2:
					gui.selectOption(gui.pointer);
					gui.swipe(Direction.Right);
					break;
				case MUSIC:
					gui.SetGUISliderProperty(MUSIC_DISPLAY,-10.0f);
					break;
				case EFFECTS:
					gui.SetGUISliderProperty(EFFECTS_DISPLAY,-10.0f);
					break;				
				default:
					break;
			}
		}
		if(Input.GetKeyDown(KeyCode.S)){//down
			gui.swipe(Direction.Down);
		}
		if(Input.GetKeyDown(KeyCode.D)){//right
			gui.swipe(Direction.Right);
			switch(gui.pointer){
				case PLUS1:
				case PLUS2:
					gui.selectOption(gui.pointer);
					gui.swipe(Direction.Left);
					break;
				case MUSIC:
					gui.SetGUISliderProperty(MUSIC_DISPLAY,10.0f);
					break;
				case EFFECTS:
					gui.SetGUISliderProperty(EFFECTS_DISPLAY,10.0f);
					break;
				default:
					break;
			}
		}
		if(Input.GetKeyDown(KeyCode.Return)){
			gui.selectOption(gui.pointer);
			switch(gui.pointer){
				case PERCEPTUAL:
					gui.selectOption(PERCEPTUAL_DISPLAY);
					break;
				case MUTE:
					gui.selectOption(MUTE_DISPLAY);
					break;
				default:
					break;
			}
		}
	}
	
	void OnGUI(){
		gui.RenderGUIObjects(gui);
	}
	
	void HandleGuiOnToggle (object sender, ButtonName e)
	{
		switch(e.name){
			case PERCEPTUAL_DISPLAY:
				_perceptual = e.state;
				break;
			case MUTE_DISPLAY:
				_mute = e.state;
				break;
		}
	}

	void HandleGuiOnScroll (object sender, ButtonName e)
	{
		switch(e.name){
			case MUSIC_DISPLAY:
				_music = e.amount;
				gui.SetGUINameProperty(MUSIC,("Music: " + (Mathf.Round(_music *100f)/100f)));
				break;
			case EFFECTS_DISPLAY:
				_sfx = e.amount;
				gui.SetGUINameProperty(EFFECTS,"Effects: " + (Mathf.Round(_sfx*100f)/100f));
				break;
			default:
				break;
		}
	}

	void HandleGuiOnClick (object sender, ButtonName e)
	{
		switch(e.name){
			case MINUS1: //texture
				if(_texture>0) _texture--;
				gui.SetGUINameProperty(MATHIUS_COLOR_DISPLAY,colorArray[_texture]);
				MasterController.BRAIN.m().set_texture(_texturemap[colorArray[_texture]]);
				break;
			case MINUS2: //perceptual sound
				if(_sound>0) _sound--;
				gui.SetGUINameProperty(VOICE_VOLUME_DISPLAY,soundArray[_sound]);
				break;
			case PLUS1: //texture
				if(_texture<(_mathiusTextures.Length-1)) _texture++;
				gui.SetGUINameProperty(MATHIUS_COLOR_DISPLAY,colorArray[_texture]);
				MasterController.BRAIN.m().set_texture(_texturemap[colorArray[_texture]]);
				break;
			case PLUS2: // perceptual sound
				if(_sound<(soundArray.Length-1)) _sound++;
				gui.SetGUINameProperty(VOICE_VOLUME_DISPLAY,soundArray[_sound]);
				break;
			case MAIN_MENU:
				prefs.set_mathiusTexture(_texture);
				prefs.set_musicVolume(_music);
				prefs.set_mute(_mute);
				prefs.set_SFXVolume(_sfx);
				prefs.set_perceptualVolume(_sound);
				prefs.set_usePerceptual(_perceptual);
				MasterController.BRAIN.pci().set_using_PCI(_perceptual);
				Application.LoadLevel("MainMenu");
				break;
			default:
				break;
		}
		
		
	}
}