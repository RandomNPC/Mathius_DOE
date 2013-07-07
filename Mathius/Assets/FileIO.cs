using UnityEngine;
using System.Collections;
using System.IO;

public class FileIO : MonoBehaviour {
	
	private static int TOP_PLAYER_COUNT;
	private ArrayList content;
	private ArrayList highscore;
	private static string FILENAME;
	private int _gameOver_score;
	
	private Mathius_UI ourScore;

	
	void Start(){
		TOP_PLAYER_COUNT = 10;
		content = new ArrayList();
		highscore = new ArrayList(TOP_PLAYER_COUNT);
		FILENAME = Application.persistentDataPath + "/highscores.txt";
		_gameOver_score = 0;

		ourScore = GameObject.Find("G1").GetComponent("Mathius_UI") as Mathius_UI;
		load ();
	}
	
	private void load(){
		content.Clear();
		highscore.Clear();
		
		if(!File.Exists(FILENAME)){
			using (StreamWriter sw = File.CreateText(FILENAME)){}
		}
		
		using(StreamReader sr = File.OpenText(FILENAME)){
			string text = "";
			while((text = sr.ReadLine())!=null){
					string name = text.Substring(text.IndexOf(' ')+1);
					PlayerScore ps = new PlayerScore(int.Parse(text.Substring(0,text.IndexOf(' '))),name);
					content.Add(ps);
					if(highscore.Count<=TOP_PLAYER_COUNT){
						highscore.Add(ps);
					}
					if(name.Contains("*")){
						_gameOver_score = ps.score();
					}
			}
		}
		
	}
	
	public int gameOver_score(){return _gameOver_score;}
	
	//does the highscore list contain *?
	public bool contains_highscore(){
		foreach(PlayerScore k in highscore){
			if(k.player().Contains("*")){
				return true;
			}
		}
		return false;
	}
	
	//returns the player score from the highscore list at position
	public int player_score(int pos){
		if(pos>highscore.Count || pos < 0) return -1;
		else{
			PlayerScore ps = highscore[pos] as PlayerScore;
			return ps.score();
		}
	}
	
	//returns the player name from the highscore list at position
	public string player_name(int pos){
		if(pos>highscore.Count || pos < 0) return null;
		else{
			PlayerScore ps = highscore[pos] as PlayerScore;
			return ps.player();	
		}
	}
	
	//finds the score with playername * to replace
	public void set_playername(string name){ //will look for player with * and set the name
		foreach(PlayerScore s in highscore){
			if(s.player().Contains("*")){
				s.set_playername(name);
				break;
			}
		}
	}
	
	//inserts the score from the game
	public void insert(){
		PlayerScore p = new PlayerScore(ourScore.totalScore,"*");		
		content.Add (p);
	}
	
	//saves the scores
	public void save(){
		
		ArrayList temp = new ArrayList(content.Count);
		while(content.Count>0){
			PlayerScore highest = null;
			
			foreach(PlayerScore ps in content){
				if(highest==null) highest = ps;
				else{
					if(highest.score()<ps.score()) highest = ps;	
				}
			}
			content.Remove(highest);
			temp.Add(highest);
		}
		content = temp;
		
		using(StreamWriter sw = File.CreateText(FILENAME)){
			foreach(PlayerScore c in content){
				sw.WriteLine(c.score() + " " + c.player());
			}
		}
	}
	
	private class PlayerScore{
		private int _score;
		private string _player;
		
		public PlayerScore(int s, string p){
			_score = s;
			_player = p;
		}
		
		public int score(){return _score;}
		public string player(){return _player;}
		public void set_playername(string pname){_player = pname;}
	};

}