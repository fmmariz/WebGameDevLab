using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgent 
{
    void Navigate(Transform location);
    void CompleteJob();
}
