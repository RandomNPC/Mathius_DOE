using UnityEngine;
using System.Collections;

public class ItemDropManager : MonoBehaviour {

	public GameObject[] _items;
	private GameObject _spawn;
	
	void Start(){
		_spawn = null;
	}
	
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
				_spawn = item;
			} else{
				random -= chance;
			}
		}
		if(_spawn == null) return; //no gameObject has been dropped.
		
		GameObject _item = (GameObject)Instantiate(_spawn,
												   new Vector3(position.x,position.y,position.y),
			 									   Quaternion.identity);
		_item.name = "Powerup";
		_item.transform.parent = parent.transform;
	}
	
	
}
