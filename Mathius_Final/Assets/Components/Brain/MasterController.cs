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
	private SkyBoxManager sbHelper;
	
	public static MasterController BRAIN;
	public static Vector3 UI_MAIN_MENU;
	public static Vector3 UI_CAMERA_ALT;
	
	
	void Awake() {//This is the start of the Game. Period.
		Application.targetFrameRate = 60;
		DontDestroyOnLoad(gameObject);
		Application.LoadLevel("SplashScreen");
		BRAIN = gameObject.GetComponent<MasterController>();
		pHelper = new PreferencesManager();
		sbHelper = new SkyBoxManager(_skyBoxes);
		pcHelper = new PCInterface(gameObject);
		mHelper = new Mathius(_mathius);
		tHelper = new TileManager(pHelper);
		sHelper = new ScoreManager(mHelper,pHelper);
		hHelper = new HighScoreManager();
		aHelper = new Alien(_alien);
		iHelper = new HighScoreInitials(3);
		UI_MAIN_MENU = new Vector3(0.0f,1.0f,-13.75854f);
		UI_CAMERA_ALT = new Vector3(0.0f,1.0f,-10.0f);
		pcHelper.set_using_PCI(pHelper.get_usePerceptual());
	}
	
	void Update(){
		if(pcHelper.get_using_PCI())pcHelper.PollPCUpdates();
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
		
		SoundManager.SOUNDS.playSound(SoundManager.UI_CLICK,UI_MAIN_MENU);
		tHelper.setTerrains(TerrainManager.MAPS);
		mHelper.spawn_mathius(0.0f,15.0f,150.0f);
		mHelper.set_texture(_mathiusTextures[pHelper.get_mathiusTexture()]);
		mHelper.set_lives(1);
		sHelper.set_streakCriteria(3);
		GamePause.PAUSE.set_gameEnd(false);
		
		mHelper.set_answer();
		sHelper.reset_score();
		tHelper.set_pos(0.0f);	
		hHelper.loadScores();
		aHelper.reset_postions();
	}
	
	public void onAlienShot(int val, GameObject alien){
		if(val.Equals(alien.GetComponent<AlienManager>().answer)){
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
				GameObject shield = GameObject.Find("Mathius-Shield");
				
				if(shield.Equals(null)){
					data.gameObject.GetComponent<BoxCollider>().isTrigger = false;
					GameObject.Find ("Mathius").GetComponent<Player>().crashTrajectory_mathius();
				}else{
					Destroy(shield);
					Destroy(data.gameObject);
				}
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
	public HighScoreInitials hsi(){return iHelper;}
	public SkyBoxManager sbm(){return sbHelper;}
	public PCInterface pci(){return pcHelper;}
}

