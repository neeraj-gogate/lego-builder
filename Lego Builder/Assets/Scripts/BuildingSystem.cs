using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public PlayerMovement check;
    public ColorChanger colorChanger;

    public Transform shootingPoint;

    //public GameObject standardBlock;
    //private Vector3 sBSize;

    public Arr[] blocks;
    GameObject block;
    GameObject blockHitbox;

    public Color normalColor;
    public Color highlightedColor;
    GameObject lastHighlight;

    void CheckInventory() {
        for (int i = 0; i < blocks.Length; i++) 
        {
            int n = i + 1;
            if (Input.GetKey(n.ToString()))
            {
                block = blocks[i].comps[0];
                blockHitbox = blocks[i].comps[1];
            }
        }
    }
    void MakeBlock(GameObject block)
    {
        if(Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
            Vector3 pos = hitInfo.point;
            Vector3 normal = hitInfo.normal;
            Vector3 hitSize = hitInfo.transform.gameObject.GetComponent<Renderer>().bounds.size;
            Vector3 blockSize = blockHitbox.transform.gameObject.GetComponent<Renderer>().bounds.size;
            if (hitInfo.transform.tag == "BlockHit")
            {
                //Vector3 spawnPosition = new Vector3(Mathf.RoundToInt((hitInfo.point.x)/hitSize.x  + hitInfo.normal.x/2) * hitSize.x, Mathf.RoundToInt((hitInfo.point.y)/hitSize.y  + hitInfo.normal.y/2)  * hitSize.y, Mathf.RoundToInt((hitInfo.point.z)/hitSize.z  + hitInfo.normal.z/2)  * hitSize.z);
                Vector3 spawnPosition = new Vector3(Mathf.RoundToInt((hitInfo.point.x)/blockSize.x  + hitInfo.normal.x/2) * blockSize.x, Mathf.RoundToInt((hitInfo.point.y)/blockSize.y  + hitInfo.normal.y/2)  * blockSize.y, Mathf.RoundToInt((hitInfo.point.z)/blockSize.z  + hitInfo.normal.z/2)  * blockSize.z);

                GameObject clone = Instantiate(block, spawnPosition, Quaternion.identity);
                clone.transform.Find("Hitbox").gameObject.GetComponent<Hitbox>().ChangeCol(colorChanger.currentColor);
            }
            else
            {
                //Vector3 spawnPosition = new Vector3(Mathf.RoundToInt((hitInfo.point.x)/hitSize.x  + hitInfo.normal.x/2) * hitSize.x, Mathf.RoundToInt((hitInfo.point.y)/hitSize.y  + hitInfo.normal.y/2)  * hitSize.y, Mathf.RoundToInt((hitInfo.point.z)/hitSize.z  + hitInfo.normal.z/2)  * hitSize.z);
                Vector3 spawnPosition = new Vector3(Mathf.RoundToInt((hitInfo.point.x)/blockSize.x  + hitInfo.normal.x/2) * blockSize.x, Mathf.RoundToInt((hitInfo.point.y)/blockSize.y  + hitInfo.normal.y/2)  * blockSize.y, Mathf.RoundToInt((hitInfo.point.z)/blockSize.z  + hitInfo.normal.z/2)  * blockSize.z);

                //Vector3 spawnPosition = new Vector3(Mathf.RoundToInt((hitInfo.point.x)/sBSize.x  + hitInfo.normal.x/2) * sBSize.x, Mathf.RoundToInt((hitInfo.point.y)/sBSize.y  + hitInfo.normal.y/2)  * sBSize.y, Mathf.RoundToInt((hitInfo.point.z)/sBSize.z  + hitInfo.normal.z/2)  * sBSize.z);
                //Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x/sBSize.x)*sBSize.x,Mathf.RoundToInt(hitInfo.point.y/sBSize.y)*sBSize.y,Mathf.RoundToInt(hitInfo.point.z/sBSize.z)*sBSize.z);
                GameObject clone =  Instantiate(block, spawnPosition, Quaternion.identity);
                clone.transform.Find("Hitbox").gameObject.GetComponent<Hitbox>().ChangeCol(colorChanger.currentColor);
            }
        }
    }
    private void Start()
    {
        block = blocks[0].comps[0];
        blockHitbox = blocks[0].comps[1];
        //sBSize = standardBlock.GetComponent<Renderer>().bounds.size;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MakeBlock(block);
        }
        if (!check.colorMode){
            if (Input.GetMouseButtonDown(0))
            {
                DestroyBlock();
            }
            CheckInventory();
        }
        HighlightBlock();
        
    }
    void DestroyBlock()
    {
        if(Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.tag == "BlockHit")
            {
                Hitbox check = hitInfo.transform.gameObject.GetComponent<Hitbox>();
                if (check != null){
                    Destroy(check.parent);
                }
            }
        }

    }
    void HighlightBlock()
    {
        if(Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
            Hitbox hitbox = hitInfo.transform.gameObject.GetComponent<Hitbox>();
            if (hitInfo.transform.tag == "BlockHit")
            {
                if (lastHighlight == null)
                {
                    lastHighlight = hitInfo.transform.gameObject;
                    normalColor = hitbox.components[0].GetComponent<Renderer>().material.color;
                    hitbox.ChangeCol(highlightedColor);
                }
                else if (lastHighlight != hitInfo.transform.gameObject)
                {
                    lastHighlight.GetComponent<Hitbox>().ChangeCol(normalColor);
                    normalColor = hitbox.components[0].GetComponent<Renderer>().material.color;
                    hitbox.ChangeCol(highlightedColor);
                    lastHighlight = hitInfo.transform.gameObject;
                }
            }
            else if(lastHighlight != null)
            {
                lastHighlight.GetComponent<Hitbox>().ChangeCol(normalColor);
                lastHighlight = null;
            }
        }
    }
}
//Vector3 spawnPosition = new Vector3(Mathf.RoundToInt((hitInfo.point.x)/sBSize.x)*sBSize.x,Mathf.RoundToInt((hitInfo.point.y + hitInfo.normal.y/2)/sBSize.y)*sBSize.y,Mathf.RoundToInt((hitInfo.point.z + hitInfo.normal.z/2)/sBSize.z)*sBSize.z);
                //Vector3 spawnPosition = new Vector3(Mathf.RoundToInt((hitInfo.point.x)/sBSize.x  + hitInfo.normal.x/2) * sBSize.x, Mathf.RoundToInt((hitInfo.point.y)/sBSize.y  + hitInfo.normal.y)  * sBSize.y, Mathf.RoundToInt((hitInfo.point.z)/sBSize.z  + hitInfo.normal.z/2)  * sBSize.z);
                //Vector3 spawnPosition = new Vector3(Mathf.RoundToInt((hitInfo.point.x)/sBSize.x)*sBSize.x,Mathf.RoundToInt((hitInfo.point.y + hitInfo.normal.y/2)/sBSize.y)*sBSize.y,Mathf.RoundToInt((hitInfo.point.z + hitInfo.normal.z/2)/sBSize.z)*sBSize.z);
               //Vector3 spawnPosition = new Vector3(Mathf.RoundToInt((pos.x + normal.x/2)/hitSize.x)*hitSize.x, Mathf.RoundToInt((pos.y + normal.y/2)/hitSize.y)*hitSize.y, Mathf.RoundToInt((pos.z + normal.z/2)/hitSize.z)*hitSize.z);
                //Vector3 spawnPosition = new Vector3(Mathf.RoundToInt((hitInfo.point.x)/sBSize.x) * sBSize.x + Mathf.RoundToInt(hitInfo.normal.x/2/hitSize.x)*hitSize.x,Mathf.RoundToInt((hitInfo.point.y)/sBSize.y) * sBSize.y + Mathf.RoundToInt(hitInfo.normal.y/2/hitSize.y)*hitSize.y,Mathf.RoundToInt((hitInfo.point.z)/sBSize.z) * sBSize.z + Mathf.RoundToInt(hitInfo.normal.z/2/hitSize.z)*hitSize.z);

                //Vector3 spawnPosition = new Vector3(Mathf.RoundToInt((hitInfo.point.x)/sBSize.x) * sBSize.x + hitInfo.normal.x/2, Mathf.RoundToInt((hitInfo.point.y)/sBSize.y)  * sBSize.y + hitInfo.normal.y/2, Mathf.RoundToInt((hitInfo.point.z)/sBSize.z)  * sBSize.z + hitInfo.normal.x/2);
                //Vector3 spawnPosition = new Vector3((pos.x+normal.x/2)/sBSize.x,(pos.y+normal.y/2)/sBSize.y,(pos.z+normal.z/2)/sBSize.z);
                //spawnPosition = new Vector3(Mathf.RoundToInt(spawnPosition.x)*sBSize.x/2,Mathf.RoundToInt(spawnPosition.y)*sBSize.y/2,Mathf.RoundToInt(spawnPosition.z)*sBSize.z/2);
                //Vector3 spawnPosition = new Vector3(Mathf.RoundToInt((hitInfo.point.x)/sBSize.x  + hitInfo.normal.x/2) * sBSize.x, Mathf.RoundToInt((hitInfo.point.y)/sBSize.y  + hitInfo.normal.y/2)  * sBSize.y, Mathf.RoundToInt((hitInfo.point.z)/sBSize.z  + hitInfo.normal.z/2)  * sBSize.z);
               