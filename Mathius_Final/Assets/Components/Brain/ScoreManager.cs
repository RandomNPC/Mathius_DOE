using UnityEngine;
using System.Collections;

public class ScoreManager{
	
	private int _wrong;
	private int _correct;
	private int _streak;
	private int _streakCriteria;
	private int _streak_build;
	private string _equation;
	private bool _isOnStreak;
	private Mathius _mathius;
	
	private const int CORRECT_ANSWER = 100;
	private const int WRONG_ANSWER = 10;
	
	
	public ScoreManager(Mathius instance){
		_wrong = 0;
		_correct = 0;
		_streak = 0;
		_streakCriteria = 0;
		_streak_build = 0;
		_isOnStreak = false;
		_equation = "";
		_mathius = instance;
	}
	
	public void reset_score(){
		_wrong = 0;
		_correct = 0;
		_streak = 0;
		_streakCriteria = 0;
		_streak_build = 0;
		_equation = "";
		_isOnStreak = false;
	}
	
	public void set_streakCriteria(int criteria){
		_streakCriteria = criteria;
	}
	
	public void onCorrectAnswer(){
		_correct++;
		_streak_build++;
		if(_streak_build >=_streakCriteria && !_isOnStreak){ 
			_isOnStreak = true;
			return;
		}
		if(_isOnStreak){
			_streak++;
		}
	}
	
	public void onWrongAnswer(){
		_wrong++;
		_streak_build = 0;
		_isOnStreak = false;
		_streak = 0;
	}
	
	public void set_equation(string equation){_equation = equation;}
	public string get_equation(){return _equation;}
	public int get_score(){return (CORRECT_ANSWER*_correct - WRONG_ANSWER*_wrong);}
	public int get_answer(){return _mathius.get_answer();}
	public int get_lives(){return _mathius.get_lives();}
	public int get_streak(){return _streak;}
}
