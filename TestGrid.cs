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
        public Vector3[,,] Vertex { get; private set; }

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

        private Vector3[,,] InitializeVertexPosition()
        {
            Vector3[,,] vertexPosition = new Vector3[GridDimension.x, GridDimension.y, GridDimension.z];
            for(int x = 0; x< GridDimension.x; x++)
            {
                for (int y= 0; y < GridDimension.y; y++)
                {
                    for (int z = 0; z < GridDimension.z; z++)
                    {
                        Vector3 vert = new Vector3(
                            x * CellDimension.x,
                            y * CellDimension.y,
                            z * CellDimension.z
                            ) ;
                        vert += center;
                        vertexPosition[x, y, z] = vert;
                    }
                }
            }
            return vertexPosition;
        }
    }
}
