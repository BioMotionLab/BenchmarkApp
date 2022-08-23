using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangramsCalc : MonoBehaviour
{
    [SerializeField] Transform phone;
    [SerializeField] Transform screen;
    public Vector3 pos;
    public Vector3 rot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = phone.position - screen.position;
        rot = phone.rotation.eulerAngles - screen.rotation.eulerAngles;
    }
}
