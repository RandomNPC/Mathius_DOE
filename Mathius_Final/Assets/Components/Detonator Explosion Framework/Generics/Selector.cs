using System;
using UnityEngine;
using System.Collections;

public class Selector<T>{
	
	private T[] _list;
	private int _pos;
	
	public Selector(T[] list){
		_list = list;
		_pos = 0;
		if(_list.Length <= _pos) return;
	}
	
	public void next(){
		if(_pos < (_list.Length-1)){
			_pos++;
		}
	}
	
	public void prev(){
		if(_pos > 0){
			_pos--;
		}
	}
	
	public T selected(){
		return (T)(object)_list[_pos];
	}
}
