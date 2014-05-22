using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Root : MonoBehaviour
{
	private NeuralNet brain;
	private Leaf[] leaves;

	private float lineAngle = 0.0f;
	private LineRenderer lineRenderer;
	private int lineCount = 1;
	private float lineLength = 0.5f;
	private Vector3[] linePositions;
	private Vector3 lastPosition;

	void Start() {
		brain = new NeuralNet();
		brain.CreateNetwork();

		leaves = GetComponentsInChildren<Leaf>() as Leaf[];

		lineRenderer = GetComponent<LineRenderer>() as LineRenderer;
		linePositions = new Vector3[100];
		linePositions[0] = new Vector3();
		linePositions[1] = new Vector3();

		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, linePositions[0]);
		lineRenderer.SetPosition(1, linePositions[1]);

		lastPosition = linePositions[lineCount];
	}

	void Update() {
		List<double> inputs = new List<double>();
		foreach (Leaf leaf in leaves) {
			inputs.Add(leaf.Intensity);
		}
		List<double> ouputs = brain.Update(inputs);

		float factorTranslation = (float)ouputs[0];
		float factorRotation = ((float)ouputs[1] - 0.5f) * 2.0f;
		
		lineAngle = factorRotation * Mathf.PI * 10.0f;
		Vector3 rotation = new Vector3(Mathf.Cos(lineAngle), Mathf.Sin(lineAngle), 0);

		Vector3 nextPosition = lastPosition + factorTranslation * rotation * Time.deltaTime;
		linePositions[lineCount] = nextPosition;
		lastPosition = nextPosition;

		lineRenderer.SetPosition(lineCount, nextPosition);

		if (Vector3.Distance(linePositions[lineCount-1], nextPosition) >= lineLength) {
			lineCount++;
			linePositions[lineCount] = linePositions[lineCount-1];
			lineRenderer.SetVertexCount(lineCount+1);
			lineRenderer.SetPosition(lineCount, linePositions[lineCount]);
			lastPosition = linePositions[lineCount];
		}

	}
}
