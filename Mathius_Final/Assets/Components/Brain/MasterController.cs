using UnityEngine;
using System.Collections;

public class MasterController : MonoBehaviour {
	
	public GameObject _mathius;
	public GameObject _alien;
	public Texture[] _mathiusTextures;
	public Material[] _skyBoxes;
	
	public GameObject end;
	
	private PreferencesManager pHelper;
	private Mathius mHelper;
	private TileManager tHelper;
	private ScoreManager sHelper;
	private HighScoreManager hHelper;
	private HighScoreInitials iHelper;
	private Alien aHelper;
	private PCInterface pcHelper;
	//private Selector<Texture> stHelper;
	private SkyBoxManager sbHelper;
	
	public static MasterController BRAIN;
	
	void Awake() {//This is the start of the Game. Period.
		DontDestroyOnLoad(gameObject);
		Application.LoadLevel("MainMenu");
		BRAIN = gameObject.GetComponent<MasterController>();
		sbHelper = new SkyBoxManager(_skyBoxes[0],_skyBoxes[1]);
		pHelper = new PreferencesManager();
		mHelper = new Mathius(_mathius);
		tHelper = new TileManager(pHelper);
		sHelper = new ScoreManager(mHelper,pHelper);
		hHelper = new HighScoreManager();
		aHelper = new Alien(_alien);
		pcHelper = new PCInterface();
		pcHelper.set_using_PCI(false);
		iHelper = new HighScoreInitials(3);
		//stHelper = new Selector<Texture>((Texture[])_mathiusTextures);
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
		
		tHelper.setTerrains(TerrainManager.MAPS);
		mHelper.spawn_mathius(0.0f,15.0f,150.0f);
		mHelper.set_lives(1);
		mHelper.set_answer();
		sHelper.reset_score();
		sHelper.set_streakCriteria(3);
		tHelper.set_pos(0.0f);	
		hHelper.loadScores();
		GamePause.PAUSE.set_gameEnd(false);
	}
	
	public void onAlienShot(char val, GameObject alien){
		if(char.GetNumericValue(val).Equals(alien.GetComponent<AlienManager>().answer)){
			GameObject land_ref = alien.transform.parent.gameObject;
			Vector3 alien_pos = alien.transform.position;
			EquationGenerator.EquationOperation op = alien.GetComponent<AlienManager>().operation;
			Destroy(alien);	
			gameObject.GetComponent<ItemDropManager>().drop_item(alien_pos,land_ref);
			sHelper.onCorrectAnswer(op);
		}
		else{
			sHelper.onWrongAnswer();
		}
	}
	
	public void onReachedTargetTile(){ //we reached the last tile (TileManager.cs)
		
	}
	
	public void onReachProblemsSolved(){ //completed all the problems required to clear level (ScoreManager.cs)

		GameObject[] worlds = GameObject.FindGameObjectsWithTag("World");
		foreach(GameObject world in worlds){world.GetComponent<SpawnAlien>().enabled = false;}
		
		GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alian");
		foreach(GameObject alien in aliens){Destroy(alien);}
		
		GameObject cam = GameObject.Find("MathiusEarthCam");
		cam.GetComponent<MoveForward>().enabled = false;
		Instantiate(end,new Vector3(cam.transform.position.x+10.0f,
									cam.transform.position.y,
									150.0f),
									Quaternion.identity);
		GamePause.PAUSE.set_gameEnd(true);
	}
	
	public void onMathiusCollision(Collider data){
		if(data.gameObject.name.Contains("Alian")){
			if(mHelper.get_answer().Equals(data.gameObject.GetComponent<AlienManager>().answer)){
				GameObject alien_ref = data.gameObject;
				GameObject land_ref = alien_ref.transform.parent.gameObject;
				Vector3 alien_pos = alien_ref.transform.position;
				Destroy(data.gameObject);
				gameObject.GetComponent<ItemDropManager>().drop_item(alien_pos,land_ref);
				mHelper.set_answer();
				mHelper.set_lives(mHelper.get_lives()+1);
			}
			else{
				data.gameObject.GetComponent<BoxCollider>().isTrigger = false;
				GameObject.Find ("Mathius").GetComponent<Player>().crashTrajectory_mathius();
			}
		}
		else{
			if(!data.gameObject.name.Contains("Surface")) return;
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
	public HighScoreInitials hsi(){return iHelper;}
	//public Selector<Texture> sT(){return stHelper;}
	public SkyBoxManager sbm(){return sbHelper;}
}

