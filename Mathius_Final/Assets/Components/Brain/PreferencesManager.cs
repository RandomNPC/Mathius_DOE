using UnityEngine;
using System.Collections;

public class PreferencesManager{
	
	private byte _terrains;
	private byte _operations;
	private int _tileNum;
	private int _eqFormat;
	
	public PreferencesManager(){
		_terrains = (byte)PlayerPrefs.GetInt("using_terrains",1);
		_operations = (byte)PlayerPrefs.GetInt("using_operations",0);
		_tileNum = PlayerPrefs.GetInt("num_of_terrains",1);
		_eqFormat = PlayerPrefs.GetInt("using_eqFormat",0);
	}
	
	public void set_terrains(byte terrains){_terrains = terrains;}
	public byte get_terrains(){return _terrains;}
	public void set_operations(byte operations){_operations = operations;}
	public byte get_operations(){return _operations;}
	public void set_tileNum(int num){_tileNum = num;}
	public int get_tileNum(){return _tileNum;}
	public void set_eqFormat(int eqFormat){_eqFormat = eqFormat;}
	public int get_eqFormat(){return _eqFormat;}
	
}
