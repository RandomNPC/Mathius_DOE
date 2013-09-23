using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	private float _perceptualVolume;
	private Dictionary<int,float> _pVolume;
	private int _pVolumeMode;
	private bool _mute;
	
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
	const string PERCEPTUALVOLUME = "perceptualVolume";
	const string MUTE = "mute";
	
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
		_mute = (PlayerPrefs.GetInt(MUTE,0).Equals(1)) ? true : false;
		
		_pVolume = new Dictionary<int, float>();
		_pVolume.Clear();		
		_pVolume.Add (0,0.0f);
		_pVolume.Add (1,0.5f);
		_pVolume.Add (2,1.0f);
		
		int state = PlayerPrefs.GetInt(PERCEPTUALVOLUME,0);
		
		_perceptualVolume = _pVolume[state];
		_pVolumeMode = state;
	}
	
	public void set_terrains(byte terrains){
		if(terrains == 0x0) terrains = 0x1;
		_terrains = terrains;
		PlayerPrefs.SetInt(TERRAINS,(int)terrains);
		PlayerPrefs.Save();
	}
	public byte get_terrains(){return _terrains;}
	public void set_operations(byte operations){
		if(operations == 0x0) operations = 0x1;
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
		PlayerPrefs.SetFloat(MUSICVOLUME,volume);
		PlayerPrefs.Save();
	}
	
	public float get_musicVolume(){return _musicVolume;}
	
	public void set_SFXVolume(float volume){
		_SFXVolume = volume;
		SoundManager.SOUNDS.setVolume(_SFXVolume);
		PlayerPrefs.SetFloat(SFXVOLUME,volume);
		PlayerPrefs.Save();
	}
	
	public float get_SFXVolume(){return _SFXVolume;}
	
	public void set_perceptualVolume(int mode){
			_pVolumeMode = mode;
			PlayerPrefs.SetInt(PERCEPTUALVOLUME,mode);
			PlayerPrefs.Save();
	}
	
	public float get_perceptualVolume(){return _perceptualVolume;}
	public int get_perceptualVolumeMode(){return _pVolumeMode;}
	public void set_mute(bool state){
		_mute = state;
		PlayerPrefs.SetInt(MUTE,(state) ? 1 : 0);
		PlayerPrefs.Save();
	}
	
	public bool get_mute(){return _mute;}
	
}