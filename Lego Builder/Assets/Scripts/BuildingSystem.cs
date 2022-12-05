using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject block;

    void MakeBlock(GameObject block)
    {
        ///Physics.Raycast();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MakeBlock(block);
        }
    }
}
