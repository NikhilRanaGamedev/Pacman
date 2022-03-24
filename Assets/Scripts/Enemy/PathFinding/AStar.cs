using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    Grid grid;

    public Transform seeker; // the set of nodes to be evaluated
    public Transform target; // the set of nodes already evaluated

    private float enemySpeed;

    // int pathNodeIndex = 0;

    void Awake()
    {
        grid = GetComponent<Grid>();
        enemySpeed = 5.5f;
    }

    public Vector3 MoveSeeker(Vector3 seekerPos, Vector3 targetPos)
    {
        if(path.Count != 0)
        {
           return Vector3.MoveTowards(seekerPos, path[0].position, enemySpeed * Time.deltaTime);
        } else if(path.Count == 0)
        {
          return Vector3.MoveTowards(seekerPos, targetPos, enemySpeed * Time.deltaTime);
        }

        return seekerPos;
    }

    public void PathFinding(Vector3 startPos, Vector3 targetPos) // Find shortest path from seeker to target
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode); // add currentNode to closed set

            if(currentNode == targetNode) // if currentNode is equal to targetNode than path has been found
            {
                RetracePath(startNode, targetNode); // Instantiate a path from start to target
                return;
            }

            foreach(Node neighbour in grid.GetNeighbours(currentNode)) // check neighbouring positions
            {
                if(!neighbour.walkable || closedSet.Contains(neighbour)) // if neighbour is walkable or not || neighbour is in closedSet
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) // if new path is shorter || neighbour is not in openSet
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode); // set fCost by assigning gCost and hCost
                    neighbour.parent = currentNode; // set parent of neighbour to currentNode

                    if(!openSet.Contains(neighbour)) // if neighbour is not in openSet
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    int GetDistance(Node nodeA, Node nodeB) // distance from one position to another
    {
        int disX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int disY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(disX > disY)
        {
            return 14 * disY + 10 * (disX - disY);
        } else
        {
            return 14 * disX + 10 * (disY - disX);
        }
    }

    public List<Node> path = new List<Node>();
    void RetracePath(Node startNode, Node endNode) // retrace path from start to target
    {
        // List<Node> path = new List<Node>();
        // if(path.Count != 0)
        // {
        //     if(seeker.position == path[0].position)
        //     {
                path.Clear();
        //     }
        // }
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        grid.path = path;
    }
}
