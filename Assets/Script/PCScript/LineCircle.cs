using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LineCircle {
    private bool[] visited;
    private int[] edgeTo; // 记录路径  
    private bool[] inStack;
    private StringBuilder sb;
    private bool hasCicle = false;
    public LineCircle(LineGraph G)
    {
        visited = new bool[G.VertexCount];
        edgeTo = new int[G.VertexCount];
        sb = new StringBuilder();
        inStack = new bool[G.VertexCount];
        for (int i = 0; i < G.VertexCount; i++)
        {
            visited[i] = false;
        }
        for (int i = 0; i < G.VertexCount; i++)
        {
            if (!visited[i])
            {
                DFS(G, i);
            }
        }

    }
    private void DFS(LineGraph G, int i)
    {
        inStack[i] = true;
        visited[i] = true;
        for (int node = 0; node < G.getAdj(i).Count; node++)
        {
            int nodeValue = G.getAdj(i)[node].either().index;
            if (!visited[nodeValue])
            {
                edgeTo[nodeValue] = i;
                
                DFS(G, nodeValue);
            }
            else if(inStack[nodeValue])
            {
                //for (int j = i; j != nodeValue; j = edgeTo[j])
                //{
                //    sb.Append(G.getVertex(j).index + "----->");
                //}
                hasCicle = true;
               // sb.Append(nodeValue + "——————》");
            }
        }
        sb.Append("\n");
    }
    public bool HasCycle {
        get {
            return hasCicle;
        }
    }
    public override string ToString()
    {
        return sb.ToString();
    }
}
