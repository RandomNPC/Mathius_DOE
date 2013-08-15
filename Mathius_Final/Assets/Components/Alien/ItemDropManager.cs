using UnityEngine;
using System.Collections;

public class ItemDropManager : MonoBehaviour {

	public GameObject[] _items;
	
	public void drop_item(Vector3 position, GameObject parent){
		if(_items.Length<=0) return;
		float total = 0.0f;
		foreach(GameObject item in _items){
			total += item.GetComponent<PowerUpManager>().dropChance;
		}
		
		float random = Random.value * total;
		foreach(GameObject item in _items){
			float chance = item.GetComponent<PowerUpManager>().dropChance;
			if(random < chance){
				GameObject _item = (GameObject)Instantiate(item,
												   new Vector3(position.x,position.y,position.z),
			 									   Quaternion.identity);
				_item.name = "Powerup";
				_item.transform.parent = parent.transform;
				return;
			} else{
				random -= chance;
			}
		}
	}	
}
