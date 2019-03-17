using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideMap : MonoBehaviour
{
    void Start()
    {
        Assets.Scripts.Player.PlayerEvents.PlayerTeleported += OnPlayerTeleported;
    }

    void OnPlayerTeleported()
    {
        canvas.gameObject.SetActive(false);
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
