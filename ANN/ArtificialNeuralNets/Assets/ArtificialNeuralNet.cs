using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialNeuralNet
{   
    public int nOutputs;
    public int nInputs;
    public int nHiddenLayers;
    public int nNeuronsPerHiddenLayers;
    public double learningRate = 0.001;
    List<Layer> layers = new List<Layer>();

    public ArtificialNeuralNet(int nInputs, int nOutputs,
            int nHidden, int nNPhidden, double lr) 
    {
        this.nInputs = nInputs;
        this.nOutputs = nOutputs;
        this.nHiddenLayers = nHidden;
        this.nNeuronsPerHiddenLayers = nNPhidden;
        this.learningRate = lr;

        if(nHiddenLayers > 0) 
        {
            layers.Add(new Layer(nNeuronsPerHiddenLayers, nInputs));
            for(int i = 0; i<nHiddenLayers - 1; i++) 
            {
                layers.Add(new Layer(nNeuronsPerHiddenLayers, nNeuronsPerHiddenLayers));
            }

            layers.Add(new Layer(nOutputs, nNeuronsPerHiddenLayers));
        } 
        else 
        {
            layers.Add(new Layer(nOutputs, nInputs));
        }
    }

}
