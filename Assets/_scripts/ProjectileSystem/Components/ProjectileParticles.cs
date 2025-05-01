using System.Linq;
using UnityEngine;

namespace Tero.ProjectileSystem.Components
{
    public enum SplatIntractedTags
    {
        Police,
        Player,
        Floor,
        Ground,
        Wall
    }
    public class ProjectileParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] impactParticles;
        public GameObject splatPrefab;
        private GameObject NormalPolicebodySprite;
        private GameObject ArmedPolicebodySprite;
        private GameObject FemalePolicebodySprite;
        private GameObject PlayerbodySprite;
        private bool IsbulletIntracthuman = false; // check weather bullet intract with wall/Ground or Person

        private string Object_With_PoliceName = "/Base Bone/Chest Bone";
        private string Object_With_normalPoliceName = "/BaseBone/body_2";
        private string Object_With_PlayerName = "/Base Bone/Chest Bone";
        private string Object_With_FemalePlayerName = "/pelvisbone/Base Bone/Chest Bone";
        public void SpawnImpactParticles(Vector3 position, Quaternion rotation,bool tag)
        {
            if (!tag)
            {
                Instantiate(impactParticles[0], position, rotation);
            }
            else
            {
                Instantiate(impactParticles[1], new Vector2(Random.Range(position.x + 0.1f, position.x + 0.5f),position.y), rotation);
            }
        }

        public void SpawnImpactParticles(RaycastHit2D hit,bool tag)
        {
            var rotation = Quaternion.FromToRotation(transform.right, hit.normal);
            SpawnImpactParticles(hit.point, rotation,tag);
        }

        public void SpawnImpactParticles(RaycastHit2D[] hits)
        {
            if(hits.Length <= 0 )
                return;

            if (hits[0].transform.CompareTag(SplatIntractedTags.Police.ToString()) || hits[0].transform.CompareTag(SplatIntractedTags.Player.ToString()))
                IsbulletIntracthuman = true;
            else
                IsbulletIntracthuman = false;



            ArmedPolicebodySprite = GameObject.Find(hits[0].transform.name + Object_With_PoliceName);
            NormalPolicebodySprite = GameObject.Find(hits[0].transform.name + Object_With_normalPoliceName);
            FemalePolicebodySprite = GameObject.Find(hits[0].transform.name + Object_With_FemalePlayerName);

            PlayerbodySprite = GameObject.Find(hits[0].transform.name + Object_With_PlayerName);
           
            SpawnImpactParticles(hits[0], IsbulletIntracthuman);

            // Code for splat blood
            if (hits[0].transform.CompareTag(SplatIntractedTags.Floor.ToString()) || hits[0].transform.CompareTag(SplatIntractedTags.Wall.ToString())) return;

                GameObject splat = Instantiate(splatPrefab,new Vector2(Random.Range(hits[0].point.x+0.1f, hits[0].point.x + 0.5f), hits[0].point.y), Quaternion.identity) as GameObject;
       
            if(NormalPolicebodySprite != null)
                splat.transform.SetParent(NormalPolicebodySprite.transform, true);
            if (ArmedPolicebodySprite != null)
                splat.transform.SetParent(ArmedPolicebodySprite.transform, true);
            if (FemalePolicebodySprite != null)
                splat.transform.SetParent(FemalePolicebodySprite.transform, true);
            if (PlayerbodySprite != null)
                splat.transform.SetParent(PlayerbodySprite.transform, true);

                Splat splatScript = splat.GetComponent<Splat>();
                    if (hits[0].collider.gameObject.tag == SplatIntractedTags.Floor.ToString())
                        splatScript.Initilize(Splat.SplatLocation.Background);
                    else
                        splatScript.Initilize(Splat.SplatLocation.Foreground);
        }
    }

}