using UnityEngine;
using System.Collections;

public class Leaf : MonoBehaviour {

	private TextMesh text;
	private float intensity = 0.0f;

	void Start ()
	{
		text = GetComponentInChildren<TextMesh>() as TextMesh;
	}
	
	void Update ()
	{
		intensity = Mathf.Cos(Time.time);
		text.text = "" + intensity;
	}

	public float Intensity { get { return intensity; } }
}
