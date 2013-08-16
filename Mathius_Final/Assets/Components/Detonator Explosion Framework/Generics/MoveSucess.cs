using UnityEngine;
using System.Collections;
using System;

public class MoveSucess<T> : EventArgs{
		
	public T _arg{get; private set;}
		
	public MoveSucess(T arg){
			_arg = arg;
	}
}
		
	
