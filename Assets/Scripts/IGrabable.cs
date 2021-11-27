using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

interface IGrabable
{ 
    bool IsGrabbed { get; set; }
    void Grab(GameObject grabber);
    void Release();
}