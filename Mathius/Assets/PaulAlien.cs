using UnityEngine;
using System.Collections;

public class PaulAlien : MonoBehaviour {
	
	public TextMesh equation;//global varriable
	public int answer;
	public AudioClip alianExplosion;
	public AudioClip alianFail;
	
	private PaulScore ps;//script reference
	
	// Use this for initialization
	void Start () {
		equation = gameObject.GetComponentInChildren(typeof(TextMesh)) as TextMesh;
		equation.renderer.material.color = Color.magenta;
		answer = 0;
		generateEquation();
		ps = GameObject.Find ("MathiusEarthCam").GetComponent("PaulScore") as PaulScore;
		alianExplosion = Resources.Load("score_01") as AudioClip;
		alianFail = Resources.Load("error_01") as AudioClip;
	}
	
	void generateEquation(){
		
		answer = (int)Random.Range(0,9);
		int temp = (int)Random.Range(0,10);
	
		
		switch(Mathf.FloorToInt(Random.Range(0,4))){
			case 0: //addition		
				equation.text = "x + " + temp + " = " + (answer+temp);
				break;
			case 1: //subtract
				if(answer >= temp){
					equation.text = "x - " + temp + " = " + (answer-temp);
				}
				else{
					equation.text = temp + " - x = " + (temp-answer);
				}
				break;
			case 2: //multiplication
				if(temp<1) temp = (int)Random.Range(1,10);
				switch((int)Random.Range(0,2)){
					case 0:
						equation.text = temp + " * x = " + (temp*answer);		
						break;
					case 1:
					case 2:
						equation.text = "x * " + temp + " = " + (temp*answer);
						break;
					default:
						break;
				}
				break;
			case 3: // division
			case 4:
				if(temp==0){
					if(answer==0){//not okay
							answer = (int)Random.Range(1,9);
							temp = (int)Random.Range(1,10);
					}
				}else{
					if(answer==0){ //not okay
							answer = (int)Random.Range(1,9);
							temp = (int)Random.Range(1,10);
					}
				}
				equation.text = (temp*answer) + " / x = " + temp;
				break;
			default:		
				break;
		}
	}
	
	public void alien_shot(char num){
		string num_shot = char.GetNumericValue(num).ToString();
		string number = answer.ToString();
		if(number.Equals(num_shot)){
			ps.correct_answer();
			Destroy(gameObject);
		}else{
			ps.wrong_answer();
			audio.PlayOneShot(alianFail,10000.0f);
		}
	}
}
