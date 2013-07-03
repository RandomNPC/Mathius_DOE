using UnityEngine;
using System.Collections;

public class Alian : MonoBehaviour {
	
	public GameObject explode;
	public string equation = "THIS IS A TEST";
	public string variable = "THIS IS A TEST";
	char[] shield;
	int level = 0;
	
	public 
	
	bool scene = false;
	int oscilate = 10;
	int oscPlace = 0;
	
	void generateEquation () {
		int answer = (int) Mathf.Floor(Random.Range(0, 100));
		variable = "" + answer;
		
		string eqn = "x";
		
		int multiple = (int) Mathf.Floor(Random.Range(1, 13));
		if (multiple != 1)
		{
			eqn = multiple + eqn;
			answer = answer * multiple;
		}
		
		for (int i = 0; i < 1; i++)
		{
			int addition = (int) Mathf.Floor(Random.Range(-100, 100));
			if (addition > 0)
			{
				eqn = eqn + "+" + addition;
				answer += addition;
			}
			else if (addition < 0)
			{
				eqn = eqn + addition;
				answer += addition;
			}
		}
		
		equation = eqn + " = " + answer;
	}
	
	//On Start
	void Start () {
		generateEquation();
		oscilate = (int) Mathf.Floor(Random.Range(10, 20));
		shield = variable.ToCharArray();
		//shield.Reverse();
	}
	
	//On Update
	void Update () {
		/*transform.Translate(-1, 0, 0);
		if (oscPlace < oscilate)
		{
			transform.Translate(0, 1, 0);
			oscPlace++;
			if (oscPlace > oscilate) oscilate *= -1; 
		}
		else
		{
			transform.Translate(0, -1, 0);
			oscPlace--;
			if (oscPlace < oscilate) oscilate *= -1; 
		}*/
	}
	
	void OnCollisionEnter (Collision data) {
		if (data.gameObject.name == "Player")
		{
			if (data.gameObject.GetComponent<Player>().variable != variable)
			{
				Instantiate(explode, transform.position, transform.rotation);
				GameObject.FindGameObjectWithTag("GUI").GetComponent<Score>().score -= 15;
				Destroy(gameObject);
				return;
			}
			else if (data.gameObject.GetComponent<Player>().variable == variable)
			{
				Destroy(gameObject);
				return;
			}
		}
		if (data.gameObject.name == "NumBullet(Clone)")
		{
			
			if (shield[level] == data.gameObject.GetComponent<NumBullet>().variable)
			{
				level += 1;
				if (level == variable.Length)
				{
					Destroy(gameObject);
					Instantiate(explode, transform.position, transform.rotation);
					GameObject.FindGameObjectWithTag("GUI").GetComponent<Score>().score += (10*level);
					return;
				}
			}
			else if (shield[level] != data.gameObject.GetComponent<NumBullet>().variable)
			{
				GameObject.FindGameObjectWithTag("GUI").GetComponent<Score>().score -= 5;
				level = 0;
				return;
			}
			
			/*if (data.gameObject.GetComponent<NumBullet>().variable == variable)
			{
				Destroy(gameObject);
				Instantiate(explode, transform.position, transform.rotation);
				GameObject.FindGameObjectWithTag("GUI").GetComponent<Score>().score += 10;
				return;
			}
			else if (data.gameObject.GetComponent<NumBullet>().variable != variable)
			{
				GameObject.FindGameObjectWithTag("GUI").GetComponent<Score>().score -= 5;
				return;
			}*/
		}
	}
	
	void OnBecameVisible () {
		scene = true;
	}
	
	void OnBecameInvisible () {
		if (scene == true)
			Destroy(gameObject);
	}
	
	void OnMouseOver () {
		tag = "TargetedShip";
	}
	
	void OnMouseExit () {
		tag = "Ship";
	}
	
	string returnEqn () {
		return equation;	
	}
}