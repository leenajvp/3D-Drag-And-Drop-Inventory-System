using System;
using UnityEngine;

public interface IHelmet
{
    Sprite Image { get; }
    bool isAvailable { get; set; }
    void Collect();
    void Drop();
}