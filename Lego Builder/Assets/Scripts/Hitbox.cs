using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public GameObject parent;
    public GameObject[] components;
    
    public void ChangeCol(Color color) 
    {
        for (int i = 0; i < components.Length; i++) 
        {
            components[i].GetComponent<MeshRenderer> ().material.color = color;
        }
    } 
}
