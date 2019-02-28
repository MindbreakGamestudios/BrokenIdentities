using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideMap : MonoBehaviour
{
    void Start()
    {
        
    }

    [SerializeField]
    private Canvas canvas;
    void Update()
    {
        if (canvas != null && Input.GetButtonDown("ShowHideMap"))
        {
            if (canvas.isActiveAndEnabled)
                canvas.gameObject.SetActive(false);
            else
                canvas.gameObject.SetActive(true);
        }
    }
}
