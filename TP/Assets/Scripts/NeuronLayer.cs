using UnityEngine;
using System.Collections;

public class NeuronLayer {
	public int neuronCount;
	public ArrayList neurons; //<Neuron>

	public NeuronLayer (int neuronCount_, int inputCountPerNeuron_)
	{
		neurons = new ArrayList();
		for (int i = 0; i < neuronCount_; ++i) {
			neurons.Add(new Neuron(inputCountPerNeuron_));
		}
	}
}
