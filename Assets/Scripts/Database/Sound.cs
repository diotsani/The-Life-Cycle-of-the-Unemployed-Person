using UnityEngine;

namespace Team8.Unemployment.Database
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1f;
    
        [Range(0.3f, 3f)]
        public float pitch = 1f;
    }
}

