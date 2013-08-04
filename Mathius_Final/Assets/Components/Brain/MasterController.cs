using UnityEngine;
using System.Collections;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 

public class MasterController : MonoBehaviour {
	
	public GameObject _mathius;
	public GameObject _alien;
	public GameObject[] maps;
	
	private PreferencesManager pHelper;
	private Mathius mHelper;
	private TileManager tHelper;
	private ScoreManager sHelper;
	private HighScoreManager hHelper;
	private Alien aHelper;
	private PCInterface pcHelper;
	
	public static MasterController BRAIN;
	
	void Awake() {//This is the start of the Game. Period.
		DontDestroyOnLoad(gameObject);
		Application.LoadLevel("MainMenu");
		BRAIN = gameObject.GetComponent<MasterController>();
		
		pHelper = new PreferencesManager();
		mHelper = new Mathius(_mathius);
		tHelper = new TileManager(maps);
		sHelper = new ScoreManager(mHelper);
		hHelper = new HighScoreManager();
		aHelper = new Alien(_alien);
		pcHelper = new PCInterface();
		pcHelper.set_using_PCI(false);
	}
	
	//Events
	public void onGameEnd(){
		hHelper.addScore(sHelper.get_score());
		Application.LoadLevel("GameOver");
	}
	
	public void onTriggerTransition(){
		tHelper.triggerTransition();
	}
	
	public void onTriggerNewTerrain(){
		tHelper.generateNextTerrain();
	}
	
	public void onGameStart(){
		mHelper.spawn_mathius(0.0f,15.0f,344.0f);
		mHelper.set_lives(5);
		mHelper.set_answer();
		sHelper.set_streakCriteria(3);
		sHelper.reset_score();
		tHelper.set_pos(0.0f);	
		hHelper.loadScores();
	}
	
	public void onAlienShot(char val, GameObject alien){
		if(char.GetNumericValue(val).Equals(alien.GetComponent<AlienManager>().answer)){
			Destroy(alien);	
			sHelper.onCorrectAnswer();
		}
		else{
			sHelper.onWrongAnswer();
		}
	}
	
	public void onMathiusCollision(Collision data){
		if(data.gameObject.name.Contains("Alian")){
			if(mHelper.get_answer().Equals(data.gameObject.GetComponent<AlienManager>().answer)){
				Destroy(data.gameObject);
				mHelper.set_answer();
				mHelper.set_lives(mHelper.get_lives()+1);
			}
			else{ 
				GameObject.Find ("Mathius").GetComponent<Player>().crashTrajectory_mathius();
			}
		}
		else{
			GameObject.Find("Mathius").GetComponent<Player>().destroy_mathius();
			mHelper.set_lives(mHelper.get_lives()-1);
			Vector3 cam = GameObject.Find("MathiusEarthCam").transform.position;
			mHelper.spawn_mathius(cam.x,cam.y,cam.z+100.0f);
		}
	}
	
	//class instances. Use 
	public PreferencesManager pm(){return pHelper;}
	public Mathius m(){return mHelper;}
	public ScoreManager sm(){return sHelper;}
	public TileManager tm(){return tHelper;}
	public HighScoreManager hsm(){return hHelper;}
	public Alien al(){return aHelper;}
	public PCInterface pci(){return pcHelper;}
}

