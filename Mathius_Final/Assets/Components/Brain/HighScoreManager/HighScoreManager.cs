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
	
	public void addScore(int myscore){
		hs = new HighScore(myscore,"");
		_scores.Add(hs);
		
		HighScore lowest = null;
		foreach(HighScore score in _scores){
			if(lowest==null) lowest = score;
			else if(score.get_score() < lowest.get_score()) lowest = score;
		}
		_scores.Remove(lowest);
		if(_scores.Contains(hs)){
			_isHighScore = true;
		}
		else _isHighScore = false;
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
		
		sortScores();
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
