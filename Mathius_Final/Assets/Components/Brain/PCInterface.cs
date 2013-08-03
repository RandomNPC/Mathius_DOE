using System;
using UnityEngine;
using System.Collections;

public class PCInterface{
	
	private byte _direction;
	private bool _using_PCI;
	private string _bullet_val;
	private bool _fire_bullet;
	
	public PCInterface(){
		_direction = Player.MATHIUS_NO_MOVE;
		_using_PCI = false;
		_bullet_val = "";
		_fire_bullet = false;
	}
	
	public void set_direction(byte direction){_direction = direction;}
	public byte get_direction(){return _direction;}
	public void set_using_PCI(bool state){_using_PCI = state;}
	public bool get_using_PCI(){return _using_PCI;}
	public void set_bullet_to_fire(string bulletNum){
			_bullet_val = bulletNum;
			_fire_bullet = true;
	}
	public string get_bullet_to_fire(){
		_fire_bullet = false;
		return _bullet_val;
	}
	public bool get_fire_bullet(){return _fire_bullet;}
	
}
