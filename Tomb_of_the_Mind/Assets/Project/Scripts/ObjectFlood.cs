using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

[System.Serializable]
public class ObjectFlood : MonoBehaviour
{
    [SerializeField]
    private string bundleName; // used for storing the objects under a single parent
    [SerializeField]
    private List<GameObject> o; // objects to flood fill
    [SerializeField]
    private float scaleFator;

    [SerializeField]
    private Vector2 bl; // bottom left
    [SerializeField]
    private Vector2 tr; // top right

    [SerializeField]
    private Vector2 stepSize; // how much should the step be when determining wether to place object or not
    [SerializeField]
    [Range(0,1)]
    private float chanceForSpawm;

    [SerializeField]
    private bool variableStepSize; // if the step size should be between 0 and stepSize

    [SerializeField]
    private float editorSphereScale = 1.0f;


    public LayerMask mask;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s">start position</param>
    /// <param name="rd">ray direction</param>
    /// <returns></returns>
    private Vector3 GetPlacementPosition(Vector3 s, Vector3 rd)
    {
        RaycastHit raycastHit;
        bool hit = Physics.Raycast(s, rd, out raycastHit, Mathf.Infinity, mask);
        Debug.Log(raycastHit.collider);
        if (raycastHit.collider == null)
        {
            return Vector3.negativeInfinity;
        }
        
        return raycastHit.point;
        

    }
    public void Flood()
    {
        Vector2 bl = this.bl + new Vector2(transform.position.x, transform.position.z);
        Vector2 tr = this.tr + new Vector2(transform.position.x, transform.position.z);

        Debug.Log(bl);
        Debug.Log(tr);


        if (o == null)
        {
            Debug.LogError("PREFAB IS EMPTY!");
            return;
        }
        bool e = transform.Find(bundleName) != null;
        if (e) // if we already have a flood with this name, we delete it before creating a new one
            DestroyImmediate(transform.Find(bundleName).gameObject);


        GameObject p = new GameObject();
        p.name = bundleName;
        p.transform.SetParent(gameObject.transform);  // adding script object as parent
        p.transform.localScale = Vector3.one;


        Vector2 cp = bl; // current position
        float xp = bl.x; // x position

        while (cp.y <= tr.y)
        {
            while(cp.x <= tr.x)
            {
                int chance = Random.Range(0,10000);
                if(chance >= chanceForSpawm * 10000)
                {
                    cp += Vector2.right*stepSize.x; // step to the right
                    continue;
                }            

                Vector3 pos = GetPlacementPosition(new Vector3(cp.x, gameObject.transform.position.z, cp.y), Vector3.down);
                print(pos);
                if(pos.x == Mathf.NegativeInfinity)
                {
                    cp += Vector2.right * stepSize.x; // step to the right
                    continue;
                }

                int objInd = Random.Range(0, o.Count);
                GameObject i = Instantiate(o[objInd], pos, Quaternion.identity, p.transform);

                i.transform.localScale = Vector3.one * scaleFator;
                i.transform.Rotate(Vector3.up * Random.Range(0, 360));
                i.layer = 7;

                foreach (Transform child in i.transform)
                {
                    child.gameObject.layer = 7;
                }
                



                cp += Vector2.right * stepSize.x; // step to the right

            }
            cp += Vector2.up * stepSize.y; // step up

            cp -= Vector2.right * (cp.x - xp); // step left to beginning
            
        }

    }

    private void OnDrawGizmos()
    {
        Vector2 bl = this.bl + new Vector2(transform.position.x, transform.position.z);
        Vector2 tr = this.tr + new Vector2(transform.position.x, transform.position.z);

        Gizmos.DrawWireSphere(new Vector3(bl.x, transform.position.y, bl.y), editorSphereScale);
        Gizmos.DrawWireSphere(new Vector3(tr.x, transform.position.y, tr.y), editorSphereScale);

    }
}
