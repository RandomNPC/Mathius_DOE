using UnityEngine;
using System.Collections;

public class LevelEditorUI : MonoBehaviour {
	
	public GUISkin thisMetalGUISkin;
	private GUIManager gui;
	private PreferencesManager prefs;
	private PCInterface pc;
	
	private int _terrain_num;
	private int _format_num;
	private int _num_to_win;
	private float _alien_speed;
	
	private bool[] toggleTerrain = {false,false,false,false,false,false,false,false};
	private string[] terrainNames = {"Island","Forest","Desert","Moon","Snow","Volcano","Forest","Zack's Map"};
	private bool[] toggleOperation = {false,false,false,false};
	private string[] formatArray = {"Arithmetic","Algebra","Mixed"};
	
	private const string LEVEL_EDITOR = "Settings";
	private const string NUMBER_OF_TERRAINS = "Number of Terrains : ";
	private const string MINUS1 = "-1";
	private const string TERRAIN_NUM = "Terrain Num";
	private const string PLUS1 = "+1";
	private const string RESET = "Reset";
	private const string SAVE = "Save";
	private const string CUSTOM_GAME = "Custom Game";
	private const string TERRAIN_SELECTION = "Terrain Selection";
	private const string TERRAIN0 = "Terrain0";
	private const string TERRAIN1 = "Terrain1";
	private const string TERRAIN2 = "Terrain2";
	private const string TERRAIN3 = "Terrain3";
	private const string TERRAIN4 = "Terrain4";
	private const string TERRAIN5 = "Terrain5";
	private const string TERRAIN6 = "Terrain6";
	private const string TERRAIN7 = "Terrain7";
	private const string PLUS = "+";
	private const string MINUS = "-";
	private const string DIVIDE = "/";
	private const string MULTIPLY = "x";
	private const string MATH_OPERATIONS = "Math Operations";
	private const string FORMAT = "Format: ";
	private const string MINUS2 = "-2";
	private const string PLUS2 = "+2";
	private const string EQUATION_FORMAT = "Equation Format";
	private const string MINUS3 = "-3";
	private const string PLUS3 = "+3";
	private const string NUMBER_TO_WIN = "Number to win? ";
	private const string NUMBER_TO_WIN_VALUE = "Number to win value";
	private const string ALIEN_SPEED = "Alien Speed: ";
	private const string ALIEN_SLIDER_SPEED = "Alien slider speed";
	

