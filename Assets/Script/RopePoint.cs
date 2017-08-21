using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePoint : MonoBehaviour {
    bool isMove=false;
    UltimateRope ropeController;
    Camera expCamera;
    Element eleLine;
    Element linkedEle;
    public GameObject linkObj {
        get {
            return _linkObj;
        }
        set {
            if (_linkObj != value) {
                _linkObj = value;
            }
        }
    }
    [SerializeField]
    private GameObject _linkObj;
    bool isLink=false;
    // Use this for initialization
    void Start () {
        ropeController = transform.parent.GetComponentInChildren<UltimateRope>();
        expCamera = GameObject.FindGameObjectWithTag(ResourceTool.EXPCAMERA).GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isLink) {
            if(linkObj!=null)
            transform.position = linkObj.transform.position;
        }
        if (isMove)
        {
            Ray ray = expCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                ropeController.Regenerate(false);
                if (isLink) {
                    
                    
                }
                isLink = false;
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            isMove = false;
        }
	}
    void OnMouseDown() {
        isMove = true;
    }
    void OnTriggerEnter(Collider coli)
    {
        
        if (coli.tag == "Point" && coli.gameObject.transform.parent != transform.parent)
        {
            if (!isLink)
            {
                linkObj = coli.gameObject;
                eleLine = CreateElement.Instance.GetElement(transform.parent.name);
              //  Debug.Log(linkObj.transform.parent.name);
                linkedEle = CreateElement.Instance.GetElement(linkObj.transform.parent.name);
                if (transform.name == ResourceTool.STARTPOINT)
                {
                    //eleLine.startIsLinked = true;
                    //eleLine.startEleObj = linkedEle;
                    if (linkObj.name == ResourceTool.STARTPOINT)
                    {
                        //linkedEle.startEleObj = eleLine;
                    }
                    else
                    {
                        //linkedEle.endEle = eleLine;
                    }
                }
                else
                {
                    //eleLine.endIsLinked = true;
                    //eleLine.endEle = linkedEle;
                    if (linkObj.name == ResourceTool.STARTPOINT)
                    {
                        //linkedEle.startEleObj = eleLine;
                    }
                    else
                    {
                        //linkedEle.endEle = eleLine;
                    }
                }
                isMove = false;
                transform.position = coli.gameObject.transform.position;
              //  Debug.Log("bling~连接");
                isLink = true;

            }
        }
    }
}
