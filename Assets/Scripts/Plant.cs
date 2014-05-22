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

		int[] rootCount = {3, 5, 1};
		GeneratePlant(3, rootCount);
	}

	void GeneratePlant(int flowerCount, int[] rootCount)
	{
		for (int d = 0; d < flowerCount; d++) {
			GameObject flowerObject = Instantiate(flowerPrefab) as GameObject;
			flowerObject.transform.parent = transform;
			float range = 10.0f;
			flowerObject.transform.localPosition = new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0);
			flowers.Add(flowerObject.GetComponent<Flower>());

			// add flower inputs (one per flower for now)
			inputs += 1;

			// Generate Roots
			for (int r = 0; r < rootCount[d]; r++) {
				GameObject rootObject = Instantiate(rootPrefab) as GameObject;
				rootObject.transform.parent = transform;
				rootObject.transform.localPosition = flowerObject.transform.localPosition;
				roots.Add(rootObject.GetComponent<Root>());

				// add leaves inputs (one per root for now)
				//inputs += 0;
				// add roots outputs 
				outputs += outputPerRoot;
			}
		}


		brain = new NeuralNet();
		brain.CreateNetwork(inputs, outputs, layers, neurons);

		ready = true;
	}

	void Update() {

		if (ready) {
			List<double> inputs = GetLeavesEnergy();
			List<double> ouputs = brain.Update(inputs);

			int rootTranslation = 0;
			int rootRotation = 1;
			int rootGrowth = 2;
			foreach (Root root in roots) {
				root.Grow(ouputs[rootTranslation], ouputs[rootRotation], ouputs[rootGrowth]);
				// offset
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
