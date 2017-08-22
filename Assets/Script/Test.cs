using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int vertxCount = 8;
        DirectGraph g = new DirectGraph();
        List<Vertex> vertexArray=new List<Vertex>();
        for (int v = 0; v < vertxCount; v++)
        {
            Vertex vertex = new Vertex(v);
            vertexArray.Add(vertex);
            g.addVertex(vertex);
        }
        ElecEdge battery = new ElecEdge(g.getVertex(1), g.getVertex(0));
        battery.Voltage = 20;//设电池电压为20V
        g.addEdge(battery);
        g.SetBattery(battery);
        g.addEdge(0,2);
        ElecEdge R1 = new ElecEdge(vertexArray[2], vertexArray[5]);
        R1.Resistance = 5;
        g.addEdge(R1);
        g.addEdge(2, 3);
        ElecEdge R2 = new ElecEdge(vertexArray[3], vertexArray[4]);
        R2.Resistance = 5;
        g.addEdge(R2);
        g.addEdge(4,5);
        g.addEdge(2, 5);
        g.addEdge(5, 6);
        ElecEdge R3 = new ElecEdge(vertexArray[6], vertexArray[7]);
        R3.Resistance = 5;
        g.addEdge(R3);
        g.addEdge(7, 1);
        Debug.Log(g.toString());
        g.CheckCircle();
        Debug.Log("是否串联"+g.isSeries());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
