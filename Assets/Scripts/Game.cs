using UnityEngine;
using System.Collections;

public class Game {

	private static Game instance;

	private Game() {}

	public static Game Instance {
		get  {
			if (instance == null) { instance = new Game(); }
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
