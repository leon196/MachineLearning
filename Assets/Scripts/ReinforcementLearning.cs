using UnityEngine;
using System.Collections;

public class ReinforcementLearning : MonoBehaviour {

	public int bonus = 1;
	public int malus = -1;
	public float epsilon = 0.1;
	public float alpha = 0.2;
	public float gamma = 0.9;

	private string[] actions;
	private string[] states;
	private string[] q;
	// Use this for initialization
	void Start () {
	
		//Initialize array
		actions = new string["push", "turn", "growFlower", "growLeaf"];
		states = new string["rootPosition", "flowerPosition", "leafPosition"];
		q = new string[actions] [states];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	string GetQ(string state, string action)
	{
		return q [state] [action];
	}

	void LearnQ(string state, string action, int bonus, 
}

def learnQ(self, state, action, reward, value):
	oldv = self.q.get((state, action), None)
		if oldv is None:
			self.q[(state, action)] = reward
				else:
				self.q[(state, action)] = oldv + self.alpha * (value - oldv)