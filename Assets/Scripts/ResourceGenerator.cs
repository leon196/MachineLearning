using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceGenerator : MonoBehaviour {

	public GameObject capturedPrefab;
	public GameObject lightPrefab;
	private GameObject grid;
	private List<ResourceLight> lights;
	private int gridDimension = 18;
	private float lightScale = 2.0f;

	private bool[] gridCaptured;
	private List<GameObject> gridCapturedObjects;

	private float minDistanceBetweenLightAndLeaf = 1.0f;

	// Use this for initialization
	void Start ()
	{
		grid = new GameObject();
		grid.transform.parent = transform;
		grid.name = "Grid";

		gridCapturedObjects = new List<GameObject>();
		gridCaptured = new bool[gridDimension*gridDimension];
		for (int c = 0; c < gridDimension*gridDimension; c++) {
			gridCaptured[c] = false;
		}

		// ResourceLights
		lights = new List<ResourceLight>();
		int lightCount = 70;
		float half = GetHalf();
		for (int l = 0; l < lightCount; l++)
		{
			int randomIndex = Random.Range(0, gridDimension * gridDimension);
			float intensity = Random.Range(0.4f, 8.0f);

			GameObject lightObject = Instantiate(lightPrefab) as GameObject;
			lightObject.transform.position = new Vector3((randomIndex % gridDimension) * lightScale, Mathf.Floor(randomIndex/gridDimension) * lightScale, 0);
			lightObject.transform.position -= new Vector3(half, half, 0);
			lightObject.transform.rotation = Quaternion.Euler(0, -180.0f, 0);
			lightObject.transform.parent = grid.transform;
			lightObject.renderer.material.color = new Color(intensity, intensity, Random.Range(0.1f, 0.3f), 1.0f);

			ResourceLight light = lightObject.GetComponent<ResourceLight>();
			light.intensity = intensity;
			lights.Add(light);
		}
	}

	public float GetHalf() {
		return gridDimension * lightScale * 0.5f;
	}
	
	//public List<ResourceLight> Resouces { get { return lights; } }

	public float GetIntensityPosition(Vector3 target) {
		float intensity = 0.0f;
		foreach (ResourceLight light in lights) {
			if (Vector3.Distance(light.transform.position, target) <= minDistanceBetweenLightAndLeaf) {
				intensity += light.intensity;
			}
		}
		return intensity;
	}

	public float GetMinDistanceBetweenRootAndLight(Vector3 target) {
		float distance = 0.0f;
		foreach (ResourceLight light in lights) {
			float dist = Vector3.Distance(light.transform.position, target);
			if (dist < distance) {
				distance = dist;
			}
		}
		return distance;
	}

	public int CheckGridPosition(Vector3 target, Color caseColor) {

		float half = GetHalf();
		if (target.x < -half || target.x > half || target.y < -half || target.y > half) {
			return 0;
		}

		int index = (int)(Mathf.Round(target.x / lightScale + half / lightScale)) + (int)(Mathf.Round(target.y / lightScale + half / lightScale) * gridDimension);
		if (index >= 0 && index < gridDimension * gridDimension -1 && !gridCaptured[index]) {
			gridCaptured[index] = true;
			GameObject caseCaptured = Instantiate(capturedPrefab) as GameObject;
			caseCaptured.transform.position = new Vector3((index % gridDimension) * lightScale - half, Mathf.Floor(index/gridDimension) * lightScale - half, 0);
			caseCaptured.transform.parent = grid.transform;
			caseCaptured.renderer.material.SetColor("_Color", new Color(caseColor.r, caseColor.g, caseColor.b, 0.1f));
			caseCaptured.renderer.material.SetColor("_Emission", caseColor);
			gridCapturedObjects.Add(caseCaptured);
			return 1;
		} else {
			return 0;
		}
	}

	public void Restart() {
		foreach (GameObject gridCapturedObject in gridCapturedObjects) {
			Destroy(gridCapturedObject);
		}
		for (int c = 0; c < gridDimension*gridDimension; c++) {
			gridCaptured[c] = false;
		}
	}
}
