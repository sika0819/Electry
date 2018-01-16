using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePoint : MonoBehaviour {
    bool isMove=false;
    UltimateRope ropeController;
    Camera expCamera;
    Node linkedPoint;
    Rope rope;
    //Experiment exp;
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
        GameObject cameraObj = GameObject.FindGameObjectWithTag(ResourceTool.EXPCAMERA);
        if (cameraObj != null)
        {
            expCamera=cameraObj.GetComponent<Camera>();
        }
        rope = CreateElement.Instance.GetRope(transform.parent.name);
        //GameObject expObj = GameObject.Find(ResourceTool.EXPERIMENT);
        //if (expObj != null) {
        //    exp = expObj.GetComponent<Experiment>();
        //}
    }
	
	// Update is called once per frame
	void Update () {
        ropeController.Regenerate(false);
        if (expCamera != null)
        {
            Ray ray = expCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (isLink)
            {
                if (linkObj != null)
                    transform.position = linkObj.transform.position;
            }
            if (!isLink && isMove)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                   
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                isMove = false;
            }
            if (linkObj == null)
            {
                isLink = false;
            }
        }
    }
    void OnMouseDown() {
        isMove = true;
    }
    void OnTriggerEnter(Collider coli)
    {
        if (coli.tag == ResourceTool.POINT && coli.gameObject.transform.parent != transform.parent && (!coli.transform.parent.name.Contains(ResourceTool.ROPE)))
        {
            if (!isLink)
            {
                linkObj = coli.gameObject;
                Debug.Log(linkObj);
                if (isMove)
                {
                    SetLinkObj(coli.gameObject);
                }
            }
        }
    }
    public void SetLinkObj(GameObject LinkObj) {
        if (!isLink&&LinkObj.transform.parent != transform.parent && (!LinkObj.transform.parent.name.Contains(ResourceTool.ROPE))){
            Node linkPoint = CreateElement.Instance.GetPoint(LinkObj.name);
            linkObj = LinkObj;
            transform.position = LinkObj.transform.position;
            isMove = false;
            linkedPoint = linkPoint;

            if (name == ResourceTool.STARTPOINT)
            {
                rope.startPoint = linkedPoint;
                rope.LinkObj1 = LinkObj;
            }
            if (name == ResourceTool.ENDPOINT)
            {
                rope.endPoint = linkedPoint;
                rope.LinkObj2 = LinkObj;
            }
            if (rope.CanLink)
            {
                rope.Link(rope.startPoint, rope.endPoint);
            }
            isLink = true;
        }
    }
}
