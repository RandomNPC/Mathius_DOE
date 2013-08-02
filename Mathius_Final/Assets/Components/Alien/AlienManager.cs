using UnityEngine;
using System.Collections;

public class AlienManager : MonoBehaviour {
	
	private PreferencesManager pfm;
	public TextMesh equation;
	public int answer;
	
	void Start () {
		pfm = MasterController.BRAIN.pm();
		equation = gameObject.GetComponentInChildren<TextMesh>();
		EquationGenerator eq = new EquationGenerator(pfm.get_operations(),pfm.get_eqFormat());
		equation.text = eq.Equation();
		answer = eq.answer();
	}

}
