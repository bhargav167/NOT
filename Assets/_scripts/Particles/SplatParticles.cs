using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatParticles : MonoBehaviour
{
    public ParticleSystem splatParticle;
    public GameObject splatPrefab;
    public Transform _splatHolder;
    private List<ParticleCollisionEvent> collisionEvents=new List<ParticleCollisionEvent>();

    private void OnParticleCollision(GameObject other){
        ParticlePhysicsExtensions.GetCollisionEvents(splatParticle,other,collisionEvents);
        int count=collisionEvents.Count;
        for (int i = 0; i < count; i++){
            GameObject splat = Instantiate(splatPrefab, collisionEvents[i].intersection, Quaternion.identity) as GameObject;
            splat.transform.SetParent(_splatHolder, true);
            Splat splatScript = splat.GetComponent<Splat>();
            splatScript.Initilize(Splat.SplatLocation.Foreground);
        }
    }
}
