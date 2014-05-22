using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceGenerator : MonoBehaviour {

	public GameObject lightPrefab;
	private GameObject grid;
	private List<ResourceLight> lights;
	private int gridDimension = 9;
	private float lightScale = 1.0f;

	private float minDistanceBetweenLightAndLeaf = 0.5f;

	// Use this for initialization
	void Start ()
	{
		grid = new GameObject();
		grid.transform.parent = transform;
		grid.name = "Grid";

		// ResourceLights
		lights = new List<ResourceLight>();
		int lightCount = 20;
		float half = gridDimension * lightScale * 0.5f;
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
}
