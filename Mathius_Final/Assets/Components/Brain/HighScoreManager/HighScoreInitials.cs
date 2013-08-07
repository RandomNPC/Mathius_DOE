using System;
using UnityEngine;
using System.Collections;

//left to right = move pos++
//right to left = move pos--

//up = move ascii wheel prev
//down = move ascii wheel next

public class HighScoreInitials{
	
	private AsciiWheel[] _slots;
	private AsciiWheel _ref;
	private int _pos;
	
	public EventHandler onSucess_SwipedLeftToRight;
	public EventHandler onSucess_SwipedRightToLeft;
	
	public HighScoreInitials(int slots){
		if(slots <= 0) throw new IndexOutOfRangeException();
		_slots = new AsciiWheel[slots];
		_ref = _slots[0];
		_pos = 0;
	}
	
	public void onSwipeLeftToRight(){
		if(_pos >= (_slots.Length-1)){
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
		foreach(AsciiWheel val in _slots){
			temp += char.ToString(val.get_val());
		}
		
		return temp.ToCharArray(0,_slots.Length);
	}
}
