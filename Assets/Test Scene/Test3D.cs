using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grids;

public class Test3D : MonoBehaviour
{
    [HideInInspector] public GridGenerator3D gg;

    void Start()
    {
        gg = ScriptableObject.CreateInstance<GridGenerator3D>();
        gg.GridRoot = transform;
        gg.m_GridNodeCreated.AddListener(OnNodeCreated);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gg.rows = Random.Range(5, 10);
            gg.cols = Random.Range(5, 10);
            gg.levels = Random.Range(5, 10);
            gg.rowDist = 1.2f;
            gg.colDist = 1.2f;
            gg.levelDist = 1.2f;
            gg.Generate();
        }
    }

    void OnNodeCreated(GridNodeInfo gi)
    {
        Transform node = gg.GetElementByIndex(gi.row, gi.col, gi.level);
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        go.transform.parent = node.transform;
        go.transform.localPosition = Vector3.zero;
    }
}
