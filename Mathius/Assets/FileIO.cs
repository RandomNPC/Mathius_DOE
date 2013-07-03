using UnityEngine;
using System.Collections;
using System.IO;

public class FileIO : MonoBehaviour {
	
	private StreamWriter filewriter;
	private StreamReader filereader;
	private ArrayList content;
	private static string FILENAME;
	private Mathius_UI ourScore;
	
	void Start(){
		FILENAME = Application.persistentDataPath + "/highscores.txt";
		filewriter = null;
		filereader = null;
		content = new ArrayList();
		load ();
		ourScore = GameObject.Find("G1").GetComponent("Mathius_UI") as Mathius_UI;
	}
	
	void load(){
		content.Clear();
		filereader = new StreamReader(FILENAME);
	    string line;
       
        while ((line = filereader.ReadLine()) != null)
        {
            content.Add(line);
        }
		
		filereader.Close();
		filereader = null;
	}
	
	public void add(int score, string name){
		content.Add(score+" "+name);
	}
	
	public string[] highscores(){
		return (string[]) content.ToArray(typeof(string));
	}
	
	public void save(){
		int score =  ourScore.totalScore;
		add(score,"MAX");
		filewriter = new StreamWriter(FILENAME);
		content.Sort();
		content.Reverse();
		foreach(string item in content) filewriter.WriteLine(item);
		filewriter.Close();
		filewriter = null;
	}
/*	
	public class hsTable{
		private int _score;
		private string _name;
		
		public hsTable(int s, string n){
			_score = s;
			_name = n;
		}
		
		public int score(){return score;}
		public string name(){return name;}
		
		
	};
*/
}