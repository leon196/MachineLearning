using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour
{

	// Genetic Elements
	public int rootCount = 3;
	public GameObject rootPrefab;
	private List<Root> roots;
	private int rootsCount;

	// Neural Network
	private NeuralNet brain;
	private int inputs = 0;
	private int outputs = 0;
	private int layers = 1;
	private int neurons = 6;

	void Start() {

		roots = new List<Root>();
		for (int r = 0; r < rootCount; r++) {
			GameObject rootObject = Instantiate(rootPrefab) as GameObject;
			rootObject.transform.parent = transform;
			rootObject.transform.localPosition = Vector3.zero;
			roots.Add(rootObject.GetComponent<Root>());

			// add leaves inputs (one per root for now)
			inputs += 1;
			// add roots outputs (translation & rotation)
			outputs += 2;
		}

		brain = new NeuralNet();
		brain.CreateNetwork(inputs, outputs, layers, neurons);
	}

	void Update() {

		List<double> inputs = GetLeavesEnergy();
		List<double> ouputs = brain.Update(inputs);

		int rootTranslation = 0;
		int rootRotation = 1;
		foreach (Root root in roots) {
			root.Grow(ouputs[rootTranslation], ouputs[rootRotation]);
			// offset
			rootTranslation += 2;
			rootRotation += 2;
		}
	}

	List<double> GetLeavesEnergy() {
		List<double> energies = new List<double>();
		foreach (Root root in roots) {
			List<Leaf> leaves = root.Leaves;
			foreach (Leaf leaf in leaves) {
				energies.Add(leaf.Energy);
			}
		}
		return energies;
	}
}
