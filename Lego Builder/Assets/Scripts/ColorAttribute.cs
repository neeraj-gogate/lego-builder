using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAttribute : MonoBehaviour
{
    public GameObject[] components;

    public void Change(Color color) 
    {
        for (int i = 0; i < components.Length; i++) 
        {
            components[i].GetComponent<MeshRenderer> ().material.color = color;
        }
    } 
}
