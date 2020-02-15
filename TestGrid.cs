using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Disquiet.Core.SoundVisualization
{
    /// <summary>
    /// Represent a test grid, where each vertex has a gameobject for debugging purpose
    /// </summary>
    class TestGrid
    {
        /// <summary>
        /// The xyz dimension of a single cell in the grid
        /// </summary>
        public Vector3 CellDimension { get; private set; }

        /// <summary>
        /// Specifies how many cells along each direction
        /// </summary>
        public Vector3Int GridDimension { get; private set; }

        /// <summary>
        /// Positions for each vertex
        /// </summary>
        public List<Vector3> Vertex { get; private set; }

        public Vector3 center;

        public TestGrid(Vector3 cellDim, Vector3Int gridDim)
        {
            CellDimension = cellDim;
            GridDimension = gridDim;
            Vertex = InitializeVertexPosition();
        }

        public void Refresh(Vector3 center)
        {
            this.center = center;
            Vertex = InitializeVertexPosition();
            Debug.Log("Grid refreshed");
        }

        private List<Vector3> InitializeVertexPosition()
        {
            List<Vector3> vertexPosition = new List<Vector3>();
            for(int i = 0; i< GridDimension.x; i++)
            {
                for (int j= 0; j < GridDimension.y; j++)
                {
                    for (int k = 0; k < GridDimension.z; k++)
                    {
                        Vector3 vert = new Vector3(
                            i * CellDimension.x,
                            j * CellDimension.y,
                            k * CellDimension.z
                            ) ;
                        vert += center;
                        Debug.Log("Grid vertex at " + vert.ToString());
                        vertexPosition.Add(vert);
                    }
                }
            }
            return vertexPosition;
        }
    }
}
