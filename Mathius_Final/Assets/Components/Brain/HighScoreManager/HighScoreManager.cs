using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighScoreManager{
	
	private const int MAX_ENTRIES = 10;
	private List<HighScore> _scores;
	private bool _isHighScore;
	private HighScore hs;
	private int _pos;
	private static string[] _players = {"PAT","SAM","TED","SUE","DAN","MAY","SAL","MAI","JOY","ZAC"};
	private static int[] _players_score = {10000,9000,8000,7000,6000,5000,4000,3000,2000,0};
	
	public HighScoreManager(){
		_scores = new List<HighScore>();
		_isHighScore = false;
		hs = null;
		_pos = -1;
	}
	
	public void loadScores(){
			_scores.Clear();
			for(int k = 0; k<MAX_ENTRIES; k++){
				HighScore hs = new HighScore(PlayerPrefs.GetInt("Player H" + k,_players_score[k]),
											 PlayerPrefs.GetString("Player " + k,_players[k])
											);
				_scores.Add(hs);
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
		
		if(_pos>=(_scores.Count-1)){
			_scores.Remove(hs);
			_isHighScore = false;
		}
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
					if(highest.get_score() > hse.get_score()){highest = hse;}
				}
			}
			temp.Add(highest);
			_scores.Remove(highest);

		}
		_scores = temp;
		_scores.Reverse();
		temp = null;
	}
	
	public void saveScores(){
		for(int k = 0; k < MAX_ENTRIES; k++){
			HighScore hse = _scores[k];
			
			PlayerPrefs.SetInt("Player H"+k,hse.get_score());
			PlayerPrefs.Save();
			PlayerPrefs.SetString("Player "+k,hse.get_name());
			PlayerPrefs.Save();
		}
	}
	
	public void set_isHighScore(bool state){_isHighScore = state;}
	public bool get_isHighScore(){return _isHighScore;}
	public HighScore hS(){return hs;}
		

}
