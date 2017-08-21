using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int vertxCount = 4;
        DirectGraph g = new DirectGraph();
        LineGraph dianlutu = new LineGraph();
        List<int> vertexArray=new List<int>();
        for (int v = 0; v < vertxCount; v++)
        {
            Vertex vertex = new Vertex(v);
            vertexArray.Add(vertex.index);
            g.addVertex(vertex);
            Node node = new Node(v);
            dianlutu.addVertex(node);
        }
        g.addEdge(0, 1);
        g.addEdge(0, 2);
        g.addEdge(1, 3);
        g.addEdge(2, 3);
        g.addEdge(3, 0);
        List<List<List<int>>> outlist = new List<List<List<int>>>();
        DirectCycle c = new DirectCycle(g);
        Debug.Log(c.toString());
        Debug.Log(g.toString());
        int n1 = 0;
        StringBuilder sb = new StringBuilder();
        foreach (List<List<int>> i in outlist)
        {
            n1++;
            Debug.Log("第" + n1 + "个循环：");
            int n2 = 0;

            foreach (List<int> j in i)
            {
                n2++;
                switch (n2)
                {
                    case 1: sb.Append("循环部件为："); break;
                    case 2: sb.Append("循环进口为："); break;
                    case 3: sb.Append("循环出口为："); break;
                }

                foreach (int k in j)
                {
                    sb.Append(k);
                }
                Debug.Log(sb);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
