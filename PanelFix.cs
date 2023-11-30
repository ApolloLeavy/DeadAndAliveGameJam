
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelFix : MonoBehaviour,  IPointerExitHandler
{

    public GameObject desc;
    // Start is called before the first frame update
    void Start()
    {
        desc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnPointerExit(PointerEventData eventData)
    {
        desc.SetActive(false);

    }
}