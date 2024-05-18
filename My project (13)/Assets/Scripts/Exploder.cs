using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{  
    [SerializeField] private Cube _prefab;

    private void OnEnable()
    {
        _prefab.NotDivided += Explode;
    }

    private void OnDisable()
    {
        _prefab.NotDivided -= Explode;
    }



    public void Explode(Cube cube)
    {
        foreach (Rigidbody explorableObject in GetExplorableObjects(cube))  
        {
            explorableObject.AddExplosionForce(cube.GetExplodeForce(), cube.transform.position, cube.GetExplodeRadius());
        }
    }

    private List<Rigidbody> GetExplorableObjects(Cube cube)
    {
        Collider[] hits = Physics.OverlapSphere(cube.transform.position, cube.GetExplodeRadius());

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }
}
