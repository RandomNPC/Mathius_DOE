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
	private int _problems_to_clear;
	private int _bonus_points;
	private Mathius _mathius;
	
	private const int CORRECT_ANSWER = 100;
	private const int WRONG_ANSWER = 10;
	private const int ADDITION_BONUS = 100;
	private const int SUBTRACTION_BONUS = 200;
	private const int MULTIPLICATION_BONUS = 300;
	private const int DIVISION_BONUS = 400;
	
	public ScoreManager(Mathius instance){
		_wrong = 0;
		_correct = 0;
		_streak = 0;
		_streakCriteria = 0;
		_streak_build = 0;
		_isOnStreak = false;
		_equation = "";
		_mathius = instance;
		_problems_to_clear = PlayerPrefs.GetInt("win_number",25);
		_bonus_points = 0;
	}
	
	public void reset_score(){
		_wrong = 0;
		_correct = 0;
		_streak = 0;
		_streakCriteria = 0;
		_streak_build = 0;
		_equation = "";
		_isOnStreak = false;
		_bonus_points = 0;
	}
	
	public void set_streakCriteria(int criteria){
		_streakCriteria = criteria;
	}
	
	public void onCorrectAnswer(EquationGenerator.EquationOperation operation){
		
		switch(operation){
			case EquationGenerator.EquationOperation.ADDITION:
				_bonus_points += ADDITION_BONUS;
				break;
			case EquationGenerator.EquationOperation.SUBTRACTION:	
				_bonus_points += SUBTRACTION_BONUS;
				break;
			case EquationGenerator.EquationOperation.MULTIPLICATION:
				_bonus_points += MULTIPLICATION_BONUS;
				break;
			case EquationGenerator.EquationOperation.DIVISION:
				_bonus_points += DIVISION_BONUS;
				break;
			default:
				break;
		}
		
		_correct++;
		_problems_to_clear--;
		if(_problems_to_clear <= 0){MasterController.BRAIN.onReachProblemsSolved();}
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
	public int get_score(){return (CORRECT_ANSWER*_correct - WRONG_ANSWER*_wrong + _bonus_points);}
	public int get_answer(){return _mathius.get_answer();}
	public int get_lives(){return _mathius.get_lives();}
	public int get_streak(){return _streak;}
	public void set_problems_remaining(int num){_problems_to_clear = num;}
	public int get_problems_remaining(){return _problems_to_clear;}
}
