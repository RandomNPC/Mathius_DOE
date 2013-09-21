using UnityEngine;
using System.Collections;

public class AlienManager : MonoBehaviour {
	
	private PreferencesManager pfm;
	public int answer;
	public string equation;
	public EquationGenerator.EquationOperation operation;
	
	void Start () {
		pfm = MasterController.BRAIN.pm();
		EquationGenerator eq = new EquationGenerator(pfm.get_operations(),pfm.get_eqFormat());
		equation = eq.Equation();
		answer = eq.answer();
		operation = eq.eqOperation();
	}
	
	void OnDestroy(){
		MasterController.BRAIN.GetComponent<ItemDropManager>().drop_item(transform.position,gameObject.transform.parent.gameObject);
	}
}
