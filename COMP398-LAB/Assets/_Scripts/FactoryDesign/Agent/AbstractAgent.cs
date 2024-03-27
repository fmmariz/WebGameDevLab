using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractAgent : MonoBehaviour, IAgent
{
    public NavMeshAgent _myself;
    public Vector3 _destination;
    public bool _isJobComplete;

    public abstract void CompleteJob();

    public abstract void Navigate(Transform location);
}
