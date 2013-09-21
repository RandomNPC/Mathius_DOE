using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour {
	
	private List<GameObject> terrains;
	public GameObject[] terrain;
	
	public const byte TERRAIN_1 = 0x01;
	public const byte TERRAIN_2 = 0x02;
	public const byte TERRAIN_3 = 0x04;
	public const byte TERRAIN_4 = 0x08;
	public const byte TERRAIN_5 = 0x10;
	public const byte TERRAIN_6 = 0x20;
	public const byte TERRAIN_7 = 0x40;
	public const byte TERRAIN_8 = 0x80;
	
	
	public GameObject[] get_terrains(byte _terrains){
		if(terrains == null) terrains = new List<GameObject>();
		terrains.Clear();
		for(int pos = 0; pos < 8; pos++){
			if(((_terrains >> pos) & 0x01) == 0x01){
				if(pos <= (terrain.Length-1)) terrains.Add(terrain[pos]);
			}
		}
		return terrains.ToArray();
	}
}
