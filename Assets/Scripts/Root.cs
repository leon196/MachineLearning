using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Root : MonoBehaviour
{
	private Plant plant;

	public GameObject leafPrefab;
	private List<Leaf> leaves = new List<Leaf>();
	public List<Leaf> Leaves { get { return leaves; } }
	private float leafGrowth = 0.0f;

	private LineRenderer lineRenderer;
	private int lineCount = 1;
	private float angle = 0.0f;
	private float lineMaxSegment = 0.1f;
	private Vector3 lastPosition;
	private Vector3[] linePositions;
	private const int LINE_COUNT = 1000;

	private float polarity = 1.0f;

	private float halfGrid;

	public void CreateLeaf()
	{
		// Spawn GameObject
		GameObject leaf = Instantiate(leafPrefab) as GameObject;
		leaf.transform.parent = transform;
		leaf.transform.position = lastPosition;

		// Update List
		leaves = (GetComponentsInChildren<Leaf>() as Leaf[]).ToList<Leaf>();

		// Update Brain
		if (!plant.SimpleInput) {
			plant.AddBrainInput();
		}
	}

	public void Setup()
	{
		halfGrid = Manager.Instance.GetGrid().GetHalf();

		lineRenderer = GetComponent<LineRenderer>() as LineRenderer;

		linePositions = new Vector3[LINE_COUNT];
		linePositions[0] = transform.position;
		linePositions[1] = transform.position;

		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, linePositions[0]);
		lineRenderer.SetPosition(1, linePositions[1]);

		lastPosition = linePositions[lineCount];

		plant = transform.parent.GetComponent<Plant>();
	}

	public void Grow(double factorTranslation, double factorRotation, double factorLeaf)
	{
		// Rotation & Translation
		angle = (float)factorRotation * polarity;
		Vector3 rotation = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
		Vector3 nextPosition = lastPosition + (float)factorTranslation * rotation * Time.deltaTime;

		if (polarity > 0 && (nextPosition.x < -halfGrid || nextPosition.x > halfGrid || nextPosition.y < -halfGrid || nextPosition.y > halfGrid)) {
			//angle += Mathf.PI;
			//rotation = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
			//nextPosition = lastPosition + (float)factorTranslation * rotation * Time.deltaTime;
			polarity *= -1.0f;
		}

		// Clamp screen borders
		//float offset = 1.0f;
		//nextPosition = new Vector3(Mathf.Max(-halfGrid+offset, Mathf.Min(nextPosition.x, halfGrid-offset)), Mathf.Max(-halfGrid+offset, Mathf.Min(nextPosition.y, halfGrid-offset)), 0);
		
		linePositions[lineCount] = nextPosition;
		lastPosition = nextPosition;

		// Update LineRenderer
		lineRenderer.SetPosition(lineCount, nextPosition);
		if (lineCount < LINE_COUNT-1 && Vector3.Distance(linePositions[lineCount-1], nextPosition) >= lineMaxSegment) {
			lineCount++;
			linePositions[lineCount] = linePositions[lineCount-1];
			lineRenderer.SetVertexCount(lineCount+1);
			lineRenderer.SetPosition(lineCount, linePositions[lineCount]);
			lastPosition = linePositions[lineCount];

		}

		// Leaf Growth
		leafGrowth += (float)factorLeaf;
		if (leafGrowth >= 1.0f) {
			CreateLeaf();
			leafGrowth = 0.0f;

			plant.AddCaseGrid(Manager.Instance.GetGrid().CheckGridPosition(lastPosition));
		}
	}
}
