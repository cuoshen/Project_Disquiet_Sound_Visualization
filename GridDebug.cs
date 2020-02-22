using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Disquiet.Core.SoundVisualization
{
    class GridDebug : MonoBehaviour
    {
        private Renderer renderer;
        private GridSoundSampler sampler;
        public GameObject SamplerGO;

        void Start()
        {
            renderer = gameObject.GetComponent<Renderer>();
            sampler = SamplerGO.GetComponent<GridSoundSampler>();
        }

        void Update()
        {
            renderer.material.SetTexture("_MainTex", sampler.SoundTexture);
        }
    }
}
