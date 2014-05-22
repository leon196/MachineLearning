using UnityEngine;
using System.Collections;

public class Manager {

	private static Manager instance;

	private Manager() {}

	public static Manager Instance {
		get  {
			if (instance == null) { instance = new Manager(); }
			return instance;
		}
	}

	private ResourceGenerator _grid = null;
	public ResourceGenerator GetGrid() { 
		if (!_grid) { 
			_grid = Object.FindObjectOfType(typeof(ResourceGenerator)) as ResourceGenerator;
		}
		return _grid;
	}
}
