using UnityEngine;
using System;
using System.Collections;
using System.Linq;
public class ReinforcementLearning : MonoBehaviour {

	public int bonus = 1;
	public int malus = -1;

	private float[] actions = new float[9];
	private float[] states = new float[9];
    private float[][] q = new float[18][];
    private float epsilon = 0.1f;
    private float alpha = 0.2f;
    private float gamma = 0.9f;

	// Use this for initialization
	void Start () 
	{
		//Initialize array
//		actions = new string["push", "turn", "growFlower", "growLeaf"];
//		states = new string["rootPosition", "flowerPosition", "leafPosition"];

    }
	// Update is called once per frame
	void Update () {
    }

    /// <summary>
    /// get the Q value at a specified index
    /// </summary>
    /// <param name="state_index">index of state</param>
    /// <param name="action_index">index of action</param>
    /// <returns>float value of a Q</returns>
    float getQ(int state_index, int action_index)
    {
        if (q[state_index][action_index]!=null)
            return q[state_index][action_index];
        else
            return -1.1f;
    }

    void learnQ(int state_index, int action_index, float reward, float value)
    {
        float oldv = getQ(state_index, action_index);
        if(oldv == -1.1f)
        {
            q[state_index][action_index] = reward;
        }
        else
        {
            q[state_index][action_index] = oldv + alpha * (value - oldv);
        }
    }


    float chooseAction(int state_index, bool return_Q = false)
    {
        System.Random rand = new System.Random();
        float[] copy_q = new float[9];
        float[] best = new float[9];
        int iterator = 0;
        int count = 0;
        float minQ = Int32.MaxValue;
        float maxQ = Int32.MinValue;
        float mag;
        float action;

        for(int a = 0; a < actions.Length; a++)
        {
            copy_q[a] = getQ(state_index, a);
        }

        if(rand.NextDouble() < epsilon)
        {
            minQ = copy_q.Min();
            mag = Math.Max(Math.Abs(minQ), Math.Abs(maxQ));
            for(int i = 0 ; i < actions.Length; i++)
            {
                copy_q[i] = (float)(copy_q[i] + rand.NextDouble() * mag - 0.5f * mag);
            }
            maxQ = copy_q.Max();
        }

        foreach(var qs in copy_q)
        {
            if (qs == maxQ) count++;
        }

        if(count > 1)
        {
            for (int i = 0, j = 0; i < actions.Length; i++)
                best[j++] = (copy_q[i] == maxQ) ? copy_q[i] : 0f;
            iterator = rand.Next(9);
        }
        else
        {
            for (int i = 0; i < copy_q.Length; i++)
                if (copy_q[i] == maxQ)
                    iterator = i;
        }

        action = actions[iterator];

        if(return_Q)
        {
            for (int i = 0; i < copy_q.Length; i++)
                q[state_index][i] = copy_q[i];
        }
        return action;
    }

    void learn(int state1_index, int action1_index, float reward, int state2_index)
    {
        float maxQnew = Int32.MinValue;
        for (int a = 0; a < actions.Length; a++)
            maxQnew = Math.Max(maxQnew, getQ(state2_index, a));
        learnQ(state1_index, action1_index, reward, reward + gamma * maxQnew);
    }


}
