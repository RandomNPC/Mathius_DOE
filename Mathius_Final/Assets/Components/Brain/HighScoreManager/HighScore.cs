using UnityEngine;
using System.Collections;

public class HighScore{
	
	private int _score;
	private string _name;
	
	public HighScore(int score, string name){
		_score = score;
		_name = name;
	}
	
	public void set_score(int score){_score=score;}
	public int get_score(){return _score;}
	public void set_name(string name){_name = name;}
	public string get_name(){return _name;}
}
