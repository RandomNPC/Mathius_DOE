using UnityEngine;
using System.Collections;

public class PreferencesManager{
	
	private byte _terrains;
	private byte _operations;
	private int _tileNum;
	private int _eqFormat;
	private int _numWin;
	private float _alienSpeed;
	
	const string TERRAINS = "using_terrains";
	const string OPERATIONS = "using_operations";
	const string TILES = "num_of_terrains";
	const string EQ_FORMAT = "using_eqFormat";
	const string NUM_WIN = "win_number";
	const string ALIENSPEED = "_gameSpeed";
	
	public PreferencesManager(){
		_terrains = (byte)PlayerPrefs.GetInt(TERRAINS,1);
		_operations = (byte)PlayerPrefs.GetInt(OPERATIONS,15);
		_tileNum = PlayerPrefs.GetInt(TILES,1);
		_eqFormat = PlayerPrefs.GetInt(EQ_FORMAT,0);
		_numWin = PlayerPrefs.GetInt(NUM_WIN,25);
		_alienSpeed = PlayerPrefs.GetFloat(ALIENSPEED ,0.5f);
	}
	
	public void set_terrains(byte terrains){
		_terrains = terrains;
		PlayerPrefs.SetInt(TERRAINS,(int)terrains);
		PlayerPrefs.Save();
	}
	public byte get_terrains(){return _terrains;}
	public void set_operations(byte operations){
		_operations = operations;
		PlayerPrefs.SetInt(OPERATIONS,(int)operations);
		PlayerPrefs.Save();
	}
	public byte get_operations(){return _operations;}
	public void set_tileNum(int num){
		_tileNum = num;
		PlayerPrefs.SetInt(TILES,num);
		PlayerPrefs.Save();
	}
	public int get_tileNum(){return _tileNum;}
	public void set_eqFormat(int eqFormat){
		_eqFormat = eqFormat;
		PlayerPrefs.SetInt(EQ_FORMAT,eqFormat);
		PlayerPrefs.Save();
	}
	public int get_eqFormat(){return _eqFormat;}
	public void set_numWin(int winner){
		_numWin = winner;
		PlayerPrefs.SetInt(NUM_WIN,_numWin);
		PlayerPrefs.Save();
	}
	public int get_numWin(){return _numWin;}
	public void set_alienSpeed(float speed){
		_alienSpeed = speed;
		PlayerPrefs.SetFloat(ALIENSPEED,speed);
		PlayerPrefs.Save();
	}
	public float get_alienSpeed(){return _alienSpeed;}
	
}
