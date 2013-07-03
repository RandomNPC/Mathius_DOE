using UnityEngine;
using System.Collections;

public class PaulScore : MonoBehaviour {
	
	public int lives;
	public int mathius_variable;
	public int num_correct;
	public int num_wrong;
	private MoveCamera moveCamera;
	public int streak_score;
	private int correct;
	private bool streak_mode;
	public string equation;
	public AudioClip alianDeath;
	
	// Use this for initialization
	void Start () {
		correct = 0;
		streak_mode = false;
		streak_score = 0;
		lives = 5;
		mathius_variable = (int)Mathf.Floor(Random.Range(0, 9));
		num_correct = 0;
		num_wrong = 0;
		equation = "";
		moveCamera = GameObject.Find("MathiusEarthCam").GetComponent("MoveCamera") as MoveCamera;
		alianDeath = Resources.Load("score_01") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
		nearest_alien();
		if(lives<= 0){
			FileIO newFile = gameObject.GetComponent("FileIO") as FileIO;
			newFile.save();
			Application.LoadLevel("GameOver");
		}
	}
	
	public void nearest_alien(){
		GameObject[] gos;
		GameObject closest = null;
        gos = GameObject.FindGameObjectsWithTag("Alian");
		if(gos.Length<=0) return ;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
		PaulAlien pa = closest.GetComponent("PaulAlien") as PaulAlien;
		equation = pa.equation.text;
	}
	
	public void correct_answer(){
		print(alianDeath);
		num_correct++;
		correct++;
		audio.PlayOneShot(alianDeath ,0.7F);
		if(correct>= 3){
			streak_mode = true;
		}
		if (streak_mode){
			streak_score++;
		}
	}
	public void wrong_answer(){
		num_wrong++;
		correct = 0;
		streak_mode =false;
		streak_score = 0;
	}
	
	public void mathius_crashes(int alien_num,GameObject alien){
		if(alien_num.Equals(mathius_variable)){ 
			Destroy(alien);
			lives++;
			mathius_variable = (int)Mathf.Floor(Random.Range(0, 9));
		}
		else{ 
			moveCamera.destroy_mathius();
			lives--;
		}			
	}
}
