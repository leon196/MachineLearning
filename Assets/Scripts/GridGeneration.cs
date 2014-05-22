using UnityEngine;
using System.Collections;

public class GridGeneration : MonoBehaviour {

	public GameObject gridCase;
	private int gridDimension = 9;
	private float gridCaseScale = 2.0f;

	// Use this for initialization
	void Start () {
		for (int i = gridDimension * gridDimension - 1; i >= 0; i--) {
			GameObject plane = Instantiate(gridCase) as GameObject;
			plane.transform.position = new Vector3((i % gridDimension) * gridCaseScale, Mathf.Floor(i/gridDimension) * gridCaseScale, 0);
			plane.transform.rotation = Quaternion.Euler(0, -180.0f, 0);
			plane.transform.parent = transform;
			plane.renderer.material.color = new Color(Random.Range(0.1f, 0.4f), Random.Range(0.4f, 8.0f), Random.Range(0.1f, 0.3f), 1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
