using UnityEngine;
using System.Collections;

public class Script : MonoBehaviour {

	private LineRenderer racine;
	private float timeStart = 0.0f;
	private int lineCount = 1;
	private float lineLength = 1.0f;
	private Vector3[] linePositions;

	// Use this for initialization
	void Start () {
		racine = GetComponentInChildren<LineRenderer>() as LineRenderer;
		linePositions = new Vector3[100];
		linePositions[0] = new Vector3();
		linePositions[1] = new Vector3();

		racine.SetVertexCount(2);
		racine.SetPosition(0, linePositions[0]);
		racine.SetPosition(1, linePositions[1]);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = linePositions[lineCount];
		Vector3 nextPosition = currentPosition + new Vector3(Mathf.Abs(Mathf.Cos(Time.time)), 0, Mathf.Sin(Time.time * 0.4f)) * Time.deltaTime;
		linePositions[lineCount] = nextPosition;

		racine.SetPosition(lineCount, nextPosition);

		if (Vector3.Distance(linePositions[lineCount-1], nextPosition) >= lineLength) {
			lineCount++;
			linePositions[lineCount] = linePositions[lineCount-1];
			racine.SetVertexCount(lineCount+1);
			racine.SetPosition(lineCount, linePositions[lineCount]);
		}
	}
}
