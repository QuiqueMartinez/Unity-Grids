using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Grids
{
    [System.Serializable]
    public class GridNodeCreated : UnityEvent<GridNodeInfo>
    {
    }

    [System.Serializable]
    public class GridNodeDestroyed : UnityEvent<GridNodeInfo>
    {
    }

    public struct GridNodeInfo
    {
        public Transform transform;
        public int row;
        public int col;
        public int level;

        public GridNodeInfo(Transform _transform, int _row, int _col, int _level)
        {
            transform = _transform;
            row = _row;
            col = _col;
            level = _level;
        }
    }

    public abstract class GridGenerator : ScriptableObject
    {
        protected int _rows = 1 ;
        protected int _cols = 1;
        protected int _levels = 1;
        protected Vector3 _elementsDistance = Vector3.one;

        public GridNodeCreated m_GridNodeCreated;
        public Transform[,,] grid;
        public Transform GridRoot;

        private void Awake()
        {
            m_GridNodeCreated = new GridNodeCreated();
        }

        public IEnumerable<GridNodeInfo> GetNextItem()
        {
            for (int x = 0; x < grid.GetLength(0); x += 1)
            {
                for (int y = 0; y < grid.GetLength(1); y += 1)
                {
                    for (int z = 0; z < grid.GetLength(2); z += 1)
                    {
                       yield return new GridNodeInfo (grid[x, y, z], x,y,z);
                    }
                }
            }
        }

        public abstract void Generate();

        protected void InitGrid()
        {
            ResetGrid();
            grid = new Transform[_rows, _cols, _levels];
            foreach (GridNodeInfo gi in GetNextItem())
            {
                GameObject go = new GameObject();
                go.name = "Grid_" +  gi.row+ "_" + gi.col + "_" + gi.level;
                if (GridRoot != null) go.transform.parent = GridRoot;
                go.transform.localPosition = new Vector3(gi.row * _elementsDistance.x, gi.col * _elementsDistance.y, gi.level * _elementsDistance.z);
                grid[gi.row, gi.col, gi.level] = go.transform;
                m_GridNodeCreated.Invoke(gi);
            }
        }

        public Transform GetElementByIndex(int row, int col, int level)
        {
            return grid[row, col, level];
        }

        void ResetGrid()
        {
            if (grid != null)
            {
                foreach (GridNodeInfo gi in GetNextItem())
                {
                    Destroy(gi.transform.gameObject);
                }
                grid = null;
            }
        }
    }

    [CreateAssetMenu(menuName = "Create 2D Grid", fileName ="My 2D Grid", order =0)]
    public class GridGenerator2D : GridGenerator
    {
        public int rows;
        public int cols;
        public float rowDist;
        public float colDist;

        public override void Generate()
        {
            _rows = rows;
            _cols = cols;
            _levels = 1;

            _elementsDistance = new Vector3(rowDist, colDist, 0);

            InitGrid();
        }
    }

    [CreateAssetMenu(menuName = "Create 3D Grid", fileName = "My 3D Grid", order = 0)]
    public class GridGenerator3D : GridGenerator
    {
        public int rows;
        public int cols;
        public int levels;

        public float rowDist;
        public float colDist;
        public float levelDist;

        public override void Generate()
        {
            _rows = rows;
            _cols = cols;
            _levels = levels;

            _elementsDistance = new Vector3(rowDist, colDist, levelDist);

            InitGrid();
        }
    }
}
