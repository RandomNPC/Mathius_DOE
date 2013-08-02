using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour {
	
	private ArrayList terrains;
	public GameObject[] terrain;
	
	public const byte TERRAIN_1 = 0x01;
	public const byte TERRAIN_2 = 0x02;
	public const byte TERRAIN_3 = 0x04;
	public const byte TERRAIN_4 = 0x08;
	public const byte TERRAIN_5 = 0x10;
	public const byte TERRAIN_6 = 0x20;
	public const byte TERRAIN_7 = 0x40;
	public const byte TERRAIN_8 = 0x80;
	
	
	public GameObject[] selected_terrains(byte ops){
		terrains = new ArrayList();
		terrains.Clear ();
		
		if((ops & TERRAIN_1) == TERRAIN_1){
			terrains.Add(terrain[0]);
		}
		if((ops & TERRAIN_2) == TERRAIN_2){
			terrains.Add(terrain[1]);
		}
		if((ops & TERRAIN_3) == TERRAIN_3){
			terrains.Add(terrain[2]);
		}
		if((ops & TERRAIN_4) == TERRAIN_4){
			terrains.Add(terrain[3]);
		}
		if((ops & TERRAIN_5) == TERRAIN_5){
			terrains.Add(terrain[4]);
		}
		if((ops & TERRAIN_6) == TERRAIN_6){
			terrains.Add(terrain[5]);
		}
		if((ops & TERRAIN_7) == TERRAIN_7){
			terrains.Add(terrain[6]);
		}
		if((ops & TERRAIN_8) == TERRAIN_8){
			terrains.Add(terrain[7]);
		}
		return terrains.ToArray() as GameObject[];
	}
}
