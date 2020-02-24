using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Disquiet.Core.SoundVisualization
{
    class GridSoundSampler : MonoBehaviour
    {
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
        public Vector3 GridTotalSize { get; private set; }

        /// <summary>
        /// Sound intensity rendered to 3D texture with respect to world position
        /// </summary>
        public Texture3D SoundTexture;

        public GameObject Player;

        private struct VertexSample
        {
            public Vector3 Vertex;
            public float Intensity;
        }
        private VertexSample[,,] vertexSamples;

        private void Start()
        {
            VerifyGrid();
            soundSampler = SoundSamplerObj.GetComponent<SoundSampler>();
            SampleGrid = new TestGrid(GridCellSize, GridDimension);
            GridTotalSize = new Vector3(
                GridCellSize.x * (GridDimension.x - 1),
                GridCellSize.y * (GridDimension.y - 1),
                GridCellSize.z * (GridDimension.z - 1));
        }

        private void Update()
        {
            // For debug purposes, use the vector (0,0,0)
            SampleGrid.Refresh(Vector3.zero);
            //SampleGrid.Refresh(Player.transform.position);

            // Refresh vertexSamples
            vertexSamples = new VertexSample[GridDimension.x, GridDimension.y, GridDimension.z];
            for (int x = 0; x < GridDimension.x; x++)
            {
                for (int y = 0; y < GridDimension.y; y++)
                {
                    for (int z = 0; z < GridDimension.z; z++)
                    {
                        VertexSample vertexSample = new VertexSample();
                        vertexSample.Vertex = SampleGrid.Vertex[x, y, z];
                        vertexSample.Intensity = soundSampler.Evaluate(vertexSample.Vertex);
                        vertexSamples[x, y, z] = vertexSample;
                    }
                }
            }

            // Render to texture
            SoundTexture = RenderSoundToTexture();
            //SoundTexture = RenderDebugTexture();
        }

        /// <summary>
        /// Check if the GridCellSize or GridDimension contains a non-positive entry
        /// Program crashes loud and clear if that is the case
        /// </summary>
        private void VerifyGrid()
        {
            if(GridDimension.x <=0 || GridDimension.y <= 0 || GridDimension.z <= 0 || GridCellSize.x <= 0 || GridCellSize.y <= 0 || GridCellSize.z <= 0)
            {
                throw new System.Exception("Non-positive grid entry");
            }
        }

        /// <summary>
        /// Gather the information sampled form the grid and render it to a 3D Texture
        /// </summary>
        private Texture3D RenderSoundToTexture()
        {
            // Inline style method. Takes no parameter, use global variables instead.

            Texture3D soundTex3D = new Texture3D(GridDimension.x, GridDimension.y, GridDimension.z, TextureFormat.RGBA32, true);
            for(int x = 0; x < soundTex3D.width; x++)
            {
                for(int y = 0; y < soundTex3D.height; y++)
                {
                    for(int z = 0; z < soundTex3D.depth; z++)
                    {
                        float intensity = vertexSamples[x, y, z].Intensity;
                        intensity *= 10;
                        Color soundColor = new Color(intensity, intensity, intensity,1);
                        soundTex3D.SetPixel(x, y, z, soundColor);
                    }
                }
            }
            soundTex3D.Apply();
            return soundTex3D;
        }

        private Texture3D RenderDebugTexture()
        {
            Texture3D debugTex3D = new Texture3D(GridDimension.x, GridDimension.y, GridDimension.z, TextureFormat.RGBA32, true);
            for (int x = 0; x < debugTex3D.width; x++)
            {
                for (int y = 0; y < debugTex3D.height; y++)
                {
                    for (int z = 0; z < debugTex3D.depth; z++)
                    {
                        if(x % 2 == 0)
                        {
                            debugTex3D.SetPixel(x, y, z, Color.black);
                        }
                        else
                        {
                            debugTex3D.SetPixel(x, y, z, Color.white);
                        }
                    }
                }
            }
            debugTex3D.Apply();
            return debugTex3D;
        }
    }
}
