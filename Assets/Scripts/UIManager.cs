using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject loginUI;
    [SerializeField] private GameObject countUI;

    private void Start()
    {
        var loginScreen = loginUI.GetComponent<LoginScreen>();
        
        loginScreen.loggedIn += () =>
        {
            loginUI.SetActive(false);
            countUI.SetActive(true);
        };
    }
}
