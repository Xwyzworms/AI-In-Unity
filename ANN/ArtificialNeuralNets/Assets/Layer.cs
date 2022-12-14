using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer
{
    public int numNeurons;
    public List<Neuron> neurons = new List<Neuron>();

    public Layer(int nNeurons, int prevNeuronsInput) 
    {
        this.numNeurons = nNeurons;
        for(int i =0; i<this.numNeurons;i++) 
        {
            neurons.Add(new Neuron(prevNeuronsInput));
        }
    }
}
