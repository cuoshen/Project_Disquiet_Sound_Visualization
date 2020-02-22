using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Disquiet.Core.SoundVisualization
{
    class GridSoundSampler : MonoBehaviour
    {
        // The main goal is to refresh vertexSamples on every Update()
        // We will figure a way to ship the data in vertexSamples into the shader


        /// <summary>
        /// Sound sampler used to evaluate intensity value at sample points
        /// </summary>
        private SoundSampler soundSampler;
        public GameObject SoundSamplerObj;

        // In a later build, repace TestGrid with another Grid type
        // TestGrid is for debugging purposes now
        public TestGrid SampleGrid { get; private set; }
        public Vector3 GridCellSize = new Vector3(1, 1, 1);
        public Vector3Int GridDimension = new Vector3Int(10, 10, 10);

        /// <summary>
        /// Sound intensity rendered to 3D texture with respect to world position
        /// </summary>
        public Texture3D SoundTexture { get; private set; }

        public GameObject Player;

        private struct VertexSample
        {
            public Vector3 Vertex;
            public float Intensity;
        }
        private List<VertexSample> vertexSamples;

        private void Start()
        {
            soundSampler = SoundSamplerObj.GetComponent<SoundSampler>();
            SampleGrid = new TestGrid(GridCellSize, GridDimension);
            vertexSamples = new List<VertexSample>();
        }

        private void Update()
        {
            // For debug purposes, use the vector (0,0,0)
            SampleGrid.Refresh(Vector3.zero);
            //SampleGrid.Refresh(Player.transform.position);

            // Refresh vertexSamples
            vertexSamples = new List<VertexSample>();
            foreach(Vector3 vert in SampleGrid.Vertex)
            {
                VertexSample sample = new VertexSample();
                sample.Vertex = vert;
                sample.Intensity = soundSampler.Evaluate(vert);
                vertexSamples.Add(sample);
            }

            // Render to texture
            SoundTexture = RenderToBlueDebugTexture();
        }

        private Texture3D RenderSoundToTexture()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// A debug function to test 3D texture generation
        /// Delete when obsolete
        /// </summary>
        private Texture3D RenderToBlueDebugTexture()
        {
            Texture3D tex = new Texture3D(256, 256, 256, TextureFormat.RGBA32, true );
            for(int x = 0; x< tex.width; x++)
            {
                for(int y = 0; y< tex.height; y++)
                {
                    for(int z = 0; z < tex.depth; z++)
                    {
                        tex.SetPixel(x, y, z, Color.blue);
                    }
                }
            }
            tex.Apply();
            return tex;
        }
    }
}
