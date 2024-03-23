using UnityEngine;
public abstract class AbstractLocationFactory : MonoBehaviour
{
    [SerializeField] protected GameObject _prefab;
    public abstract ILocation CreateLocation();

}
