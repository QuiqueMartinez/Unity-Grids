using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
   // public Grids.GridGenerator2D gg;
    [HideInInspector] public Grids.GridGenerator2D gg2;
    // Use this for initialization
    void Start ()
    {
       /* Grid g = new Grid();
        g.cellLayout = GridLayout.CellLayout.Rectangle;
        g.cellSize = new Vector3(2,2,2);
        g.cellGap = new Vector3(1, 1, 1);
        //g.

        // gg2 = ScriptableObject.Instantiate(Grids.GridGenerator2D);
        // gg2.rows = 10;
        for (int i = 0; i < g.cellSize.x; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        }*/
     
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
           {
         //   gg2.debug();
         //   Debug.Log(gg2.rows);
        }
	}
}
