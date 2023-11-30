using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageFix : MonoBehaviour, IPointerEnterHandler
{
    public GameObject desc;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        desc.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
