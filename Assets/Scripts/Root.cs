using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Root : MonoBehaviour
{
	private List<Leaf> leaves;
	public List<Leaf> Leaves { get { return leaves;} }

	private LineRenderer lineRenderer;
	private int lineCount = 1;
	private float lineAngle = 0.0f;
	private float lineLength = 0.1f;
	private Vector3 lastPosition;
	private Vector3[] linePositions;
	private const int LINE_COUNT = 1000;

	void Start() {

		leaves = (GetComponentsInChildren<Leaf>() as Leaf[]).ToList<Leaf>();

		lineRenderer = GetComponent<LineRenderer>() as LineRenderer;
		linePositions = new Vector3[LINE_COUNT];
		linePositions[0] = new Vector3();
		linePositions[1] = new Vector3();

		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, linePositions[0]);
		lineRenderer.SetPosition(1, linePositions[1]);

		lastPosition = linePositions[lineCount];
	}

	public void Grow(double factorTranslation, double factorRotation) {
		
		lineAngle = (float)factorRotation * Mathf.PI * 10.0f;
		Vector3 rotation = new Vector3(Mathf.Cos(lineAngle), Mathf.Sin(lineAngle), 0);

		Vector3 nextPosition = lastPosition + (float)factorTranslation * rotation * Time.deltaTime;
		linePositions[lineCount] = nextPosition;
		lastPosition = nextPosition;

		lineRenderer.SetPosition(lineCount, nextPosition);

		if (lineCount < LINE_COUNT-1 && Vector3.Distance(linePositions[lineCount-1], nextPosition) >= lineLength) {
			lineCount++;
			linePositions[lineCount] = linePositions[lineCount-1];
			lineRenderer.SetVertexCount(lineCount+1);
			lineRenderer.SetPosition(lineCount, linePositions[lineCount]);
			lastPosition = linePositions[lineCount];
		}

	}
}
