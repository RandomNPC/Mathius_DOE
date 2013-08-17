using System;
using UnityEngine;
using System.Collections;

public class PCInterface{
		
	private bool _using_PCI;
	public event EventHandler<PCVoice> onNumberVoiceCommand;
	public event EventHandler<PCGesture> onGesturePerformed;
	public event EventHandler<PCHand> onHandPositionChanged;
	
	private PerCGesture _gesture;
	private PerCVoice _voice;
	
	private float[] _handpos;
	
	public PCInterface(GameObject instance){
		_using_PCI = false;
		_gesture = instance.GetComponent<PerCGesture>();
		_voice = instance.GetComponent<PerCVoice>();
		_handpos = new float[2]{0,0};
	}
	
	public void PollPCUpdates(){
		if(_gesture.swipedDown())onGesturePerformed(this,new PCGesture(Gesture.DOWN));
		if(_gesture.swipedUp())onGesturePerformed(this,new PCGesture(Gesture.UP));
		if(_gesture.swipedRight())onGesturePerformed(this,new PCGesture(Gesture.RIGHT));
		if(_gesture.swipedLeft())onGesturePerformed(this,new PCGesture(Gesture.LEFT));
		
		float[] polled_handpos = _gesture.getHandLocation();
		if(!polled_handpos[0].Equals(_handpos[0]) || !polled_handpos[1].Equals(_handpos[1])){
			onHandPositionChanged(this,new PCHand(polled_handpos[0],polled_handpos[1]));
			_handpos = polled_handpos;
		}
		
		int voicedNumber = _voice.getNumberVoiced();
		if(voicedNumber > -1) onNumberVoiceCommand(this,new PCVoice(voicedNumber));
		
		switch(_voice.getOptionVoicedAsString()){
			case "zero":
				onNumberVoiceCommand(this,new PCVoice(0));
				break;
			case "one":
				onNumberVoiceCommand(this,new PCVoice(1));
				break;
			case "two":
				onNumberVoiceCommand(this,new PCVoice(2));
				break;
			case "three":
				onNumberVoiceCommand(this,new PCVoice(3));
				break;
			case "four":
				onNumberVoiceCommand(this,new PCVoice(4));
				break;
			case "five":
				onNumberVoiceCommand(this,new PCVoice(5));
				break;
			case "six":
				onNumberVoiceCommand(this,new PCVoice(6));
				break;
			case "seven":
				onNumberVoiceCommand(this,new PCVoice(7));
				break;
			case "ate":
				onNumberVoiceCommand(this,new PCVoice(8));
				break;
			case "nine":
				onNumberVoiceCommand(this,new PCVoice(9));
				break;
			case "left":
				onGesturePerformed(this,new PCGesture(Gesture.LEFT));
				break;
			case "right":
				onGesturePerformed(this,new PCGesture(Gesture.RIGHT));
				break;
			case "up":
				onGesturePerformed(this,new PCGesture(Gesture.UP));
				break;
			case "down":
				onGesturePerformed(this,new PCGesture(Gesture.DOWN));
				break;
			case "select":
				onGesturePerformed(this,new PCGesture(Gesture.SELECT));
				break;
			case "cancel":
				onGesturePerformed(this,new PCGesture(Gesture.CANCEL));
				break;
			case "pause":
				onGesturePerformed(this,new PCGesture(Gesture.PAUSE));
				break;
			case "do a barrel roll":
				onGesturePerformed(this,new PCGesture(Gesture.DO_A_BARREL_ROLL));
				break;
			default:
				break;
		}
	}
	
	public void set_using_PCI(bool state){_using_PCI = state;}
	public bool get_using_PCI(){return _using_PCI;}
	public int[] pc_resolution(){return _gesture.getResolution();}
}
