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
        public Vector3 GridCellSize;
        public Vector3Int GridDimension;

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
            SampleGrid.Refresh(Player.transform.position);

            // Refresh vertexSamples
            vertexSamples = new List<VertexSample>();
            foreach(Vector3 vert in SampleGrid.Vertex)
            {
                VertexSample sample = new VertexSample();
                sample.Vertex = vert;
                sample.Intensity = soundSampler.Evaluate(vert);
                vertexSamples.Add(sample);
            }
        }
    }
}
