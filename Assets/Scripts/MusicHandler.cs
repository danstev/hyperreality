﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioSource music;
    
    // Start is called before the first frame update
    void Start()
    {
        music.Play(0);
    }

}
