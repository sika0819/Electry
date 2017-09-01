using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGraph{//电路图
    private Dictionary<int,Node> vertxList;//邻接表,每个顶点都要有。
    private int edgeCount=0;//边节点数量
    private List<List<Node>> circlelist;
    private List<List<Node>> halfList;
    private List<List<Edge>> halfEdgeList;
    public Element battery;
    public LineGraph() {
        vertxList = new Dictionary<int, Node>();//储存序号
        circlelist = new List<List<Node>>();
        halfList = new List<List<Node>>();
        halfEdgeList = new List<List<Edge>>();
    }
    public int VertexCount
    {//顶点数量
        get {
            return vertxList.Count;
        }
    }
    public int EdgeCount {
        get {
            return edgeCount;
        }
    }
    public void addVertex(Node vertex) {
        //Debug.Log("添加节点："+vertex.index);
        if (!vertxList.ContainsKey(vertex.index))
        {
            vertxList.Add(vertex.index,vertex);
        }
    }
    public void addEdge(Edge e) {
        Node v = e.either();
        Node w = e.other(v);
        if (vertxList.ContainsKey(v.index))
        {
            vertxList[v.index].adj.Add(e);
            vertxList[w.index].adj.Add(e);
        }
        edgeCount++;
        CheckCircle();
        
        //Debug.Log("添加边:节点" + v.index + "————>节点" + w.index+" 电压"+e.Voltage);
       // Debug.Log(ToString());
    }

    
    public List<List<Node>> getHalfList()
    {
        return halfList;
    }
    public int halfCircleCount {
        get {
            return halfList.Count;
        }
    }
    public void addEdge(int v, int w) {
        Node a = vertxList[v];
        Node b = vertxList[w];
        Edge e = new Edge(a, b);
        addEdge(e);
        
    }
    public Node getVertex(int v) {
        return vertxList[v];
    }
    public List<Edge> getAdj(int v)
    {//返回v点相连的边
        return vertxList[v].adj;
    }
    public float allVoltage
    {
        get
        {
            return battery.Voltage;
        }
    }
    float m_allResistance = 0;
    public float allResistance
    {
        get
        {
            return m_allResistance;
        }
    }
    public Edge GetEdge(int nodeI, int nodeJ) {
        List<Edge> edges = getEdges();
        for (int i = 0; i < edges.Count; i++) {
            Node v = edges[i].either();
            Node w = edges[i].other(v);
            //Debug.Log(v.index+"——>"+w.index+"\n"+
            //    nodeI+"——>"+nodeJ);
            if ((v.index == nodeI && w.index == nodeJ)||(v.index==nodeJ&&w.index==nodeI))
                return edges[i];
        }
        Debug.LogError("没有找到边！");
        return null;
    }
    public List<Edge> getEdges()
    {//返回所有便的集合
        List<Edge> edges = new List<Edge>();
        for (int i = 0; i < VertexCount; i++)
        {
            for (int e = 0; e < getAdj(i).Count; e++)
            {
                edges.Add(getAdj(i)[e]);
            }
        }
        return edges;
    }
    public override string ToString()
    {
        string s = VertexCount + " 个顶点, " + EdgeCount + " 条边\n";
        for (int i = 0; i < VertexCount; i++)
        {
            s += i + ": ";
            for (int j = 0; j < vertxList[i].adj.Count; j++)
            {
                Edge e = vertxList[i].adj[j];
                s += "节点："+e.other(vertxList[i]).index+" ";
            }
            s += "\n";
        }
        return s;
    }
    public List<List<int>> AdjMatrix
    {
        get
        {
            List<List<int>> matrix1 = new List<List<int>>();
            for (int i = 0; i < VertexCount; i++)
            {
                List<int> matrix2 = new List<int>();
                for (int j = 0; j < VertexCount; j++)
                {
                    if (getVertex(i).hasPathTo(j))
                    {
                        matrix2.Add(1);
                    }
                    else
                    {
                        matrix2.Add(0);
                    }
                }
                matrix1.Add(matrix2);
            }
            return matrix1;
        }
    }

    public int CircleCount {
        get {
            return circlelist.Count;
        }
    }

    public void removeEdge(Edge lineEdge)
    {
        for (int i = 0; i < VertexCount; i++)
        {
            for (int j = vertxList[i].adj.Count-1; j >=0; j--)
            {
                if (vertxList[i].adj[j].Equals(lineEdge))
                {
                    vertxList[i].adj.RemoveAt(j);
                }
            }
            
        }
    }
    public void setBattery(Element battery) {
        this.battery = battery;
    }
    public void CheckCircle()
    {  
        int top = -1;
        int[] stack = new int[VertexCount];
        bool[] inStack = new bool[VertexCount];
        bool[] visited = new bool[VertexCount];
        circlelist = new List<List<Node>>();
        for (int i = 0; i < VertexCount; i++)
        {
            if (!visited[i])
            {
                DFS(i, visited, stack, ref top, inStack);
            }
        }
        generateHalf();
    }
    void generateHalf() {
        halfList = circlelist;
        for (int i = halfList.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < halfList[i].Count - 1; j++)
            {
                if (battery != null)
                {
                    Node v = halfList[i][j];
                    Node w = halfList[i][j + 1];
                    // Debug.Log(v.index+"——>"+w.index);
                    if (v.index == battery.startPoint.index && w.index == battery.endPoint.index)
                    {
                        halfList.RemoveAt(i);
                        break;
                    }
                    else if (!isLinkToBattery(halfList[i]))
                    {
                        halfList.RemoveAt(i);
                        break;
                    }
                }
            }
            
        }
        for (int temp = halfList.Count - 1; temp >= 0; temp--) {
            for (int a = temp; a >= 0; a--)
            {
                if (temp == a)
                    continue;
                int sameCount = 0;
                for (int halfLoop = halfList[temp].Count - 1; halfLoop >= 0; halfLoop--)
                {
                    for (int copyLoop = halfList[a].Count - 1; copyLoop >= 0; copyLoop--)
                    {
                        if (halfList[temp][halfLoop].index == halfList[a][copyLoop].index)
                        {
                            sameCount++;
                        }
                    }
                }
                Debug.Log(sameCount);
                if (sameCount == halfList[temp].Count && halfList[a].Count == halfList[temp].Count)
                {
                    halfList.RemoveAt(temp);
                    break;
                }
            }
        }
        halfEdgeList = new List<List<Edge>>();
        for (int i = 0; i < halfList.Count; i++)
        {
            List<Edge> edgeList=new List<Edge>();
            for (int j = 0; j < halfList[i].Count - 1; j++)
            {
                Edge e = GetEdge(halfList[i][j].index, halfList[i][j + 1].index);
                edgeList.Add(e);
            }
            Edge lastEdge = GetEdge(halfList[i][halfList.Count - 1].index, halfList[i][0].index);
            edgeList.Add(lastEdge);
            halfEdgeList.Add(edgeList);
        }
        Debug.Log(outHalfCircle());
        CheckElectryInform();
    }
    public string outHalfCircle() {
        string strResult = "有"+halfList.Count+"个环\n";
        for (int i = 0; i < halfList.Count; i++) {
            for (int j = 0; j < halfList[i].Count-1; j++)
            {
                strResult += " 节点" + halfList[i][j].index + "——>" + "节点" + halfList[i][j + 1].index;
            }
            strResult += "\n";
        }
        return strResult;
    }
    public string outCircle()
    {
        string strResult = "有" + circlelist.Count + "个环\n";
        for (int i = 0; i < circlelist.Count; i++)
        {
            for (int j = 0; j < circlelist[i].Count - 1; j++)
            {
                strResult += " 节点" + circlelist[i][j].index + "——>" + "节点" + circlelist[i][j + 1].index;
            }
            strResult += "\n";
        }
        return strResult;
    }
    void DFS(int x, bool[] visited, int[] stack, ref int top, bool[] inStack)
    {
        visited[x] = true;
        stack[++top] = x;
        inStack[x] = true;
        for (int i = 0; i < VertexCount; i++)
        {
            if (AdjMatrix[x][i] != 0)//有边  
            {
                if (!inStack[i])
                {
                    DFS(i, visited, stack, ref top, inStack);
                }
                else //条件成立，表示下标为x的顶点到 下标为i的顶点有环  
                {
                   // outstr = "第" + count + "环为:";
                    List<Node> circle = new List<Node>();//环
                    //从i到x是一个环，top的位置是x，下标为i的顶点在栈中的位置要寻找一下  
                    //寻找起始顶点下标在栈中的位置  
                    int t = 0;
                    for (t = top; stack[t] != i; t--) ;
                    //输出环中顶点  
                    for (int j = t; j <= top; j++)
                    {
                       // outstr += vertxList[stack[j]].index + "————>";
                        circle.Add(vertxList[stack[j]]);
                        
                    }
                   // circle.Add(vertxList[stack[t]]);
                    //outstr += stack[t] +"\n";
                    if (circle.Count > 3)
                    {
                        //count++;
                        circlelist.Add(circle);
                        //Debug.Log(outstr);
                    }
                }
            }
        }
        //处理完结点后，退栈  
        top--;
        inStack[x] = false;
    }
   
    bool isBingLian
    {
        get
        {
            List<List<Node>> circle = getHalfList();
            for (int i = 0; i < circle.Count; i++)
            {
                if (isLinkToBattery(circle[i]))
                {
                    CreateElement.Instance.ShowInform = "连接成功，当前为并联";
                    return true;
                }
            }
            return false;
        }
    }
    public void CheckElectryInform() {
        if (halfCircleCount > 0)
        {
            if (halfCircleCount == 1 && isLinkToBattery(getHalfList()[0]))
            {
                CreateElement.Instance.ShowInform = "连接成功，当前为串联";
                GenerateChuanLian();
            }
            else if (isBingLian)
            {
                GenerateBingLian();
            }
        }
        else
        {
            CreateElement.Instance.ShowInform = "当前为断路！请连接电源";

        }
    }
    class PairIndex {
        KeyValuePair<int, int> keypair;
        public PairIndex(int key1, int keyValue) {
            keypair = new KeyValuePair<int, int>(key1,keyValue);
        }
        public int PairValue
        {
            get
            {
                return keypair.Value;
            }
        }
        public int PairKey {
            get {
                return keypair.Key;
            }
        }
    }
    /// <summary>
    /// 计算串联电路
    /// </summary>
    public void GenerateChuanLian()
    {
        m_allResistance = 0;
        //Debug.Log(halfList.Count);
        for (int i = 0; i < halfList[0].Count - 1; i++)
        {
            Edge e = GetEdge(halfList[0][i].index, halfList[0][i + 1].index);
            //Debug.Log(halfList[0][i].index+"——>"+halfList[0][i + 1].index);
            m_allResistance += e.Resistance;
        }
        Edge lastedge = GetEdge(halfList[0][halfList[0].Count - 1].index, halfList[0][0].index);
        m_allResistance += lastedge.Resistance;
        if (allResistance != 0)
        {
            float allElectry = allVoltage / allResistance;
            Debug.Log("电阻：" + allResistance + " 电压:" + allVoltage + " 电流:" + allElectry);
            for (int i = 0; i < circlelist[0].Count - 1; i++)
            {
                Edge nowEdge = GetEdge(circlelist[0][i].index, circlelist[0][i + 1].index);
                nowEdge.Electry = allElectry;
                Element nowElement = CreateElement.Instance.GetElement(nowEdge.name);
                switch (nowElement.EleType)
                {
                    case ElementType.Light:
                        ElecLight light = CreateElement.Instance.GetEleLight(nowEdge.name);
                        light.setCurrency(allElectry);
                        light.Electry();
                        break;
                    default:
                        nowElement.Electry();
                        break;
                }
            }
            Edge lastEdge = GetEdge(circlelist[0][circlelist[0].Count - 1].index, circlelist[0][0].index);
            lastEdge.Electry = allElectry;
            Element lastElement = CreateElement.Instance.GetElement(lastEdge.name);
            FaDian(lastElement, allElectry);
        }
        else
        {
            CreateElement.Instance.ShowInform = "线路短路！请检查！";
        }
    }
    void FaDian(Element lastElement,float allElectry) {
        switch (lastElement.EleType)
        {
            case ElementType.Light:
                ElecLight light = CreateElement.Instance.GetEleLight(lastElement.LineEdge.name);
                light.setCurrency(allElectry);
                light.Electry();
                break;
            default:
                lastElement.Electry();
                break;
        }
    }
    /// <summary>
    /// 计算并联，找出环路之间重复的节点，重复的节点电阻沿着环路相加。再将所有环中去掉重复的边，将剩下同环上的相加，不同环按照并联公式计算
    /// </summary>
    public void GenerateBingLian()
    {
        CreateElement.Instance.ShowInform = "并联中。";
        Dictionary<PairIndex, List<Edge>> repeatEdge = new Dictionary<PairIndex, List<Edge>>();//储存重复节点
        Debug.Log(outHalfCircle());
        for (int i = 0; i < halfEdgeList.Count; i++)
        {
            for (int j = i; j < halfEdgeList.Count; j++)
            {
                if (i == j)
                    continue;
                string outstr = "";

                List<Edge> repeatSameCircle = new List<Edge>();//同一个环上重复的电阻
                outstr = "第" + i + "环与第" + j + "环进行比较\n";
                for (int halfLoop = 0; halfLoop < halfEdgeList[i].Count; halfLoop++)
                {
                    Edge e1 = halfEdgeList[i][halfLoop];
                    Node v1 = e1.either();
                    Node w1 = e1.other(v1);
                    for (int copyLoop = 0; copyLoop < halfEdgeList[j].Count; copyLoop++)
                    {
                        Edge e2 = halfEdgeList[j][copyLoop];
                        Node v2 = e2.either();
                        Node w2 = e2.other(v2);
                        if (e1.isEqual(e2))
                        {
                            outstr += "第" + i + "环与第" + j + "环重复的边：" + v1.index + "——>" + w1.index + "\n";
                            repeatSameCircle.Add(e1);
                        }
                    }
                }
                Debug.Log(outstr);
                repeatEdge.Add(new PairIndex(i, j), repeatSameCircle);
            }
        }
        List<List<Edge>> relessList = new List<List<Edge>>();
        for (int i = 0; i < halfEdgeList.Count; i++)
        {
            List<Edge> repeapList = new List<Edge>();
            foreach (KeyValuePair<PairIndex, List<Edge>> item in repeatEdge)
            {
                if (item.Key.PairKey == i || item.Key.PairValue == i)
                    repeapList = item.Value;
            }
            List<Edge> copyList = halfEdgeList[i];
            for (int copy = copyList.Count - 1; copy >= 0; copy--)
            {
                for (int rep = 0; rep < repeapList.Count; rep++)
                {
                    if (copyList[copy].isEqual(repeapList[rep]))
                    {
                        copyList.RemoveAt(copy);
                    }
                }
            }
            relessList.Add(copyList);
        }
        float allRepeatRes = 0;
        float upRes = 1;
        float dowRes = 0;
        float repeatRes = 0;
        foreach (KeyValuePair<PairIndex, List<Edge>> item in repeatEdge)
        {
            for (int i = 0; i < item.Value.Count; i++)
            {
                Debug.Log(item.Value[i]);
                repeatRes += item.Value[i].Resistance;
            }
            upRes *= repeatRes;
            dowRes += repeatRes;
        }
        if (dowRes != 0)
        {
            allRepeatRes = upRes / dowRes;
        }
        float allReless = 0;
        float up1 = 1;
        float down1 = 0;
        float relessResitance = 0;
        for (int i = 0; i < relessList.Count; i++)
        {

            for (int j = 0; j < relessList[i].Count; j++)
            {
                relessResitance += relessList[i][j].Resistance;
            }
            up1 *= relessResitance;
            down1 += relessResitance;
        }
        if (down1 != 0)
        {
            allReless = up1 / down1;
        }
        m_allResistance = allReless + allRepeatRes;
        float allElectry = allVoltage / m_allResistance;
        foreach (KeyValuePair<PairIndex, List<Edge>> item in repeatEdge)
        {
            for (int i = 0; i < item.Value.Count; i++)
            {
               Edge e= GetEdge(item.Value[i].either().index, item.Value[i].other(item.Value[i].either()).index);
            }
        }
    }
    public bool isLinkToBattery(List<Node> circle)
    {
        for (int i = 0; i < circle.Count - 1; i++)
        {
            if (battery != null)
            {
                // Debug.Log("电池："+battery.getFrom().index +"——>"+ battery.getTo().index+"\n"
                //    + circle[i].index + "——>" + circle[i + 1].index);
                if ((circle[i].index == battery.endPoint.index && circle[i + 1].index == battery.startPoint.index)||
                    (circle[i].index == battery.startPoint.index && circle[i + 1].index == battery.endPoint.index)
                    || (circle[0].index == battery.startPoint.index && circle[circle.Count-1].index == battery.endPoint.index)||
                    (circle[0].index == battery.endPoint.index && circle[circle.Count - 1].index == battery.startPoint.index))
                {
                    return true;
                }
            }
        }
        return false;
    }
    
}
