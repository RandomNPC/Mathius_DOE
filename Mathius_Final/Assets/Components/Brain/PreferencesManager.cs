using UnityEngine;
using System.Collections;

public class PreferencesManager{
	
	private byte _terrains;
	private byte _operations;
	private int _tileNum;
	private int _eqFormat;
	private int _numWin;
	private float _alienSpeed;
	
	public PreferencesManager(){
		_terrains = (byte)PlayerPrefs.GetInt("using_terrains",1);
		_operations = (byte)PlayerPrefs.GetInt("using_operations",15);
		_tileNum = PlayerPrefs.GetInt("num_of_terrains",1);
		_eqFormat = PlayerPrefs.GetInt("using_eqFormat",0);
		_numWin = PlayerPrefs.GetInt("win_number",25);
		_alienSpeed = PlayerPrefs.GetFloat("_gameSpeed" ,0.5f);
	}
	
	public void set_terrains(byte terrains){_terrains = terrains;}
	public byte get_terrains(){return _terrains;}
	public void set_operations(byte operations){_operations = operations;}
	public byte get_operations(){return _operations;}
	public void set_tileNum(int num){_tileNum = num;}
	public int get_tileNum(){return _tileNum;}
	public void set_eqFormat(int eqFormat){_eqFormat = eqFormat;}
	public int get_eqFormat(){return _eqFormat;}
	public void set_numWin(int winner){_numWin = winner;}
	public int get_numWin(){return _numWin;}
	public void set_alienSpeed(float speed){_alienSpeed = speed;}
	public float get_alienSpeed(){return _alienSpeed;}
	
}
