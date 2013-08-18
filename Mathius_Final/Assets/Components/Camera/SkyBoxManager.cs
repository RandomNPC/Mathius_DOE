using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SkyBoxes{
	DAY,
	NIGHT
}

public class SkyBoxManager{
	
	private Material _current;
	private Material[] _skyboxes;
	private Dictionary<string,SkyBoxes> _skyboxMap;
	
	public SkyBoxManager(Material[] materials){
		_skyboxMap = new Dictionary<string, SkyBoxes> ();
		_skyboxMap.Clear();
		_skyboxes = materials;
		_current = null;
	}
	
	public void mapSkyBox(string map_name){
	
		if(_skyboxMap.ContainsKey(map_name)){
			switch(_skyboxMap[map_name]){
				case SkyBoxes.DAY:
					_skyboxMap[map_name] = SkyBoxes.NIGHT;
					_current = _skyboxes[1];
					break;	
				case SkyBoxes.NIGHT:
					_skyboxMap[map_name] = SkyBoxes.DAY;
					_current = _skyboxes[0];
					break;
				default:
					break;
			}
		}else{
			_skyboxMap.Add(map_name,SkyBoxes.DAY);
			_current = _skyboxes[1];
		}
		GameObject.Find("MathiusEarthCam").GetComponent<Skybox>().material = _current;
		
	}
}
