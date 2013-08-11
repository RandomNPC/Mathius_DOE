using UnityEngine;
using System.Collections;

public class EquationGenerator{
	
	public enum EquationOperation{
		ADDITION,
		SUBTRACTION,
		MULTIPLICATION,
		DIVISION,
	}
	
	private int solution;
	private string equation;
	private EquationOperation operation;
	
	private int displayMode;
	private ArrayList operations;
	
	public const byte ADDITION = 0x01;
	public const byte SUBTRACTION =0x02;
	public const byte MULTIPLICATION = 0x04;
	public const byte DIVISION = 0x08;
	public const int ARITHMETIC = 0;
	public const int ALGEBRA = 1;
	public const int MIXED = 2;
		
	public EquationGenerator(byte ops,int displayMode){
		operations = new ArrayList();
		
		operations.Clear();
		this.displayMode = displayMode;
		solution = (int)Random.Range(0,9);
		equation = "";
		
		if((ops & ADDITION) == ADDITION){
			operations.Add("+");
		}
		if((ops & SUBTRACTION) == SUBTRACTION){
			operations.Add("-");
		}
		if((ops & MULTIPLICATION) == MULTIPLICATION){
			operations.Add("x");
		}
		if((ops & DIVISION) == DIVISION){
			operations.Add("/");
		}
	}
	
	private void EqFormat(int temp,string op){
		
		switch(displayMode){
			case 0: //arithmetic
				switch(op){
					case "+":
						if((solution - temp)<0){
							temp = Random.Range(0,solution);
						}
						equation = (solution-temp) + " + " + temp + " = ?"; 
						break;
					case "-":
						equation = (temp+solution) + " - " + temp + " = ?";
						break;
					case "x":
						while(true){
							if(solution%temp==0) break;
							temp = Random.Range(1,9);
						}
						equation = (solution/temp) + " * " + temp + " = ?";
						break;
					case "/":
						equation = (temp*solution) + " / " + temp + " = ?";
						break;
					default: //wtf
						break;
				}
				break;
			case 1: //algebra
				switch(op){
					case "+":
						equation = " ?  + " + temp + " = " + (solution+temp);
						break;
					case "-":
						if(solution >= temp){
							equation = "? - " + temp + " = " + (solution-temp);
						}
						else{
							equation = temp + " - ? = " + (temp-solution);
						}
						break;
					case "x":
						if(temp<1) temp = (int)Random.Range(1,9);
						switch((int)Random.Range(0,2)){
							case 0:
								equation = temp + " * ? = " + (temp*solution);		
								break;
							case 1:
							case 2:
								equation = "? * " + temp + " = " + (temp*solution);
								break;
							default:
								break;
						}
						break;
					case "/":
						equation = (temp*solution) + " / ? = " + temp;
						break;
					default: //wtf
						break;
				}
				break;
			case 2: //mixed
				displayMode = Random.Range(0,2);
				EqFormat(temp,op);
				break;
			default: //wtf
				break;
		}
	}
	
	public string Equation(){
		string _operation = operations[(int)Random.Range(0,(operations.Count-1))] as string;
		int temp = (int)Random.Range(0,10);
		
		switch(_operation){
			case "+":
				EqFormat(temp,_operation);
				operation = EquationOperation.ADDITION;
				break;
			case "-":
				EqFormat(temp,_operation);
				operation = EquationOperation.SUBTRACTION;
				break;
			case "x":
				if(temp<1) temp = (int)Random.Range(1,9);
				EqFormat(temp,_operation);
				operation = EquationOperation.MULTIPLICATION;
				break;
			case "/":
				while(true){
						if(!solution.Equals(0) && !temp.Equals(0)) break;
						solution = (int)Random.Range(1,9);
						temp = (int)Random.Range(1,9);
				}
				EqFormat(temp,_operation);
				operation = EquationOperation.DIVISION;
				break;
			default: //wtf
				break;
		}
		return equation;
	}
	
	public int answer(){return solution;}
	public EquationOperation eqOperation(){return operation;}
}