	void Start(){
		SoundManager.SOUNDS.playSound(SoundManager.UI_CLICK,MasterController.UI_CAMERA_ALT);
		gui = new GUIManager(thisMetalGUISkin);
		gui.OnClick += HandleGuiOnClick;
		gui.OnToggle += HandleGuiOnToggle;
		gui.OnScroll += HandleGuiOnScroll;
		
		prefs = MasterController.BRAIN.pm();
		pc = MasterController.BRAIN.pci();
		
		pc.onGesturePerformed += HandlePconGesturePerformed;
		
		_terrain_num = prefs.get_tileNum();
		_format_num = prefs.get_eqFormat();
		_alien_speed = prefs.get_alienSpeed();
		_num_to_win = prefs.get_numWin();
		loadSettingsFromPreferences();
		
		gui.CreateGUIObject(LEVEL_EDITOR,
							"Level Editor",
							new Rect((Screen.width/5)/2,(3*(Screen.height/100)),(4*(Screen.width/5)),(18*(Screen.height/100))),
							GUIType.Label,
							"label");
		//Number of Terrains
		/*gui.CreateGUIObject(NUMBER_OF_TERRAINS,
							"Number of Terrains : ",
							new Rect((Screen.width/100)*20, (Screen.height/100)*25,(Screen.width/100)*50, (Screen.height/100)*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(MINUS1,
							"-",
							new Rect((Screen.width/100)*50, (Screen.height/100)*25,(Screen.width/100)*5, (Screen.height/100)*5),
							GUIType.Button,
							"button");
		gui.CreateGUIObject(TERRAIN_NUM,
							_terrain_num.ToString(),
							new Rect((Screen.width/100)*55, (Screen.height/100)*26,(Screen.width/100)*50, (Screen.height/100)*5),
							GUIType.Label,
							"nobox");
		gui.CreateGUIObject(PLUS1,
							"+",
							new Rect((Screen.width/100)*60, (Screen.height/100)*25,(Screen.width/100)*5, (Screen.height/100)*5),
							GUIType.Button,
							"button");*/
		//Reset
		gui.CreateGUIObject(RESET,
							"Reset",
							new Rect(71*(Screen.width/100) ,(94*(Screen.height/100)) ,(3*(Screen.width/10)) ,(15*(Screen.height/100))),
							GUIType.Button,
							"box");
		//Save
		gui.CreateGUIObject(SAVE,
							"Save",
							new Rect(42*(Screen.width/100) ,(94*(Screen.height/100)) ,(29*(Screen.width/100)) ,(15*(Screen.height/100))),
							GUIType.Button,
							"box");
		//Custom Game
		gui.CreateGUIObject(CUSTOM_GAME,
							"Custom",
							new Rect((Screen.width/100) ,(94*(Screen.height/100)) ,(4*(Screen.width/10)) ,(15*(Screen.height/100))),
							GUIType.Button,
							"box");
		//Terrain Selection
		gui.CreateGUIObject(TERRAIN_SELECTION,
							"Terrain Selection",
							new Rect((Screen.width/100)*20, (Screen.height/100)*30,(Screen.width/100)*50, (Screen.height/100)*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(TERRAIN0,
							terrainNames[0],
							new Rect((Screen.width/100)*20, (Screen.height/100)*35, 100, 30),
							GUIType.Toggle,
							"toggle",
							toggleTerrain[0]);
		gui.CreateGUIObject(TERRAIN1,
							terrainNames[1],
							new Rect((Screen.width/100)*60, (Screen.height/100)*35, 100, 30),
							GUIType.Toggle,
							"toggle",
							toggleTerrain[1]);
		gui.CreateGUIObject(TERRAIN2,
							terrainNames[2],
							new Rect((Screen.width/100)*20, (Screen.height/100)*40, 100, 30),
							GUIType.Toggle,
							"toggle",
							toggleTerrain[2]);
		gui.CreateGUIObject(TERRAIN3,
							terrainNames[3],
							new Rect((Screen.width/100)*60, (Screen.height/100)*40, 100, 30),
							GUIType.Toggle,
							"toggle",
							toggleTerrain[3]);
		gui.CreateGUIObject(TERRAIN4,
							terrainNames[4],
							new Rect((Screen.width/100)*20, (Screen.height/100)*45, 100, 30),
							GUIType.Toggle,
							"toggle",
							toggleTerrain[4]);
		gui.CreateGUIObject(TERRAIN5,
							terrainNames[5],
							new Rect((Screen.width/100)*60, (Screen.height/100)*45, 100, 30),
							GUIType.Toggle,
							"toggle",
							toggleTerrain[5]);
		gui.CreateGUIObject(TERRAIN6,
							terrainNames[6],
							new Rect((Screen.width/100)*20, (Screen.height/100)*50, 100, 30),
							GUIType.Toggle,
							"toggle",
							toggleTerrain[6]);
		gui.CreateGUIObject(TERRAIN7,
							terrainNames[7],
							new Rect((Screen.width/100)*60, (Screen.height/100)*50, 100, 30),
							GUIType.Toggle,
							"toggle",
							toggleTerrain[7]);
		//Math Operations
		gui.CreateGUIObject(MATH_OPERATIONS,
							"Math Operations",
							new Rect((Screen.width/100)*20, (Screen.height/100)*55,(Screen.width/100)*50, (Screen.height/100)*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(PLUS,
							"+",
							new Rect((Screen.width/100)*20, (Screen.height/100)*60, 100, 30),
							GUIType.Toggle,
							"toggle",
							toggleOperation[0]);
		gui.CreateGUIObject(MINUS,
							"-",
							new Rect((Screen.width/100)*60, (Screen.height/100)*60, 100, 30),
							GUIType.Toggle,
							"toggle",
							toggleOperation[1]);
		gui.CreateGUIObject(MULTIPLY,
							"X",
							new Rect((Screen.width/100)*20, (Screen.height/100)*65, 100, 30),
							GUIType.Toggle,
							"toggle",
							toggleOperation[2]);
		gui.CreateGUIObject(DIVIDE,
							"%",
							new Rect((Screen.width/100)*60, (Screen.height/100)*65, 100, 30),
							GUIType.Toggle,
							"toggle",
							toggleOperation[3]);
		//Equation Format
		gui.CreateGUIObject(FORMAT,
							"Format: ",
							new Rect((Screen.width/100)*20, (Screen.height/100)*70,(Screen.width/100)*50, (Screen.height/100)*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(MINUS2,
							"-",
							new Rect((Screen.width/100)*40, (Screen.height/100)*70,(Screen.width/100)*5, (Screen.height/100)*5),
							GUIType.Button,
							"button");
		gui.CreateGUIObject(EQUATION_FORMAT,
							formatArray[_format_num],
							new Rect((Screen.width/100)*45, (Screen.height/100)*71,(Screen.width/100)*50, (Screen.height/100)*5),
							GUIType.Label,
							"nobox");
		gui.CreateGUIObject(PLUS2,
							"+",
							new Rect((Screen.width/100)*60, (Screen.height/100)*70,(Screen.width/100)*5, (Screen.height/100)*5),
							GUIType.Button,
							"button");
		//win set
		gui.CreateGUIObject(NUMBER_TO_WIN,
							"Number to win? ",
							new Rect((Screen.width/100)*20, (Screen.height/100)*75,(Screen.width/100)*50, (Screen.height/100)*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(MINUS3,
							"-",
							new Rect((Screen.width/100)*50, (Screen.height/100)*75,(Screen.width/100)*5, (Screen.height/100)*5),
							GUIType.Button,
							"button");
		gui.CreateGUIObject(PLUS3,
							"+",
							new Rect((Screen.width/100)*60, (Screen.height/100)*75,(Screen.width/100)*5, (Screen.height/100)*5),
							GUIType.Button,
							"button");
		gui.CreateGUIObject(NUMBER_TO_WIN_VALUE,
							_num_to_win.ToString(),
							new Rect((Screen.width/100)*55, (Screen.height/100)*76,(Screen.width/100)*50, (Screen.height/100)*5),
							GUIType.Label,
							"nobox");
		//Alien Speed
		gui.CreateGUIObject(ALIEN_SPEED,
							"Alien Speed: " + (Mathf.Round(_alien_speed *100f)/100f),
							new Rect((Screen.width/100)*20, (Screen.height/100)*80,(Screen.width/100)*50, (Screen.height/100)*5),
							GUIType.Label,
							"button");
		gui.CreateGUIObject(ALIEN_SLIDER_SPEED,
							"ALIEN_SLIDER_SPEED",
							new Rect ((Screen.width/100)*50, (Screen.height/100)*80, (Screen.width/100)*20, (Screen.height/100)*2),
							GUIType.Slider,
							"",
							false,
							0.25f,
							1.0f,
							_alien_speed);
		
		
		gui.connect(TERRAIN0,TERRAIN1,TERRAIN1,CUSTOM_GAME,TERRAIN2);
		gui.connect(TERRAIN1,TERRAIN0,TERRAIN0,CUSTOM_GAME,TERRAIN3);
		gui.connect(TERRAIN2,TERRAIN3,TERRAIN3,TERRAIN0,TERRAIN4);
		gui.connect(TERRAIN3,TERRAIN2,TERRAIN2,TERRAIN1,TERRAIN5);
		gui.connect(TERRAIN4,TERRAIN5,TERRAIN5,TERRAIN2,TERRAIN6);
		gui.connect(TERRAIN5,TERRAIN4,TERRAIN4,TERRAIN3,TERRAIN7);
		gui.connect(TERRAIN6,TERRAIN7,TERRAIN7,TERRAIN4,PLUS);
		gui.connect(TERRAIN7,TERRAIN6,TERRAIN6,TERRAIN5,MINUS);
		gui.connect(PLUS,MINUS,MINUS,TERRAIN6,MULTIPLY);
		gui.connect(MINUS,PLUS,PLUS,TERRAIN7,DIVIDE);
		gui.connect(MULTIPLY,DIVIDE,DIVIDE,MINUS,FORMAT);
		gui.connect(DIVIDE,MULTIPLY,MULTIPLY,MINUS,FORMAT);
		gui.connect(FORMAT,MINUS2,PLUS2,MULTIPLY,NUMBER_TO_WIN);
		gui.connect(NUMBER_TO_WIN,MINUS3,PLUS3,FORMAT,ALIEN_SPEED);
		gui.connect(ALIEN_SPEED,"","",NUMBER_TO_WIN,CUSTOM_GAME);
		gui.connect(CUSTOM_GAME,RESET,SAVE,ALIEN_SPEED,TERRAIN0);
		gui.connect(SAVE,CUSTOM_GAME,RESET,ALIEN_SPEED,TERRAIN0);
		gui.connect(RESET,SAVE,CUSTOM_GAME,ALIEN_SPEED,TERRAIN0);
		
		
		
		gui.connect(MINUS1,"",NUMBER_OF_TERRAINS,"","");
		gui.connect(PLUS1,NUMBER_OF_TERRAINS,"","","");
		gui.connect(MINUS2,"",FORMAT,"","");
		gui.connect(PLUS2,FORMAT,"","","");
		gui.connect(MINUS3,"",NUMBER_TO_WIN,"","");
		gui.connect(PLUS3,NUMBER_TO_WIN,"","","");
		
		
		gui.pointer = TERRAIN0;
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
				case MINUS3:
					gui.selectOption(gui.pointer);
					gui.swipe(Direction.Right);
					break;
				case ALIEN_SPEED:
					gui.SetGUISliderProperty(ALIEN_SLIDER_SPEED,-0.1f);
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
				case PLUS3:
					gui.selectOption(gui.pointer);
					gui.swipe(Direction.Left);
					break;
				case ALIEN_SPEED:
					gui.SetGUISliderProperty(ALIEN_SLIDER_SPEED,0.1f);
					break;
				default:
					break;
			}
		}
		if(Input.GetKeyDown(KeyCode.Return)){
			gui.selectOption(gui.pointer);
		}
	}
	
	void HandlePconGesturePerformed (object sender, PCGesture e)
	{
		switch(e.gesture){
			case Gesture.UP:
				gui.swipe(Direction.Up);
				break;
			case Gesture.DOWN:
				gui.swipe(Direction.Down);
				break;
			case Gesture.LEFT:
				gui.swipe(Direction.Left);
				switch(gui.pointer){
					case MINUS1:
					case MINUS2:
					case MINUS3:
						gui.selectOption(gui.pointer);
						gui.swipe(Direction.Right);
						break;
					case ALIEN_SPEED:
						gui.SetGUISliderProperty(ALIEN_SLIDER_SPEED,-0.1f);
						break;
					default:
						break;
				}
				break;
			case Gesture.RIGHT:
				gui.swipe(Direction.Right);
				switch(gui.pointer){
					case PLUS1:
					case PLUS2:
					case PLUS3:
						gui.selectOption(gui.pointer);
						gui.swipe(Direction.Left);
						break;
					case ALIEN_SPEED:
						gui.SetGUISliderProperty(ALIEN_SLIDER_SPEED,0.1f);
						break;
					default:
						break;
				}
				break;
			case Gesture.SELECT:
				gui.selectOption(gui.pointer);
				break;
		}
	}
	
	void OnGUI(){
		gui.RenderGUIObjects(gui);	
	}
	
	void HandleGuiOnClick (object sender, ButtonName e)
	{
		switch(e.name){
			case MINUS1://terrain
				if(_terrain_num>1) _terrain_num--;
				gui.SetGUINameProperty(TERRAIN_NUM,_terrain_num.ToString());
		        prefs.set_tileNum(_terrain_num);
				break;
			case MINUS2://eq format
				if(_format_num>0) _format_num--;
				gui.SetGUINameProperty(EQUATION_FORMAT,formatArray[_format_num]);		      		
				prefs.set_eqFormat(_format_num);
				break;
			case MINUS3://num to win
				if(_num_to_win>1) _num_to_win--;
				gui.SetGUINameProperty(NUMBER_TO_WIN_VALUE,_num_to_win.ToString());
				prefs.set_numWin(_num_to_win);
				break;
			case PLUS1://terrain
				_terrain_num++;
				gui.SetGUINameProperty(TERRAIN_NUM,_terrain_num.ToString());
	          	prefs.set_tileNum(_terrain_num);
				break;
			case PLUS2://eq format
				if(_format_num<(formatArray.Length-1)) _format_num++;
				gui.SetGUINameProperty(EQUATION_FORMAT,formatArray[_format_num]);
		      	prefs.set_eqFormat(_format_num);
				break;
			case PLUS3://num to win
				_num_to_win++;
				gui.SetGUINameProperty(NUMBER_TO_WIN_VALUE,_num_to_win.ToString());
				prefs.set_numWin(_num_to_win);
				break;
			case SAVE:
				Application.LoadLevel("MainMenu");
				break;
			case RESET:
				prefs.set_operations((byte)15);
	          	prefs.set_tileNum(1);
	      		prefs.set_terrains((byte)1);
	      		prefs.set_eqFormat(0);
	      		prefs.set_numWin(25);
	      		prefs.set_alienSpeed(0.0f);
				Application.LoadLevel("MainMenu");
				break;
			case CUSTOM_GAME:
				Application.LoadLevel("Earth Scene");
				break;
			default:
				break;
		}
	}
	
	void HandleGuiOnToggle (object sender, ButtonName e)
	{
		switch(e.name){
			case TERRAIN0:
				toggleTerrain[0] = e.state;
				break;
			case TERRAIN1:
				toggleTerrain[1] = e.state;
				break;
			case TERRAIN2:
				toggleTerrain[2] = e.state;
				break;
			case TERRAIN3:
				toggleTerrain[3] = e.state;
				break;
			case TERRAIN4:
				toggleTerrain[4] = e.state;
				break;
			case TERRAIN5:
				toggleTerrain[5] = e.state;
				break;
			case TERRAIN6:
				toggleTerrain[6] = e.state;
				break;
			case TERRAIN7:
				toggleTerrain[7] = e.state;
				break;
			case PLUS:
				toggleOperation[0] = e.state;
				break;
			case MINUS:
				toggleOperation[1] = e.state;
				break;
			case MULTIPLY:
				toggleOperation[2] = e.state;
				break;
			case DIVIDE:
				toggleOperation[3] = e.state;
				break;
			default:
				break;
		}
		prefs.set_terrains(using_terrains());
		prefs.set_operations(using_operations());
	}
	
	void HandleGuiOnScroll (object sender, ButtonName e)
	{
		switch(e.name){
			case ALIEN_SLIDER_SPEED:
				_alien_speed = e.amount;
				gui.SetGUINameProperty(ALIEN_SPEED,"Alien Speed: " +(Mathf.Round(_alien_speed *100f)/100f));
				prefs.set_alienSpeed(_alien_speed);
				break;
			default:
				break;
		}
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
		if(toggleOperation[0]){temp = (byte)(temp | EquationGenerator.ADDITION);}
		if(toggleOperation[1]){temp = (byte)(temp | EquationGenerator.SUBTRACTION);}
		if(toggleOperation[2]){temp = (byte)(temp | EquationGenerator.MULTIPLICATION);}
		if(toggleOperation[3]){temp = (byte)(temp | EquationGenerator.DIVISION);}
		return temp;
	}
	
	private void loadSettingsFromPreferences(){

		byte terrainTemp = prefs.get_terrains();
		if((terrainTemp & TerrainManager.TERRAIN_1) == TerrainManager.TERRAIN_1){toggleTerrain[0] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_2) == TerrainManager.TERRAIN_2){toggleTerrain[1] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_3) == TerrainManager.TERRAIN_3){toggleTerrain[2] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_4) == TerrainManager.TERRAIN_4){toggleTerrain[3] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_5) == TerrainManager.TERRAIN_5){toggleTerrain[4] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_6) == TerrainManager.TERRAIN_6){toggleTerrain[5] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_7) == TerrainManager.TERRAIN_7){toggleTerrain[6] = true;}
		if((terrainTemp & TerrainManager.TERRAIN_8) == TerrainManager.TERRAIN_8){toggleTerrain[7] = true;}

		byte opTemp = prefs.get_operations();
		if((opTemp & EquationGenerator.ADDITION) == EquationGenerator.ADDITION){toggleOperation[0] = true;}
		if((opTemp & EquationGenerator.SUBTRACTION) == EquationGenerator.SUBTRACTION){toggleOperation[1] = true;}
		if((opTemp & EquationGenerator.MULTIPLICATION) == EquationGenerator.MULTIPLICATION){toggleOperation[2] = true;}
		if((opTemp & EquationGenerator.DIVISION) == EquationGenerator.DIVISION){toggleOperation[3] = true;}

	}
		
}
