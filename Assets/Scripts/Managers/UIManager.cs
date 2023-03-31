using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject menu;

    void Start()
    {
        
    }

    public void OnPlayClicked()
    {
        GameManager.Instance.OnPlayClicked();
        menu.SetActive(false);
    }

}
