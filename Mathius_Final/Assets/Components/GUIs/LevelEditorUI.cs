using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEditorUI : MonoBehaviour {
	public GUISkin thisMetalGUISkin;
	private int terrainNum;
	private bool toggleTxt1;
	private bool toggleTxt2;
	private bool toggleTxt3;
	private bool toggleTxt4;
	private bool toggleTxt5;
	private bool toggleTxt6;
	private bool toggleTxt7;
	private bool toggleTxt8;
	private bool opToggleTxt1;
	private bool opToggleTxt2;
	private bool opToggleTxt3;
	private bool opToggleTxt4;
	private int winNum;
	public float hSliderValue;
	private int formatInt;
	private string[] formatArray;
	private string[] terrainNames;
	private Dictionary<string,int> format;
	private MasterController mc;
	private ScoreManager sm;
	
	// Use this for initialization
	void Start () {
		sm = MasterController.BRAIN.sm();
		winNum = MasterController.BRAIN.pm().get_numWin();
		hSliderValue = MasterController.BRAIN.pm().get_alienSpeed();
		loadSettingsFromPreferences();
		
		terrainNames = new string[]{"Terrain1","Terrain2","Terrain3","Terrain4","Terrain5","Terrain6","Terrain7","Terrain8"};
		formatArray =new string [3];
		formatArray[0] ="Arithmetic";
		formatArray[1] ="Algebra";
		formatArray[2] ="Mixed";
		format = new Dictionary<string,int>();
		format.Clear();
		format.Add(formatArray[0],EquationGenerator.ALGEBRA);
		format.Add(formatArray[1],EquationGenerator.ARITHMETIC);
		format.Add(formatArray[2],EquationGenerator.MIXED);
	}
	
	void OnGUI(){
		float intDivider = Screen.height/100;
		float widthDivider = Screen.width/100;
		//hSliderValue = 0.0F;
		Rect titleRect = new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider));
		GUI.skin = thisMetalGUISkin;
		//Title
		GUI.Label(titleRect, ("Level Editor"),GUI.skin.GetStyle("label"));
		//First Label
		GUI.Label(new Rect(widthDivider*20, intDivider*25,widthDivider*50, intDivider*5), ("Number of Terrains : "),GUI.skin.GetStyle("button"));
		//Terrain Decrementer
		if(GUI.Button(new Rect(widthDivider*50, intDivider*25,widthDivider*5, intDivider*5),("-"),GUI.skin.GetStyle("button"))){
			if(terrainNum>1){
				terrainNum --;
			}
		}
		//Terrain int
		GUI.Label(new Rect(widthDivider*55, intDivider*26,widthDivider*50, intDivider*5), ("" +terrainNum),GUI.skin.GetStyle("toggle"));
		//Terrain Incrementer
		if(GUI.Button(new Rect(widthDivider*60, intDivider*25,widthDivider*5, intDivider*5),("+"),GUI.skin.GetStyle("button"))){
			if(terrainNum>=1){
				terrainNum ++;
			}
		}
		//Reset
		if(GUI.Button (new Rect(71*(Screen.width/100) ,(94*intDivider) ,(3*(Screen.width/10)) ,(15*intDivider) ) ,("Reset") ,GUI.skin.GetStyle("box") ) ){
			SoundManager.SOUNDS.playSound(SoundManager.UI_CLICK,CameraCollider.MATHIUS_EARTH_CAM);
			MasterController.BRAIN.pm().set_operations(using_operations());
			MasterController.BRAIN.pm().set_tileNum(num_of_terrains());
			MasterController.BRAIN.pm().set_terrains(using_terrains());
			MasterController.BRAIN.pm().set_eqFormat(using_eqFormat());
			MasterController.BRAIN.pm().set_numWin(winNum);
			PlayerPrefs.SetInt("win_number",winNum);
			MasterController.BRAIN.pm().set_alienSpeed(hSliderValue);
			PlayerPrefs.SetFloat("_gameSpeed",hSliderValue);
			Application.LoadLevel("MainMenu");
		}
		//Save
		if(GUI.Button (new Rect(42*(Screen.width/100) ,(94*intDivider) ,(29*(Screen.width/100)) ,(15*intDivider) ) ,("Save") ,GUI.skin.GetStyle("box") ) ){
			SoundManager.SOUNDS.playSound(SoundManager.UI_CLICK,CameraCollider.MATHIUS_EARTH_CAM);
			MasterController.BRAIN.pm().set_operations(using_operations());
			MasterController.BRAIN.pm().set_tileNum(num_of_terrains());
			MasterController.BRAIN.pm().set_terrains(using_terrains());
			MasterController.BRAIN.pm().set_eqFormat(using_eqFormat());
			MasterController.BRAIN.pm().set_numWin(winNum);
			PlayerPrefs.SetInt("win_number",winNum);
			MasterController.BRAIN.pm().set_alienSpeed(hSliderValue);
			PlayerPrefs.SetFloat("_gameSpeed",hSliderValue);
			Application.LoadLevel("MainMenu");
		}
		//Custom Game
		if(GUI.Button (new Rect((Screen.width/100) ,(94*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Custom") ,GUI.skin.GetStyle("box") ) ){
			SoundManager.SOUNDS.playSound(SoundManager.UI_CLICK,CameraCollider.MATHIUS_EARTH_CAM);
			MasterController.BRAIN.pm().set_operations(using_operations());
			MasterController.BRAIN.pm().set_tileNum(num_of_terrains());
			MasterController.BRAIN.pm().set_terrains(using_terrains());
			MasterController.BRAIN.pm().set_eqFormat(using_eqFormat());
			MasterController.BRAIN.pm().set_numWin(winNum);
			PlayerPrefs.SetInt("win_number",winNum);
			MasterController.BRAIN.pm().set_alienSpeed(hSliderValue);
			PlayerPrefs.SetFloat("_gameSpeed",hSliderValue);
			Application.LoadLevel("Earth Scene");
		}
		
		
		//Terrain Toggle
		GUI.Label(new Rect(widthDivider*20, intDivider*30,widthDivider*50, intDivider*5), ("Terrain Selection"),GUI.skin.GetStyle("button"));
		toggleTxt1 = GUI.Toggle(new Rect(widthDivider*20, intDivider*35, 100, 30), toggleTxt1, terrainNames[0]);
		toggleTxt2 = GUI.Toggle(new Rect(widthDivider*60, intDivider*35, 100, 30), toggleTxt2, terrainNames[1]);
		toggleTxt3 = GUI.Toggle(new Rect(widthDivider*20, intDivider*40, 100, 30), toggleTxt3, terrainNames[2]);
		toggleTxt4 = GUI.Toggle(new Rect(widthDivider*60, intDivider*40, 100, 30), toggleTxt4, terrainNames[3]);
		toggleTxt5 = GUI.Toggle(new Rect(widthDivider*20, intDivider*45, 100, 30), toggleTxt5, terrainNames[4]);
		toggleTxt6 = GUI.Toggle(new Rect(widthDivider*60, intDivider*45, 100, 30), toggleTxt6, terrainNames[5]);
		toggleTxt7 = GUI.Toggle(new Rect(widthDivider*20, intDivider*50, 100, 30), toggleTxt7, terrainNames[6]);
		toggleTxt8 = GUI.Toggle(new Rect(widthDivider*60, intDivider*50, 100, 30), toggleTxt8, terrainNames[7]);
		//Opperations Toggle
		GUI.Label(new Rect(widthDivider*20, intDivider*55,widthDivider*50, intDivider*5), ("Math Operations"),GUI.skin.GetStyle("button"));
		opToggleTxt1 = GUI.Toggle(new Rect(widthDivider*20, intDivider*60, 100, 30), opToggleTxt1, "+");
		opToggleTxt2 = GUI.Toggle(new Rect(widthDivider*60, intDivider*60, 100, 30), opToggleTxt2, "-");
		opToggleTxt3 = GUI.Toggle(new Rect(widthDivider*20, intDivider*65, 100, 30), opToggleTxt3, "X");
		opToggleTxt4 = GUI.Toggle(new Rect(widthDivider*60, intDivider*65, 100, 30), opToggleTxt4, "%");
		//Eqaution Format
		GUI.Label(new Rect(widthDivider*20, intDivider*70,widthDivider*50, intDivider*5), ("Format: "),GUI.skin.GetStyle("button"));
		//Equation Decrementer
		if(GUI.Button(new Rect(widthDivider*40, intDivider*70,widthDivider*5, intDivider*5),("-"),GUI.skin.GetStyle("button"))){
			if(formatInt>0){
				formatInt --;
			}
		}
		//Equation int
		GUI.Label(new Rect(widthDivider*45, intDivider*71,widthDivider*50, intDivider*5), ("" +formatArray[formatInt]),GUI.skin.GetStyle("toggle"));
		//Equation Incrementer
		if(GUI.Button(new Rect(widthDivider*60, intDivider*70,widthDivider*5, intDivider*5),("+"),GUI.skin.GetStyle("button"))){
			if( formatInt<2){
				formatInt ++;
			}
		}
		//Win Int
		GUI.Label(new Rect(widthDivider*20, intDivider*75,widthDivider*50, intDivider*5), ("Number to win? "),GUI.skin.GetStyle("button"));
		//Win Decrementer
		if(GUI.Button(new Rect(widthDivider*50, intDivider*75,widthDivider*5, intDivider*5),("-"),GUI.skin.GetStyle("button"))){
			if(winNum>1){
				winNum --;
			}
		}
		//Win num
		GUI.Label(new Rect(widthDivider*55, intDivider*76,widthDivider*50, intDivider*5), ("" + winNum),GUI.skin.GetStyle("toggle"));
		//win Incrementer
		if(GUI.Button(new Rect(widthDivider*60, intDivider*75,widthDivider*5, intDivider*5),("+"),GUI.skin.GetStyle("button"))){
			if(winNum>=1){
				winNum ++;
			}
		}
		//Alien Speed
		GUI.Label(new Rect(widthDivider*20, intDivider*80,widthDivider*50, intDivider*5), ("Alien Speed: "+(Mathf.Round(hSliderValue *100f)/100f)),GUI.skin.GetStyle("button"));
		Rect slider = new Rect (widthDivider*50, intDivider*80, widthDivider*20, intDivider*2);
		hSliderValue = GUI.HorizontalSlider(slider, hSliderValue, 0.0F, 1.0F);
		
	}
	
	private void loadSettingsFromPreferences(){
		terrainNum = MasterController.BRAIN.pm().get_tileNum();
		formatInt = MasterController.BRAIN.pm().get_eqFormat();
		byte terrainTemp = MasterController.BRAIN.pm().get_terrains();
		if((terrainTemp & TerrainManager.TERRAIN_1) == TerrainManager.TERRAIN_1){toggleTxt1 = true;}
		if((terrainTemp & TerrainManager.TERRAIN_2) == TerrainManager.TERRAIN_2){toggleTxt2 = true;}
		if((terrainTemp & TerrainManager.TERRAIN_3) == TerrainManager.TERRAIN_3){toggleTxt3 = true;}
		if((terrainTemp & TerrainManager.TERRAIN_4) == TerrainManager.TERRAIN_4){toggleTxt4 = true;}
		if((terrainTemp & TerrainManager.TERRAIN_5) == TerrainManager.TERRAIN_5){toggleTxt5 = true;}
		if((terrainTemp & TerrainManager.TERRAIN_6) == TerrainManager.TERRAIN_6){toggleTxt6 = true;}
		if((terrainTemp & TerrainManager.TERRAIN_7) == TerrainManager.TERRAIN_7){toggleTxt7 = true;}
		if((terrainTemp & TerrainManager.TERRAIN_8) == TerrainManager.TERRAIN_8){toggleTxt8 = true;}
		
		byte opTemp = MasterController.BRAIN.pm().get_operations();
		if((opTemp & EquationGenerator.ADDITION) == EquationGenerator.ADDITION){opToggleTxt1 = true;}
		if((opTemp & EquationGenerator.SUBTRACTION) == EquationGenerator.SUBTRACTION){opToggleTxt2 = true;}
		if((opTemp & EquationGenerator.MULTIPLICATION) == EquationGenerator.MULTIPLICATION){opToggleTxt3 = true;}
		if((opTemp & EquationGenerator.DIVISION) == EquationGenerator.DIVISION){opToggleTxt4 = true;}
		
	}
	
	public int num_of_terrains(){
		PlayerPrefs.SetInt("num_of_terrains",terrainNum);
		PlayerPrefs.Save();
		return terrainNum;
	}
	
	public byte using_terrains(){
		byte temp = 0x0;
		if(toggleTxt1){temp = (byte)(temp | TerrainManager.TERRAIN_1);}
		if(toggleTxt2){temp = (byte)(temp | TerrainManager.TERRAIN_2);}
		if(toggleTxt3){temp = (byte)(temp | TerrainManager.TERRAIN_3);}
		if(toggleTxt4){temp = (byte)(temp | TerrainManager.TERRAIN_4);}
		if(toggleTxt5){temp = (byte)(temp | TerrainManager.TERRAIN_5);}
		if(toggleTxt6){temp = (byte)(temp | TerrainManager.TERRAIN_6);}
		if(toggleTxt7){temp = (byte)(temp | TerrainManager.TERRAIN_7);}
		if(toggleTxt8){temp = (byte)(temp | TerrainManager.TERRAIN_8);}
		PlayerPrefs.SetInt("using_terrains",(int)temp);
		PlayerPrefs.Save();
		return temp;
	}
	
	public byte using_operations(){
		byte temp = 0x00;
		if(opToggleTxt1){temp = (byte)(temp | EquationGenerator.ADDITION);}
		if(opToggleTxt2){temp = (byte)(temp | EquationGenerator.SUBTRACTION);}
		if(opToggleTxt3){temp = (byte)(temp | EquationGenerator.MULTIPLICATION);}
		if(opToggleTxt4){temp = (byte)(temp | EquationGenerator.DIVISION);}
		PlayerPrefs.SetInt("using_operations",(int)temp);
		PlayerPrefs.Save();
		return temp;
	}
	
	public int using_eqFormat(){
		PlayerPrefs.SetInt("using_eqFormat",formatInt);
		PlayerPrefs.Save();
		return format[formatArray[formatInt]];
	}
	
}
