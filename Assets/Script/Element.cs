using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element{//电子元器件基类
    private ElecEdge electryElement;//边节点
    private Vertex pos;//正极点
    private Vertex negative;//负极点
    private Edge insideEdge;
    private Node point1;
    private Node point2;
    private ElementType elementType;
    
    private GameObject eleObj;
    private GameObject consoleArea;
    #region public Property
    public string name {
        get {
            return eleObj.name;
        }
    }
    public float Currency
    {
        get
        {
            return pos.Electry;
        }
    }
    public float Resistance
    {
        get
        {
            if (electryElement != null)
            {
                return electryElement.Resistance;
            }
            else {
                return 0;
            }
        }
        set
        {
            if (electryElement != null)
            {
               // Debug.Log("将"+name+"的电阻设为："+value);
                electryElement.Resistance = value;
            }
        }
    }
    public GameObject EleObj//电子元器件物体
    {
        get
        {
            return eleObj;
        }
        set
        {
            eleObj = value;
        }
    }
    public ElecEdge ElectryEdge
    {//返回元器件
        get
        {
            return electryElement;
        }set {
            electryElement = value;
        }
    }
    public Edge LineEdge {
        get {
            return insideEdge;
        }set {
            insideEdge = value;
        }
    }
    public Node startPoint {
        get {
            if (point1 == null)
                Debug.LogError("节点为空");
            
            return point1;
        }set {
            if (point1 == null)
                Debug.LogError("设置空节点");
            point1 = value;
        }
    }
    public Node endPoint {
        get {
            return point2;
        }set {
            point2 = value;
        }
    }
    public GameObject StartVertexObj
    {
        get
        {
            if(startVertexObj != null)
            return startVertexObj;
            Debug.LogError("获取点程序错误");
            return null;
        }
        set
        {
            if (value != null)
            {
                startVertexObj = value;
            }
            else {
                Debug.LogError("获取点程序错误");
            }
        }
    }
    private GameObject startVertexObj;
    public GameObject EndVertexObj
    {
        get
        {
            if(endVertexObj != null)
            return endVertexObj;
            Debug.LogError("获取点程序错误");
            return null;
        }
        set
        {
            if (value != null)
            {
                endVertexObj=value;
            }
            else {
                Debug.LogError("获取点程序错误");
            }
        }
    }
    private GameObject endVertexObj;
    #endregion
    public Element() {
        consoleArea = GameObject.Find(ResourceTool.CONSOLEAREA);
    }
    public void Init(Element copyEle) {
        this.eleObj = copyEle.EleObj;
        this.electryElement = copyEle.ElectryEdge;
        this.pos = copyEle.Pos;
        this.negative = copyEle.Negative;
        this.elementType = copyEle.EleType;
        this.point1 =copyEle.startPoint;
        this.point2 =copyEle.endPoint;
        this.insideEdge = copyEle.LineEdge;
        this.electryElement = copyEle.ElectryEdge;
        this.Resistance = copyEle.Resistance;
    }
    public void Init(GameObject obj,int i)
    {
        eleObj = ResourceTool.InstitateGameObject(obj);
        eleObj.name = obj.name + i;
        eleObj.transform.SetParent(consoleArea.transform);
    }

    public virtual void Electry() {
        //Debug.Log("电流："+Currency+" 电阻："+Resistance+" 电压:"+Voltage);
    }
    public void setCurrency(float current) {//设置电流
        pos.setElectry(current);
        negative.setElectry(current);
    }
    
   
    public void SetResistance(float value) {
        Resistance = value;
        if (ElectryEdge != null) {
            ElectryEdge.Resistance = value;
        }
        if (insideEdge != null) {
            insideEdge.Resistance = value;
        }
    }
    public void InitEleType(ElementType initType)
    {
        elementType = initType;
        if (initType != ElementType.Line)
        {
           
            point1 = new Node(GenrateIndex.Instance.Index);
            point2 = new Node(GenrateIndex.Instance.Index);
            pos = new Vertex(point1.index);
            negative = new Vertex(point2.index);
            if (initType != ElementType.Battery)
            {
                electryElement = new ElecEdge(pos, negative);//从正极到负极建立一个有向边
            }
            else {
                electryElement = new ElecEdge(negative, pos);
            }
            electryElement.Resistance = 0;
            electryElement.name = name;
            insideEdge = new Edge(point1, point2);
            //Debug.Log(insideEdge);
            insideEdge.name = name;
        }
        else {
            //pos = new Vertex();
            //negative = new Vertex();
            point1 = new Node();
            point2 = new Node();
        }
        
        if (eleObj.transform.FindChild(ResourceTool.STARTPOINT))
        {
            startVertexObj = eleObj.transform.FindChild(ResourceTool.STARTPOINT).gameObject;
        }
        else
        {
            startVertexObj = new GameObject();
            startVertexObj.transform.SetParent(eleObj.transform);
            startVertexObj.name = ResourceTool.STARTPOINT;
        }
        if (eleObj.transform.FindChild(ResourceTool.ENDPOINT))
        {
            endVertexObj = eleObj.transform.FindChild(ResourceTool.ENDPOINT).gameObject;
        }
        else
        {
            endVertexObj = new GameObject();
            endVertexObj.transform.SetParent(eleObj.transform);
            endVertexObj.name = ResourceTool.ENDPOINT;
        }
        if (initType != ElementType.Line)
        {
            startVertexObj.name = ResourceTool.STARTPOINT + pos.index;
            endVertexObj.name = ResourceTool.ENDPOINT + negative.index;
        }
        point1.InitGameObj(startVertexObj);
        point2.InitGameObj(endVertexObj);
    }
    public void SetPoint(Vector3 start, Vector3 end)
    {
        StartVertexObj.transform.localPosition = start;
        EndVertexObj.transform.localPosition = end;
    }
    public void SetPosition(Vector3 pos)
    {
        eleObj.transform.localPosition = pos;
    }
    public void SetRotation(Vector3 eulerAngle)
    {
        eleObj.transform.localRotation = Quaternion.Euler(eulerAngle);
    }
    public void SetEleObj(GameObject go)
    {
        eleObj = go;
    }
    public float Voltage {
        get {
            return electryElement.Voltage;
        }set {
            SetVoltage(value);
        }
    }
    void SetVoltage(float v)
    {
        electryElement.Voltage = v;
    }
    public ElementType EleType {
        get {
            return elementType;
        }
    }
    public Vertex Pos {
        get {
            return pos;
        }set {
            pos = value;
        }
    }
    public Vertex Negative {
        get {
            return negative;
        }set {
            negative = value;
        }
    }
    public override string ToString()
    {
        return point1.index+" " + point2.index;
    }
}
