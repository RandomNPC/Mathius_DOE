using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//left to right = move pos++
//right to left = move pos--

//up = move ascii wheel prev
//down = move ascii wheel next

public class HighScoreInitials{
	
	private List<AsciiWheel> _slots;	
	private AsciiWheel _ref;
	private int _pos;
	
	public EventHandler onSucess_SwipedLeftToRight;
	public EventHandler onSucess_SwipedRightToLeft;
	
	public HighScoreInitials(int slots){
		if(slots <= 0) throw new IndexOutOfRangeException();
		_slots = new List<AsciiWheel>();
		for(int z = 0; z < slots; z++){
			AsciiWheel newWheel = new AsciiWheel();
			_slots.Add(newWheel);
		}
		_pos = 0;
		_ref = _slots[0];
	}
	
	public void onSwipeLeftToRight(){
		if(_pos < (_slots.Count-1)){
			_pos++;
			onSucess_SwipedLeftToRight(this,new EventArgs());
		}
		_ref = _slots[_pos];
	}
	public void onSwipeRightToLeft(){
		if(_pos > 0){
			_pos--;
			onSucess_SwipedRightToLeft(this,new EventArgs());
		}
		_ref = _slots[_pos];
	}
	public void onSwipeUp(){
		_ref.prev();
	}
	public void onSwipeDown(){
		_ref.next();
	}
	
	public void resetWheelPositions(){
		foreach(AsciiWheel val in _slots){
			val.reset_val();
		}
	}
	
	public char[] initialsArray(){
		string temp = "";
		foreach(AsciiWheel name in _slots){
			temp += name.get_val().ToString();
			}
		return temp.ToCharArray();
	}
	public string initials(){
		string temp = "";
		foreach(AsciiWheel name in _slots){
			temp += name.get_val().ToString();
			}
		return temp;
	}
}
