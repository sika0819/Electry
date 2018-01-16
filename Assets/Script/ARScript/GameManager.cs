using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour {
   
    public static GameManager Instance {
        get {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GameManager>();
            if (_instance == null)
            {
                GameObject temp = new GameObject();
                temp.AddComponent<GameManager>();
                _instance = temp.GetComponent<GameManager>();
            }
            return _instance;
        }
    }
    private static GameManager _instance;
    private bool isCreateRopeStart=false;
    private GameObject Cursor;
    private GameObject CursorTarget;
    private Dictionary<string, Element> elementList;

    GameObject cursorObj;
    private Element createdElement;

    // Use this for initialization
    void Start () {
        Cursor = GameObject.Find(ResourceTool.PROGRESS);
        elementList = new Dictionary<string, Element>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isCreateRopeStart) {
            Cursor.transform.position = CursorTarget.transform.position;
        }
    }
    public void CreateStart(GameObject go)
    {
        isCreateRopeStart = true;
        CursorTarget = go;
    }
    /// <summary>
    /// 创建物体
    /// </summary>
    /// <param name="GameObjName"></param>
    public void CreateGameObj(Transform parent,string GameObjName)
    {
        isCreateRopeStart = false;
       
        elementList.Add(createdElement.name, createdElement);
    }

    /// <summary>
    /// 销毁物体
    /// </summary>
    /// <param name="createObjName"></param>
    public void DestoryGameObj(string createObjName)
    {
        Element createdElement = CreateElement.Instance.GetElement(createObjName);
        if (createdElement != null)
        {
            ResourceTool.DestoryGameObject(createdElement.EleObj);
            CreateElement.Instance.RemoveElement(createdElement);
            createdElement.Destory();
            createdElement = null;
        }
    }

    /// <summary>
    /// 退出
    /// </summary>
    void OnApplicationQuit()
    {
        CreateElement.Instance.OnDestory();
        GenrateIndex.Instance.OnDestory();
    }
}
