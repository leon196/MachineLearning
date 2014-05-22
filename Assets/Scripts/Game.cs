using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public GameObject plantPrefab;
	private List<Plant> plants;

	// Use this for initialization
	void Start () {

		plants = new List<Plant>();
		int plantCount = 2;

		// Generate Plants
		for (int p = 0; p < plantCount; p++) {

			int flowerCount = Random.Range(1, 4);
			int[] rootCount = new int[flowerCount];
			for (int f = 0; f < flowerCount; f++) {
				rootCount[f] = Random.Range(1, 5);
			}

			GameObject plantObject = Instantiate(plantPrefab) as GameObject;

			float range = 3.0f;
			plantObject.transform.position = p == 0 ? new Vector3(-range, -range, 0.0f) : new Vector3(range, range, 0.0f);

			Plant plant = plantObject.GetComponent<Plant>();
			plant.GeneratePlant(flowerCount, rootCount);
			plants.Add(plant);
		}


	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}
