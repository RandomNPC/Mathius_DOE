using UnityEngine;
using System.Collections;

public class PreferencesManager{
	
	private byte _terrains;
	private byte _operations;
	private int _tileNum;
	private int _eqFormat;
	private int _numWin;
	private float _alienSpeed;
	private int _mathiusTexturesInt;
	private bool _usePerceptual;
	private float _musicVolume;
	private float _SFXVolume;
	
	const string TERRAINS = "using_terrains";
	const string OPERATIONS = "using_operations";
	const string TILES = "num_of_terrains";
	const string EQ_FORMAT = "using_eqFormat";
	const string NUM_WIN = "win_number";
	const string ALIENSPEED = "_gameSpeed";
	const string TEXTUREINT = "_mathiusTextures";
	const string USEPERCEPTUAL = "useperceptual";
	const string MUSICVOLUME = "musicvolume";
	const string SFXVOLUME = "sfxvolume";
	
	public PreferencesManager(){
		_terrains = (byte)PlayerPrefs.GetInt(TERRAINS,1);
		_operations = (byte)PlayerPrefs.GetInt(OPERATIONS,15);
		_tileNum = PlayerPrefs.GetInt(TILES,1);
		_eqFormat = PlayerPrefs.GetInt(EQ_FORMAT,0);
		_numWin = PlayerPrefs.GetInt(NUM_WIN,25);
		_alienSpeed = PlayerPrefs.GetFloat(ALIENSPEED ,0.5f);
		_mathiusTexturesInt = PlayerPrefs.GetInt(TEXTUREINT,0);
		_usePerceptual = (PlayerPrefs.GetInt(USEPERCEPTUAL,0).Equals(0)) ? false : true;
		_musicVolume = PlayerPrefs.GetFloat(MUSICVOLUME,0.5f);
		_SFXVolume = PlayerPrefs.GetFloat(SFXVOLUME,0.5f);
		MonoBehaviour.print(_usePerceptual);
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
	
	public int get_mathiusTexture(){return _mathiusTexturesInt;}
	
	public void set_mathiusTexture(int text){
		_mathiusTexturesInt = text;
		PlayerPrefs.SetInt(TEXTUREINT,text);
		PlayerPrefs.Save();
	}
	
	public void set_usePerceptual(bool use){
		_usePerceptual = use;
		MasterController.BRAIN.pci().set_using_PCI(use);
		int mode = (use) ? 1 : 0;
		PlayerPrefs.SetInt(USEPERCEPTUAL,mode);
		PlayerPrefs.Save();
	}
	
	public bool get_usePerceptual(){return _usePerceptual;}
	
	public void set_musicVolume(float volume){
		_musicVolume = volume;
		PlayerPrefs.GetFloat(MUSICVOLUME,volume);
		PlayerPrefs.Save();
	}
	
	public float get_musicVolume(){return _musicVolume;}
	
	public void set_SFXVolume(float volume){
		_SFXVolume = volume;
		PlayerPrefs.GetFloat(SFXVOLUME,volume);
		PlayerPrefs.Save();
	}
	
	public float get_SFXVolume(){return _SFXVolume;} 
}
