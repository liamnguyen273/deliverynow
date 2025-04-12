using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testScroll : MonoBehaviour
{
    [SerializeField] Image _test;
    [SerializeField] Transform _container;
    private void Start()
    {
        for(int i = 0 ; i < 50; i++)
        {
            Instantiate(_test, _container);
        }
    }
}
