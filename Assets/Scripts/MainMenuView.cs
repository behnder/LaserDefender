using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    public Action ButtonStartGameClicked { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }



    private void AddListeners()
    {
    //    _buttonStart.onClick.AddListener(() =>
    //    {
    //        ButtonStartGameClicked.Invoke();
    //    });
    }


}
