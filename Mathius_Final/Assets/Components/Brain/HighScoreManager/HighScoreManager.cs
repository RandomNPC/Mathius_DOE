using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighScoreManager{
	
	private const int MAX_ENTRIES = 10;
	private List<HighScore> _scores;
	private bool _isHighScore;
	private HighScore hs;
	private int _pos;
	
	public HighScoreManager(){
		_scores = new List<HighScore>();
		_isHighScore = false;
		hs = null;
		_pos = -1;
	}
	
	public void loadScores(){
		
		int k = 0;
		
		while(true){
			if(!PlayerPrefs.HasKey("Player " + k)) break;
			HighScore hs = new HighScore(PlayerPrefs.GetInt("Player " + k,0),
										 PlayerPrefs.GetString("Player " + k,"MAB")
										);
			_scores.Add(hs);
			k++;
		}
	}
	
	public void addScore(int score){
		hs = new HighScore(score,"");
		_scores.Add(hs);
		sortScores();
		
		try{
			_pos = _scores.IndexOf(hs);
		}catch{
			_pos = _scores.Count;
		}
		
		if(_pos> (_scores.Count-1)){_scores.Remove(hs);}
		else{
			_isHighScore = true;
		}
	}
	
	private void sortScores(){
		List<HighScore> temp = new List<HighScore>();
		while(_scores.Count>0){
			HighScore highest = null;
			foreach(HighScore hse in _scores){
				if(highest==null) highest = hse;
				else{
					if(highest.get_score() >= hse.get_score()){highest = hse;}
				}
			}
			temp.Add(highest);
			_scores.Remove(highest);
		}
		_scores = temp;
		temp = null;
	}
	
	public void saveScores(){
		for(int k = 0; k < Mathf.Min(_scores.Count,MAX_ENTRIES); k++){
			HighScore hse = _scores[k];
			
			MonoBehaviour.print(k);
			MonoBehaviour.print(_scores[k].get_name());
			MonoBehaviour.print(_scores[k].get_score());
			PlayerPrefs.SetInt("Player "+k,hse.get_score());
			PlayerPrefs.SetString("Player "+k,hse.get_name());
			PlayerPrefs.Save();
		}
	}
	
	public void set_isHighScore(bool state){_isHighScore = state;}
	public bool get_isHighScore(){return _isHighScore;}
	public HighScore hS(){return hs;}
		

}
