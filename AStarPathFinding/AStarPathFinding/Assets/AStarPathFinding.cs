using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class AStarPathFinding : MonoBehaviour
{
    
    public Maze maze;
    public Material closedMaterial;
    public Material openMaterial;
    public GameObject start;
    public GameObject end;
    public GameObject pathP;

    PathMarker startNode;
    PathMarker goalNode;
    PathMarker lastPos;
    bool done = false;
    bool hasStarted = false;

    List<PathMarker> open = new List<PathMarker>();
    List<PathMarker> closed = new List<PathMarker>();

    void RemoveAllMarkers() {

        GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");

        foreach (GameObject m in markers) Destroy(m);
    }

    void BeginSearch() {

        done = false;
        RemoveAllMarkers();

        List<MapLocation> locations = new List<MapLocation>();

        for (int z = 1; z < maze.depth - 1; ++z) {
            for (int x = 1; x < maze.width - 1; ++x) {

                if (maze.map[x, z] != 1) {

                    locations.Add(new MapLocation(x, z));
                }
            }
        }
        locations.Shuffle();

        Vector3 startLocation = new Vector3(locations[0].x * maze.scale, 0.0f, locations[0].z * maze.scale);
        startNode = new PathMarker(new MapLocation(locations[0].x, locations[0].z),
            0.0f, 0.0f, 0.0f, Instantiate(start, startLocation, Quaternion.identity), null);

        Vector3 endLocation = new Vector3(locations[1].x * maze.scale, 0.0f, locations[1].z * maze.scale);
        goalNode = new PathMarker(new MapLocation(locations[1].x, locations[1].z),
            0.0f, 0.0f, 0.0f, Instantiate(end, endLocation, Quaternion.identity), null);

        open.Clear();
        closed.Clear();

        open.Add(startNode);
        lastPos = startNode;
    }

    void SearchIT(PathMarker thisNode) {

        if (thisNode.Equals(goalNode)) {

            done = true;
            // Debug.Log("DONE!");
            return;
        }

        foreach (MapLocation dir in maze.directions) {

            MapLocation neighbour = dir + thisNode.location;

            if (maze.map[neighbour.x, neighbour.z] == 1) continue;
            if (neighbour.x < 1 || neighbour.x >= maze.width || neighbour.z < 1 || neighbour.z >= maze.depth) continue;
            if (IsClosed(neighbour)) continue;

            float totalTraverse = Vector2.Distance(thisNode.location.ToVector(), neighbour.ToVector()) + thisNode.g;
            float distanceToGoal = Vector2.Distance(neighbour.ToVector(), goalNode.location.ToVector());
            float f = totalTraverse + distanceToGoal;

            GameObject pathBlock = Instantiate(pathP, new Vector3(neighbour.x * maze.scale, 0.0f, neighbour.z * maze.scale), Quaternion.identity);

            TextMesh[] values = pathBlock.GetComponentsInChildren<TextMesh>();

            values[0].text = "G: " + totalTraverse.ToString("0.00");
            values[1].text = "H: " + distanceToGoal.ToString("0.00");
            values[2].text = "F: " + f.ToString("0.00");

            if (!UpdateMarker(neighbour, totalTraverse, distanceToGoal, f, thisNode)) {

                open.Add(new PathMarker(neighbour, totalTraverse, distanceToGoal, f, pathBlock, thisNode));
            }
        }
        // Get The lowest F
        open = open.OrderBy(p => p.f).ToList<PathMarker>();
        PathMarker pm = (PathMarker)open.ElementAt(0);
        // Add it to close
        closed.Add(pm);

        open.RemoveAt(0);
        pm.marker.GetComponent<Renderer>().material = closedMaterial;
        Debug.Log("PM " +  pm.f);
        lastPos = pm;
        Debug.Log(lastPos.f);
    }

    bool UpdateMarker(MapLocation pos, float g, float h, float f, PathMarker prt) {

        foreach (PathMarker p in open) {

            if (p.location.Equals(pos)) {

                p.g = g;
                p.h = h;
                p.f = f;
                p.parent = prt;
                return true;
            }
        }
        return false;
    }

    bool IsClosed(MapLocation marker) {
        foreach (PathMarker p in closed) {

            if (p.location.Equals(marker)) return true;
        }
        return false;
    }

    void Start() {

    }

    private void GetThePath() 
    {
        RemoveAllMarkers();
        PathMarker begin = lastPos;
        Instantiate(pathP, new Vector3(lastPos.location.x *maze.scale, 0.0f, lastPos.location.z * maze.scale), Quaternion.identity);
        while(!begin.location.Equals(startNode.location) && begin != null) 
        
        {

            Instantiate(pathP, new Vector3(lastPos.location.x *maze.scale, 0.0f, lastPos.location.z * maze.scale), Quaternion.identity);
            PathMarker childLocation = lastPos.parent;
            lastPos = childLocation;
        }

        Instantiate(pathP, new Vector3(lastPos.location.x *maze.scale, 0.0f, lastPos.location.z * maze.scale), Quaternion.identity);
        return;


    }
    void Update() {

        if (Input.GetKeyDown(KeyCode.P)) {

            BeginSearch();
            hasStarted = true;
        }

        if (Input.GetKeyDown(KeyCode.C) && !done) SearchIT(lastPos);
        if (Input.GetKeyDown(KeyCode.M)) GetThePath();
    }
}
