using UnityEngine;
using System.Collections;

public class AlienManager : MonoBehaviour {
	
	public TextMesh equation;
	public int answer;
	
	void Start () {
		equation = gameObject.GetComponentInChildren(typeof(TextMesh)) as TextMesh;
		EquationGenerator eq = new EquationGenerator(EquationGenerator.ADDITION,EquationGenerator.MIXED);
		equation.text = eq.Equation();
		answer = eq.answer();
	}

}
