using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Disquiet.Core.SoundVisualization 
{
    public class PointDebug : MonoBehaviour
    {
        private Renderer renderer;
        private SoundSampler sampler;
        public GameObject SamplerGO;

        void Start()
        {
            renderer = gameObject.GetComponent<Renderer>();
            sampler = SamplerGO.GetComponent<SoundSampler>();
        }

        // Update is called once per frame
        void Update()
        {
            float intensity = sampler.Evaluate(transform.position);
            Debug.Log("Intensity at " + intensity);
            Color soundColor = new Color(intensity, intensity, intensity, 1);
            soundColor *= 10;
            renderer.material.color = soundColor;
        }
    }
}