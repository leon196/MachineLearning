﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour
{

	// Flowers
	public GameObject flowerPrefab;
	private List<Flower>  flowers;

	// Roots
	public GameObject rootPrefab;
	private List<Root> roots;

	// Neural Network
	private NeuralNet brain;
	private int inputs = 0;
	private int outputs = 0;
	private int layers = 2;
	private int neurons = 9;

	private int outputPerRoot = 3; // translation & rotation & growth

	private bool ready = false;

	Vector3 RandomVector(float range) { return new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range)); }

	void Start()
	{
		// Generate Plant
		flowers = new List<Flower>();
		roots = new List<Root>();

		int flowerCount = Random.Range(1, 4);
		int[] rootCount = new int[flowerCount];
		for (int f = 0; f < flowerCount; f++) {
			rootCount[f] = Random.Range(1, 5);
		}
		GeneratePlant(flowerCount, rootCount);
	}

	void GeneratePlant(int flowerCount, int[] rootCount)
	{
		ready = false;

		Vector3 plantPosition = new Vector3();
		for (int d = 0; d < flowerCount; d++) {


			// Generate Flower
			GameObject flowerObject = Instantiate(flowerPrefab) as GameObject;
			flowerObject.transform.parent = transform;
			float range = 4.0f;
			flowerObject.transform.localPosition = new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0);
			flowers.Add(flowerObject.GetComponent<Flower>());

			// add flower inputs (one per flower for now)
			inputs += 1;

			// Generate Roots
			for (int r = 0; r < rootCount[d]; r++) {
				GameObject rootObject = Instantiate(rootPrefab) as GameObject;
				rootObject.transform.parent = transform;
				rootObject.transform.localPosition = flowerObject.transform.localPosition;
				Root rootComponent = rootObject.GetComponent<Root>();
				rootComponent.Setup();
				roots.Add(rootComponent);

				// add leaves inputs (one per root for now)
				//inputs += 0;
				// add roots outputs 
				outputs += outputPerRoot;
			}

			// Generate Link between flowers
			if (d > 0) {
				GameObject rootObject = Instantiate(rootPrefab) as GameObject;
				rootObject.transform.parent = transform;
				rootObject.transform.localPosition = plantPosition;
				rootObject.GetComponent<LineRenderer>().SetPosition(0, plantPosition);
				rootObject.GetComponent<LineRenderer>().SetPosition(1, flowerObject.transform.position);
			} else {
				plantPosition = flowerObject.transform.position;
			}	
		}

		brain = new NeuralNet();
		brain.CreateNetwork(inputs, outputs, layers, neurons);

		ready = true;
	}

	// Update Neural Network
	void Update() {

		if (ready) {

			// Get Inputs
			List<double> inputs = GetLeavesEnergy();

			// Get Outputs
			List<double> ouputs = brain.Update(inputs);

			// Indexes
			int rootTranslation = 0;
			int rootRotation = 1;
			int rootGrowth = 2;

			foreach (Root root in roots) {

				// Update Root
				root.Grow(ouputs[rootTranslation], ouputs[rootRotation], ouputs[rootGrowth]);

				rootTranslation += outputPerRoot;
				rootRotation += outputPerRoot;
				rootGrowth += outputPerRoot;
			}
		}
	}

	List<double> GetLeavesEnergy() {
		List<double> energies = new List<double>();

		// Flowers Energy
		foreach (Flower flower in flowers) {
			energies.Add(flower.Energy);
		}

		// Roots Leaves Energy
		foreach (Root root in roots) {
			List<Leaf> leaves = root.Leaves;
			foreach (Leaf leaf in leaves) {
				energies.Add(leaf.Energy);
			}
		}
		return energies;
	}

	public void AddBrainInput() {
		ready = false;
		inputs++;
		brain.CreateNetwork(inputs, outputs, layers, neurons);
		ready = true;
	}
}
