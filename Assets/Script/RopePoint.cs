using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePoint : MonoBehaviour {
    bool isMove=false;
    UltimateRope ropeController;
    Camera expCamera;
    Node linkedPoint;
    Rope rope;
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
        rope = CreateElement.Instance.GetRope(transform.parent.name);
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = expCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (isLink) {
            if(linkObj!=null)
            transform.position = linkObj.transform.position;
        }
        if (!isLink&&isMove)
        {
            if (Physics.Raycast(ray, out hit))
            {
                transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                ropeController.Regenerate(false);
            }

        }
        if (Input.GetMouseButtonUp(0)) {
            isMove = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            CreateElement.Instance.ShowInform = "右键取消连接";
            if (Physics.Raycast(ray, out hit))
            {
                CreateElement.Instance.ShowInform = "线连接模式，当前选中为：" + hit.collider.name;
                if (hit.collider.name == gameObject.name) {
                    linkObj = null;
                    rope.RemoveLink(rope.startPoint, rope.endPoint);
                    isLink =false;
                    isMove = true;
                }
            }
        }
	}
    void OnMouseDown() {
        isMove = true;
    }
    void OnTriggerEnter(Collider coli)
    {
        
        if (coli.tag == ResourceTool.POINT && coli.gameObject.transform.parent != transform.parent&&(!coli.transform.parent.name.Contains(ResourceTool.ROPE)))
        {
            if (!isLink)
            {
                linkObj = coli.gameObject;
              //  Debug.Log(linkObj.name);
                Node linkPoint= CreateElement.Instance.GetPoint(linkObj.name);
                isMove = false;
                transform.position = coli.gameObject.transform.position;

                isLink = true;
                linkedPoint = linkPoint;

                if (name == ResourceTool.STARTPOINT) {
                    rope.startPoint = linkedPoint;
    
                    rope.LinkObj1 = linkObj;
                }
                if (name == ResourceTool.ENDPOINT) {
                    rope.endPoint = linkedPoint;
                    
                    rope.LinkObj2 = linkObj;
                }

                if (rope.CanLink) {
                    rope.Link(rope.startPoint, rope.endPoint);
                }
            }
        }
    }
}
