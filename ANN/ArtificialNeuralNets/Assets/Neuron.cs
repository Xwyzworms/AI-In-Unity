using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron 
{
    public int numInputs;
    public double output;
    public double bias;
    public double errorGradient;

    public List<double> weights = new List<double>();
    public List<double> inputs  = new List<double>();
    
    public Neuron(int numInputs) 
    {
        bias = UnityEngine.Random.Range(-1.0f, 1.0f);
        this.numInputs =numInputs; 
        // Init Weights

        for(int i = 0; i < this.numInputs; i++) 
        {
            weights.Add(UnityEngine.Random.Range(-1.0f, 1.0f));
        }
    }
}
