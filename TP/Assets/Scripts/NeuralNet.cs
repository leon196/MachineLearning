using UnityEngine;
using System.Collections;

public class NeuralNet {

	private int inputCount = 1;
	private int outputCount = 2;
	private int layerHiddenCount = 1;
	private int neuronCountPerLayer = 6;
	private ArrayList layers; // <NeuronLayer> 
	private double bias = -1.0;
	private double activationResponse = 1.0;

	public void CreateNetwork() {
		layers = new ArrayList();
		//create the layers of the network
		if (layerHiddenCount > 0) {
			//create first hidden layer
			layers.Add(new NeuronLayer(neuronCountPerLayer, inputCount));

			for (int i=0; i<layerHiddenCount-1; ++i) {
				layers.Add(new NeuronLayer(neuronCountPerLayer, neuronCountPerLayer));
			}

			//create output layer
			layers.Add(new NeuronLayer(outputCount, neuronCountPerLayer));
		}

		else {
			//create output layer
			layers.Add(new NeuronLayer(outputCount, inputCount));
		}
	}

	public ArrayList /*<double>*/ GetWeights() {
		//this will hold the weights
		ArrayList weights = new ArrayList();
		
		//for each layer
		for (int i = 0; i < layerHiddenCount + 1; ++i) {
			//for each neuron
			NeuronLayer layer = layers[i] as NeuronLayer;
			for (int j = 0; j < layer.neuronCount; ++j) {
				//for each weight
				Neuron neuron = layer.neurons[j] as Neuron;
				for (int k = 0; k < neuron.inputCount; ++k) {
					weights.Add((double)neuron.inputWeights[k]);
				}
			}
		}
		return weights;
	}

	public int GetNumberOfWeights() {

		int weights = 0;
		
		//for each layer
		for (int i = 0; i < layerHiddenCount + 1; ++i) {
			NeuronLayer layer = layers[i] as NeuronLayer;
			//for each neuron
			for (int j = 0; j < layer.neuronCount; ++j) {
				Neuron neuron = layer.neurons[j] as Neuron;
				//for each weight
				for (int k = 0; k < neuron.inputCount; ++k) {
					weights++;
				}
			}
		}
		return weights;
	}

	public void PutWeights(ArrayList weights /*<double>*/) {
		int cWeight = 0;
		
		//for each layer
		for (int i = 0; i < layerHiddenCount + 1; ++i) {
			NeuronLayer layer = layers[i] as NeuronLayer;
			//for each neuron
			for (int j = 0; j < layer.neuronCount; ++j) {
				Neuron neuron = layer.neurons[j] as Neuron;
				//for each weight
				for (int k = 0; k < neuron.inputCount; ++k) {
					neuron.inputWeights[k] = weights[cWeight++];
				}
			}
		}
	}

	public ArrayList /*<double>*/ Update(ArrayList /*<double>*/ inputs) {
		ArrayList outputs = new ArrayList();
		int weight = 0;
		if (inputs.Count != inputCount) {
			return outputs;
		}
		for (int i = 0; i < layerHiddenCount + 1; ++i) {
			if (i > 0) {
				inputs = outputs;
			}

			outputs.Clear();
			weight = 0;

			NeuronLayer layer = layers[i] as NeuronLayer;
			int neuronCount = layer.neuronCount;

			for (int j = 0; j < neuronCount; ++j) {
				Neuron neuron = layer.neurons[j] as Neuron;
				double netInput = 0;
				int numInputs = neuron.inputCount;
				for (int k = 0; k < numInputs-1; ++k) {
					netInput += (double)neuron.inputWeights[k] * (double)inputs[weight++];
				}
				netInput += (double)neuron.inputWeights[numInputs-1] * bias;
				outputs.Add(Sigmoid(netInput, activationResponse));
				weight = 0;
			}
		}

		return outputs;
	}

	public double Sigmoid(double activation, double response) {
		return ( 1.0 / ( 1.0 + Mathf.Exp((float) (-activation / response))));
	}

}
