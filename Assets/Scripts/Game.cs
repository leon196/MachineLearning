using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public bool SimpleInputMode = false;
	public GameObject plantPrefab;
	private List<Plant> plants;

	// Use this for initialization
	void Start () {


		GeneratePlants();
	}

	void GeneratePlants()
	{
		plants = new List<Plant>();
		int plantCount = 3;
		for (int p = 0; p < plantCount; p++) {

			int flowerCount = Random.Range(2, 4);
			int[] rootCount = new int[flowerCount];
			for (int f = 0; f < flowerCount; f++) {
				rootCount[f] = Random.Range(3, 5);
			}

			GameObject plantObject = Instantiate(plantPrefab) as GameObject;
			plantObject.transform.parent = transform;

			float range = 8.0f;
			plantObject.transform.position = new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0.0f);

			Plant plant = plantObject.GetComponent<Plant>();
			plant.GeneratePlant(flowerCount, rootCount, SimpleInputMode);
			plants.Add(plant);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			Restart();
		}
	}

	void Restart() {

		foreach (Plant plant in plants) {
			Destroy(plant.gameObject);
		}
		GeneratePlants();

		Manager.Instance.GetGrid().Restart();
	}
}
