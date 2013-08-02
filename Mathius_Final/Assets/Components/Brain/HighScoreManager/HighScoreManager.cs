using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighScoreManager{
	
	private const int MAX_ENTRIES = 10;
	private List<HighScore> _scores;
	private bool _isHighScore;
	private HighScore hs;
	
	public HighScoreManager(){
		_scores = new List<HighScore>(MAX_ENTRIES);
		_isHighScore = false;
		hs = null;
	}
	
	public void loadScores(){
		for(int k = 1; k<=MAX_ENTRIES; k++){
			HighScore hs = new HighScore(PlayerPrefs.GetInt("Player " + k,0),
										 PlayerPrefs.GetString("Player " + k,"MAB")
										);
			_scores.Add(hs);
		}
	}
	
	public void addScore(int score){
		hs = new HighScore(score,"");
		_scores.Add(hs);
		sortScores();
		
		int pos = 0;
		try{
			pos = _scores.IndexOf(hs);
		}catch{
			pos = MAX_ENTRIES+1;
		}
		
		if(pos> MAX_ENTRIES){_scores.Remove(hs);}
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
		for(int k = 1; k <= MAX_ENTRIES; k++){
			HighScore hse = _scores[k];
			PlayerPrefs.SetInt("Player "+k,hse.get_score());
			PlayerPrefs.SetString("Player "+k,hse.get_name());
			PlayerPrefs.Save();
		}
	}
	
	public void set_isHighScore(bool state){_isHighScore = state;}
	public bool get_isHighScore(){return _isHighScore;}
	public HighScore hS(){return hs;}
		

}
