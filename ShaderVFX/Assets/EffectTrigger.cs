using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//For Calling effects
public class EffectTrigger : MonoBehaviour
{

    [SerializeField] private GameObject AGObj;
    private Ability_Garna AG;

    void Awake() {
       AGObj = GameObject.FindGameObjectWithTag("Player");
       AG = AGObj.GetComponent<Ability_Garna>();
    }
    void Start()
    {
        AG.slashV1.SetActive(false);
        AG.DV_HitEffect.SetActive(false);
        AG.slashV2_2.SetActive(false);
        // if(AG.Flower.Count>0){
        //     AG.Flower[0].transform.GetChild(1).gameObject.SetActive(false);
        //     AG.Flower[1].transform.GetChild(1).gameObject.SetActive(false);
        //     AG.Flower[2].transform.GetChild(1).gameObject.SetActive(false);
        //     AG.Flower[3].transform.GetChild(1).gameObject.SetActive(false);
        // }
    }
    public void SlashAppear(){
        AG.slashV1.SetActive(true);
    }
    public void DOV_VFX(){
        AG.DV_HitEffect.SetActive(true);
    }
    public void DOV_V2(){
        AG.slashOut = true;
        AG.slashV2_2.SetActive(true);
    }
    public void EmitPollen(){
        // if(AG.Flower.Count>3){
        // AG.Flower[0].transform.GetChild(1).gameObject.SetActive(true);
        // AG.Flower[1].transform.GetChild(1).gameObject.SetActive(true);
        // AG.Flower[2].transform.GetChild(1).gameObject.SetActive(true);
        // AG.Flower[3].transform.GetChild(1).gameObject.SetActive(true);
        // }
    }
}
