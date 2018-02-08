using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grids;

public class Test2D : MonoBehaviour
{
    [HideInInspector] public GridGenerator2D gg;
    void Start ()
    {
        gg = ScriptableObject.CreateInstance<GridGenerator2D>();
        gg.GridRoot =transform;
        gg.m_GridNodeCreated.AddListener(OnNodeCreated);
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gg.rows = Random.Range(5, 10);
            gg.cols = Random.Range(5, 10);
            gg.rowDist = Random.Range(1.5f, 4.0f);
            gg.colDist = Random.Range(1.5f, 4.0f);
            gg.Generate();
        }
	}

    void OnNodeCreated(GridNodeInfo gi)
    {
        Transform node = gg.GetElementByIndex(gi.row, gi.col, gi.level);
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.localScale = Vector3.one * 1.5f;
        go.transform.parent = node.transform;
        go.transform.localPosition = Vector3.zero; 
    }
}
