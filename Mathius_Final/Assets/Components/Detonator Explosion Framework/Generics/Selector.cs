using System;
using UnityEngine;
using System.Collections;

public class Selector<T>{
	
	private T[] _list;
	private int _pos;
	
	public event EventHandler<MoveSucess<T>> onMoveSucess;
	
	public Selector(T[] list){
		_list = list;
		_pos = 0;
		if(_list.Length <= _pos) throw new IndexOutOfRangeException();
		onMoveSucess(this,new MoveSucess<T>(_list[_pos]));
	}
	
	public void next(){
		if(_pos < (_list.Length-1)){
			_pos++;
			onMoveSucess(this,new MoveSucess<T>(_list[_pos]));
		}
	}
	
	public void prev(){
		if(_pos > 0){
			_pos--;
			onMoveSucess(this,new MoveSucess<T>(_list[_pos]));
		}
	}
	
}
