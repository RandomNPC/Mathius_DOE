using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEditorUI : MonoBehaviour {
	public GUISkin thisMetalGUISkin;
	private int terrainNum;
	
	private bool[] toggleTerrain = {false,false,false,false,false,false,false,false};

	private bool opToggleTxt1;
	private bool opToggleTxt2;
	private bool opToggleTxt3;
	private bool opToggleTxt4;
	
	private int winNum;
	private float hSliderValue;
	private int formatInt;
	private string[] formatArray;
	private string[] terrainNames;
	private Dictionary<string,int> format;
	private PreferencesManager pref;
	
	
	// Use this for initialization
	void Start () {
		pref = MasterController.BRAIN.pm();
		SoundManager.SOUNDS.playSound(SoundManager.UI_CLICK,MasterController.UI_CAMERA_ALT);
		
		winNum = pref.get_numWin();
		hSliderValue = pref.get_alienSpeed();
		
		loadSettingsFromPreferences();
		
		terrainNames = new string[]{"Terrain1","Terrain2","Terrain3","Terrain4","Terrain5","Terrain6","Terrain7","Terrain8"};
		formatArray =new string[]{"Arithmetic","Algebra","Mixed"};
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
	      	PreferencesManager pref = MasterController.BRAIN.pm();
    	  	pref.set_operations((byte)15);
          	pref.set_tileNum(1);
      		pref.set_terrains((byte)1);
      		pref.set_eqFormat(format[formatArray[0]]);
      		pref.set_numWin(25);
      		pref.set_alienSpeed(0.0f);
			Application.LoadLevel("MainMenu");
		}
		//Save
		if(GUI.Button (new Rect(42*(Screen.width/100) ,(94*intDivider) ,(29*(Screen.width/100)) ,(15*intDivider) ) ,("Save") ,GUI.skin.GetStyle("box") ) ){
			PreferencesManager pref = MasterController.BRAIN.pm();
    	  	pref.set_operations(using_operations());
          	pref.set_tileNum(terrainNum);
      		pref.set_terrains(using_terrains());
      		pref.set_eqFormat(formatInt);
      		pref.set_numWin(winNum);
      		pref.set_alienSpeed(hSliderValue);
			Application.LoadLevel("MainMenu");
		}
		//Custom Game
		if(GUI.Button (new Rect((Screen.width/100) ,(94*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Custom") ,GUI.skin.GetStyle("box") ) ){
	      	PreferencesManager pref = MasterController.BRAIN.pm();
    	  	pref.set_operations(using_operations());
          	pref.set_tileNum(terrainNum);
      		pref.set_terrains(using_terrains());
      		pref.set_eqFormat(formatInt);
      		pref.set_numWin(winNum);
      		pref.set_alienSpeed(hSliderValue);
			Application.LoadLevel("Earth Scene");
		}
		
		
		//Terrain Toggle
		GUI.Label(new Rect(widthDivider*20, intDivider*30,widthDivider*50, intDivider*5), ("Terrain Selection"),GUI.skin.GetStyle("button"));
		toggleTerrain[0] = GUI.Toggle(new Rect(widthDivider*20, intDivider*35, 100, 30), toggleTerrain[0], terrainNames[0]);
		toggleTerrain[1] = GUI.Toggle(new Rect(widthDivider*60, intDivider*35, 100, 30), toggleTerrain[1], terrainNames[1]);
		toggleTerrain[2] = GUI.Toggle(new Rect(widthDivider*20, intDivider*40, 100, 30), toggleTerrain[2], terrainNames[2]);
		toggleTerrain[3] = GUI.Toggle(new Rect(widthDivider*60, intDivider*40, 100, 30), toggleTerrain[3], terrainNames[3]);
		toggleTerrain[4] = GUI.Toggle(new Rect(widthDivider*20, intDivider*45, 100, 30), toggleTerrain[4], terrainNames[4]);
		toggleTerrain[5] = GUI.Toggle(new Rect(widthDivider*60, intDivider*45, 100, 30), toggleTerrain[5], terrainNames[5]);
		toggleTerrain[6] = GUI.Toggle(new Rect(widthDivider*20, intDivider*50, 100, 30), toggleTerrain[6], terrainNames[6]);
		toggleTerrain[7] = GUI.Toggle(new Rect(widthDivider*60, intDivider*50, 100, 30), toggleTerrain[7], terrainNames[7]);
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
		if((terrainTemp & TerrainManager.TERRAIN_1) == TerrainManager.TERRAIN_1){toggleTerrain[0] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_2) == TerrainManager.TERRAIN_2){toggleTerrain[1] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_3) == TerrainManager.TERRAIN_3){toggleTerrain[2] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_4) == TerrainManager.TERRAIN_4){toggleTerrain[3] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_5) == TerrainManager.TERRAIN_5){toggleTerrain[4] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_6) == TerrainManager.TERRAIN_6){toggleTerrain[5] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_7) == TerrainManager.TERRAIN_7){toggleTerrain[6] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_8) == TerrainManager.TERRAIN_8){toggleTerrain[7] = true;}
		
		byte opTemp = MasterController.BRAIN.pm().get_operations();
		if((opTemp & EquationGenerator.ADDITION) == EquationGenerator.ADDITION){opToggleTxt1 = true;}
		if((opTemp & EquationGenerator.SUBTRACTION) == EquationGenerator.SUBTRACTION){opToggleTxt2 = true;}
		if((opTemp & EquationGenerator.MULTIPLICATION) == EquationGenerator.MULTIPLICATION){opToggleTxt3 = true;}
		if((opTemp & EquationGenerator.DIVISION) == EquationGenerator.DIVISION){opToggleTxt4 = true;}
		
	}
	
	private byte using_terrains(){
		byte temp = 0x0;
		if(toggleTerrain[0]){temp = (byte)(temp | TerrainManager.TERRAIN_1);}
		if(toggleTerrain[1]){temp = (byte)(temp | TerrainManager.TERRAIN_2);}
		if(toggleTerrain[2]){temp = (byte)(temp | TerrainManager.TERRAIN_3);}
		if(toggleTerrain[3]){temp = (byte)(temp | TerrainManager.TERRAIN_4);}
		if(toggleTerrain[4]){temp = (byte)(temp | TerrainManager.TERRAIN_5);}
		if(toggleTerrain[5]){temp = (byte)(temp | TerrainManager.TERRAIN_6);}
		if(toggleTerrain[6]){temp = (byte)(temp | TerrainManager.TERRAIN_7);}
		if(toggleTerrain[7]){temp = (byte)(temp | TerrainManager.TERRAIN_8);}
		return temp;
	}
	
	private byte using_operations(){
		byte temp = 0x00;
		if(opToggleTxt1){temp = (byte)(temp | EquationGenerator.ADDITION);}
		if(opToggleTxt2){temp = (byte)(temp | EquationGenerator.SUBTRACTION);}
		if(opToggleTxt3){temp = (byte)(temp | EquationGenerator.MULTIPLICATION);}
		if(opToggleTxt4){temp = (byte)(temp | EquationGenerator.DIVISION);}
		return temp;
	}
}
