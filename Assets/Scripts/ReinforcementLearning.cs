using UnityEngine;
using System.Collections;

public class ReinforcementLearning : MonoBehaviour {

	public int bonus = 1;
	public int malus = -1;
	public float epsilon = 0.1;
	public float alpha = 0.2;
	public float gamma = 0.9;

	private int[] actions = new int[4];
	private int[] states = new int[3];
	private int[] q;
	// Use this for initialization
	void Start () {
	
		//Initialize array
//		actions = new string["push", "turn", "growFlower", "growLeaf"];
//		states = new string["rootPosition", "flowerPosition", "leafPosition"];
		q = new int[actions.Length] [states.Length];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	string GetQ(string state, string action)
	{
		return q [state] [action];
	}

	void LearnQ(string state, string action, int bonus, float value)
	{
		string oldQValue = GetQ (state, action);
		//TO DO
	}
}