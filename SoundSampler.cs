using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSampler : MonoBehaviour
{
    // keeps track of all audio sources in the scene

    /// <summary>
    /// List of all audio sources in the scene
    /// </summary>
    public List<AudioSource> Audios { get; private set; } = new List<AudioSource>();

    private const int sampleDataLength = 1024;
    private float[] sampleData;

    private void Start()
    {
        Audios = GetAllAudioSourceInScene();
    }

    /// <summary>
    /// Evaluate the combined sound intensity at given point in the scene
    /// </summary>
    public float Evaluate(Vector3 samplePt)
    {
        float totalIntensity = 0.0f;
        foreach(AudioSource ads in Audios)
        {
            if(ads.isPlaying == true)
            {
                float distance = Vector3.Distance(samplePt, ads.transform.position);
                if(distance <= ads.maxDistance) // Not exceeding the maximum distance of the audio source
                {
                    // Get the intensity of that sound source at this point
                    float intensity = SampleIntensity(ads, distance);
                    totalIntensity += intensity;
                }
            }
        }
        return totalIntensity;
    }

    /// <summary>
    /// Sample the loudness of a single AudioSource at a certain distance
    /// </summary>
    private float SampleIntensity(AudioSource audio, float distance)
    {
        sampleData = new float[sampleDataLength];
        if(audio.clip == null)
        {
            return 0.0f;
        }
        else
        {
            audio.clip.GetData(sampleData, audio.timeSamples);
            float clipIntensity = 0.0f;
            foreach(var sample in sampleData)
            {
                clipIntensity += Mathf.Abs( sample);
            }
            // We need to apply attenuation
            throw new System.NotImplementedException();
            clipIntensity /= sampleDataLength;
            return clipIntensity;
        }
    }

    private List<AudioSource> GetAllAudioSourceInScene()
    {
        List<AudioSource> audios = new List<AudioSource>();
        foreach(AudioSource ads in Resources.FindObjectsOfTypeAll(typeof(AudioSource)))
        {
            audios.Add(ads);
        }
        return audios;
    }
}
