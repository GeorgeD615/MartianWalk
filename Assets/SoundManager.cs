using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _music;

    public void MusicEnable(bool value)
    {
        _music.enabled = value;
    }


}
