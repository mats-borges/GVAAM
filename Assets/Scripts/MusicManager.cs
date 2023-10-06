using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : MonoBehaviour
{
    [SerializeField] public List<AudioClip> MusicList = new List<AudioClip>();
    [SerializeField] public List<string> MusicNameList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        //make 2 lists one for titles and one for actual song files
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
