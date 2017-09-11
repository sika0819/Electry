using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CusorBar : MonoBehaviour {
    public bool StartClick = false;
    public float speed;
    public Transform LoadingBar;
    [SerializeField]
    private float currentAmount;
    public bool Clicked = false;
    Image progress;
    Image top;
    // Use this for initialization
    void Start()
    {
        currentAmount = 0;
        progress = transform.Find("progress").GetComponent<Image>();
        top = transform.Find("top").GetComponent<Image>();
        progress.gameObject.SetActive(false);
        top.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartClick)
        {
            if (currentAmount < 100)
            {
                top.color = Color.white;
                progress.gameObject.SetActive(true);
                top.gameObject.SetActive(true);
                currentAmount += speed * Time.deltaTime;
            }
            else
            {
                currentAmount = 0;
            }
            LoadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
        }
        else {
            progress.gameObject.SetActive(false);
            top.gameObject.SetActive(false);
            currentAmount = 0;
        }
        if (Clicked) {
            progress.gameObject.SetActive(false);
            top.color = Color.green;
            currentAmount = 0;
            Invoke("ResetClicked", 1);
        }
    }
    void ResetClicked() {
        Clicked = false;
    }
}
