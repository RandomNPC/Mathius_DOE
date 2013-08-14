using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SkyBoxes{
	DAY,
	NIGHT
}

public class SkyBoxManager{
	
	private Material _skyBoxDay;
	private Material _skyBoxNight;
	private Material _current;
	private Dictionary<string,SkyBoxes> _skyboxMap;
	
	public SkyBoxManager(Material day, Material night){
		_skyboxMap = new Dictionary<string, SkyBoxes> ();
		_skyboxMap.Clear();
		_skyBoxDay = day;
		_skyBoxNight = night;
		_current = null;
	}
	
	public void mapSkyBox(string map_name){
	
		if(_skyboxMap.ContainsKey(map_name)){
			switch(_skyboxMap[map_name]){
				case SkyBoxes.DAY:
					_skyboxMap[map_name] = SkyBoxes.NIGHT;
					_current = _skyBoxNight;
					break;	
				case SkyBoxes.NIGHT:
					_skyboxMap[map_name] = SkyBoxes.DAY;
					_current = _skyBoxDay;
					break;
				default:
					break;
			}
		}else{
			_skyboxMap.Add(map_name,SkyBoxes.DAY);
			_current = _skyBoxDay;
		}
		GameObject.Find("MathiusEarthCam").GetComponent<Skybox>().material = _current;
		
	}
}
