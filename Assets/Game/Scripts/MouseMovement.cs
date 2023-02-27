using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urapirat.Movement;

[RequireComponent(typeof(Navigation))]
public class MouseMovement : MonoBehaviour
{
    private Navigation navigation;
    //private Vector3 previousMousePosition = Vector3.zero;

    void Awake()
    {
        navigation = GetComponent<Navigation>();
    }

    void FixedUpdate()
    {
        //if (previousMousePosition != Input.mousePosition)
        //{
        //    navigation.SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //    previousMousePosition = Input.mousePosition;
        //}

        navigation.SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
