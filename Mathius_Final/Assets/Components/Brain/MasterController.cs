using UnityEngine;
using System.Collections;

public class MasterController : MonoBehaviour {
	
	public GameObject[] maps;
	public GameObject mathius;

	private Mathius ship;
	private TileManager tile;
	private Scoreboard score;
	
	void Awake() {
		DontDestroyOnLoad(gameObject);
		ship = new Mathius(mathius);
		score = new Scoreboard();
		tile = new TileManager(maps);
	}
	
	public void onGameEnd(){
		print ("the End");
	}
	
	public void onMathiusCollision(Collision data){//Player.cs
		print ("Mathius collided with " + data.gameObject.name);
		if(data.gameObject.name.Contains("Alian")){
			if(ship.answer().Equals(data.gameObject.GetComponent<AlienManager>().answer)){
				Destroy(data.gameObject);
				ship.set_answer();
				score.set_mathius_properties(ship.answer(),ship.get_lives());
			}
			else{ 
				GameObject.Find ("Mathius").GetComponent<Player>().crashTrajectory_mathius();
			}
		}
		else{
			GameObject.Find("Mathius").GetComponent<Player>().destroy_mathius();
			ship.set_lives(ship.get_lives()-1);
			ship.spawn_Mathius(Camera.main.transform.position.x,
							   Camera.main.transform.position.y,
				               Camera.main.transform.position.z+100.0f);
			score.set_mathius_properties(ship.answer(),ship.get_lives());
		}
	}
	
	public void onAlienShot(char val,GameObject alien){//NumBullet.cs
		if(char.GetNumericValue(val).Equals(alien.GetComponent<AlienManager>().answer)){
			Destroy(alien);	
			score.onCorrectAnswer();
		}
		else{
			score.onWrongAnswer();
		}
	}
	
	public void onTriggerNewTerrain(){//CameraCollider.cs
		print("New Terrain Triggered");
		tile.newTerrain();
	}
	
	public void onGameStart(){//CameraCollider.cs
		ship.set_lives(5);
		ship.spawn_Mathius(0.0f,15.0f,344.0f);
		score.set_mathius_properties(ship.answer(),ship.get_lives());
		score.setStreakCriteria(3);
	}
	
	public Scoreboard mc_scoreboard(){return score;}
	
	public void setBounds(Vector3 delta){
		ship.set_bounds(delta);
	}
	
	public Vector3 getBounds(){
		return ship.get_bounds();
	}
	
	private int mathius_lives(){return ship.get_lives();}
	
	private int mathius_answer(){return ship.answer();}
	
	private class Mathius : Object{
		
		private GameObject _mathius;
		private int _lives;
		private int _answer;
		private Vector3 _log_pos;
		private Vector3 _bounds;
		
		public Mathius(GameObject mathius){
			_lives = 0;
			_mathius = mathius;
			_answer = Random.Range(0,9);
			_log_pos = new Vector3(0.0f,0.0f,0.0f);
			_bounds = new Vector3(0.0f,0.0f,0.0f);
		}
		
		public void set_pos(Vector3 pos){_log_pos = pos;}
		public Vector3 last_pos(){return _log_pos;}
		public void set_answer(){_answer = Random.Range(0,9);}
		public int answer(){return _answer;}
		public void set_bounds(Vector3 bounds){_bounds = bounds;}
		public Vector3 get_bounds(){return _bounds;}
		public void set_lives(int lives){
			if(lives>=0){ 
				_lives = lives;
			}
			else{
				GameObject.Find ("Brain").GetComponent<MasterController>().onGameEnd();		
			}
		}
		public int get_lives(){return _lives;}
		public void spawn_Mathius(float x, float y, float z){
			GameObject m = (GameObject)Instantiate(_mathius,new Vector3(x,y,z),Quaternion.identity);
			m.name = "Mathius";
		}
	}
	
	private class TileManager : Object{
		
		private GameObject _prev;
		private GameObject _current;
		private float _pos;
		private int _tile;
		private GameObject _map;
		private GameObject[] _maps;
		
		public TileManager(GameObject[] mapList){
			_prev = null;
			_current = null;
			_pos = 0.0f;
			_tile = 0;
			_map = null;
			_maps = mapList;
		}
		
		public void newTerrain(){
			if(_map==null) _map = _maps[Random.Range(0,_maps.Length-1)];
			
			GameObject land = (GameObject)Instantiate(_map,
													  new Vector3(_pos,0.0f,0.0f),
													  Quaternion.identity);
			land.name = "Surface " + _tile++;
			land.AddComponent<LandManager>();
			land.AddComponent<SpawnAlien>();
			_pos += land.GetComponent<Terrain>().terrainData.size.x/2;
			_map = _maps[Random.Range(0,_maps.Length-1)];
			_pos +=_map.GetComponent<Terrain>().terrainData.size.x/2;
			
			Destroy(_prev);
			_prev = _current;
			if(_prev!=null) _prev.GetComponent<SpawnAlien>().setSpawn(false);
			_current = land;
		}		
	}

	public class Scoreboard{	
		
		private int _correct;
		private int _wrong;
		private int _mathius_lives;
		private int _streak;
		private bool _onStreak;
		private int _streak_criteria;
		private int _mathius_answer;
		
		public Scoreboard(){
			_mathius_lives = 0;
			_mathius_answer = 0;
			_wrong = 0;
			_correct = 0;
			_streak = 0;
			_onStreak = false;
			_streak_criteria = 0;
		}
		
		public void setStreakCriteria(int start){
			_streak_criteria = start;
		}
		
		public void onCorrectAnswer(){
			_correct++;
			if(_correct>=_streak_criteria){ 
				_onStreak = true;
			}
			if(_correct>_streak_criteria){
				_streak++;
			}
		}
		
		public void set_mathius_properties(int answer,int lives){
			_mathius_answer = answer;
			_mathius_lives = lives;
		}
		
		public void onWrongAnswer(){
			_wrong++;
			_onStreak = false;
			_streak = 0;
		}
		
		public int correct(){return _correct;}
		public int wrong(){return _wrong;}
		public int streak(){return _streak;}
		public int mathius_answer(){return _mathius_answer;}
		public int mathius_lives(){return _mathius_lives;}		
	}
}
