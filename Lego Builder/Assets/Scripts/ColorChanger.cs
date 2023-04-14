using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{

    public PlayerMovement check;

    public Transform shootingPoint;
    public Color[] colors;
    public Color currentColor;

    public KeyCode change;

    public BuildingSystem buildingSystem;

    void CheckInventory() {
        for (int i = 0; i < colors.Length; i++) 
        {
            int n = i + 1;
            if (Input.GetKey(n.ToString()))
            {
                currentColor = colors[i];
            }
        }
    }
    private void ChangeColor()
    {
        if(Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo)){

            if (hitInfo.transform.tag == "BlockHit")
            {
                Hitbox script = hitInfo.transform.gameObject.GetComponent<Hitbox>();
                script.ChangeCol(currentColor);
                buildingSystem.normalColor = currentColor;
            }

        }
    }
    private void Start(){
        currentColor = colors[0];
    }
    private void Update()
    {
        if (check.colorMode){
            if (Input.GetMouseButtonDown(0)){
                ChangeColor();
            }
            CheckInventory();
        }
    }

}
