using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject DV_HitEffect_2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag==("Projectile")){
            GameObject g = col.gameObject.transform.parent.gameObject;
            g.SetActive(false);
            DV_HitEffect_2.SetActive(true);
        }
    }
}
