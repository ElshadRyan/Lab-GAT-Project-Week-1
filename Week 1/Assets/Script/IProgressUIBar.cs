using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IProgressUIBar
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChange;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float ProgressNormalized;
    }
}
