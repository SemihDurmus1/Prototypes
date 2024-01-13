using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] int minDamage, maxDamage;
    [SerializeField] Camera playerCam;
    [SerializeField] float range = 300f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject kivilcim;
    [SerializeField] GameObject mermiDeligi;

    private EnemyManager _enemyManager;
    void Start()
    {
        
    }



    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
            muzzleFlash.Play();
        }
    }

    void Fire()
    {
        RaycastHit hit;

        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            _enemyManager = hit.transform.GetComponent<EnemyManager>();

            MermiEfekti(hit);
            if (_enemyManager != null)
            {
                _enemyManager.EnemyTakeDamage(Random.Range(minDamage, maxDamage));
            }
        }
    }

    void MermiEfekti(RaycastHit hit)
    {
        Instantiate(kivilcim, hit.point, Quaternion.LookRotation(hit.normal)); //Mermi efekti
        Instantiate(mermiDeligi, hit.point, Quaternion.LookRotation(hit.normal)); //Mermi delði efekti
    }
}
