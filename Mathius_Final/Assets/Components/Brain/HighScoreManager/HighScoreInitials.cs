using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//left to right = move pos++
//right to left = move pos--

//up = move ascii wheel prev
//down = move ascii wheel next

public enum SwipeDirection{
	Left,
	Right,
	Up,
	Down
}

public class Swipe : EventArgs{
	
	public SwipeDirection swipe{get; private set;}
	public Swipe(SwipeDirection direction){
		swipe = direction;
	}
}

public class HighScoreInitials{
	
	private List<AsciiWheel> _slots;	
	private AsciiWheel _ref;
	private int _pos;
	
	public event EventHandler<Swipe> onSuccessSwipe;
	
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
	
	public void swipe(SwipeDirection direction){
		switch(direction){
			case SwipeDirection.Down:
				_ref.next();
				onSuccessSwipe(this,new Swipe(SwipeDirection.Down));
				break;
			case SwipeDirection.Right:
				if(_pos < (_slots.Count-1)){
					_pos++;
					onSuccessSwipe(this,new Swipe(SwipeDirection.Right));
				}
				_ref = _slots[_pos];
				break;
			case SwipeDirection.Left:
				if(_pos > 0){
					_pos--;
					onSuccessSwipe(this,new Swipe(SwipeDirection.Left));
				}
				_ref = _slots[_pos];
				break;
			case SwipeDirection.Up:
				_ref.prev();
				onSuccessSwipe(this,new Swipe(SwipeDirection.Up));
				break;
		}
		
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
