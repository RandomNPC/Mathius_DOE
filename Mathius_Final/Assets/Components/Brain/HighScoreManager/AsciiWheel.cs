using UnityEngine;
using System.Collections;

public class AsciiWheel{
	
	int _pos; //domain = 48 - 122
	
	public AsciiWheel(){
		_pos = 48; 
	}
	
	public void next(){ //advance to the next letter
		_pos++;
		while(!char.IsLetterOrDigit((char)_pos)){
			_pos++;
			if(_pos>122){
				_pos = 48;
			}
		}
	}
	
	public void prev(){ //go to the prev letter
		_pos--;
		while(!char.IsLetterOrDigit((char)_pos)){
			_pos--;
			if(_pos<48){
				_pos = 122;
			}
		}
	}
	
	public char get_val(){return (char)_pos;}
	public void reset_val(){_pos=48;}
}
