using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathMarker
{

    public MapLocation location;
    public float g; // Starting Points
    public float f; // Total
    public float h; // Cost To Goal
    public GameObject marker;
    public PathMarker parent;

    public PathMarker(MapLocation l ,float g,
            float h,float f, 
            GameObject marker, PathMarker p) 
    {
        this.location = l;
        this.g = g;
        this.f = f;
        this.h = h;
        this.marker = marker;
        this.parent = p;
    }

    public override bool Equals(object obj) 
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }

        else 
        {
            return location.Equals(((PathMarker) obj).location);
        }
    }
    
    public override int GetHashCode() 
    {
        return 0;
    }





}
