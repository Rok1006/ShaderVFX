using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is for all effect
public class Ability_Garna : MonoBehaviour
{
    [Header("Ability_Stinger")]
    [SerializeField] private KeyCode input_Stinger;
    [SerializeField] private GameObject needlePack; //needle prefab
    [SerializeField] private GameObject StingerEmitPt;
    public List<GameObject> needle = new List<GameObject>();
    //[SerializeField] private GameObject[] needle;
    public GameObject stingerHitEffect; //enemy get hit effect
    [SerializeField] private GameObject effectPt;
    public List<GameObject> HitEffect = new List<GameObject>();
    [SerializeField] private GameObject pedalBurst;
    [SerializeField] private GameObject sparkRing;
    [SerializeField] private GameObject stingRemain;

    [Header("Ability_AmberDust")]
    [SerializeField] private KeyCode input_AmberDust;
    [SerializeField] private GameObject amberDust;
    void Start()
    {
    //   needle[0] = needlePack.transform.GetChild(0).gameObject;  
    //   needle[1] = needlePack.transform.GetChild(1).gameObject; 
    //   needle[2] = needlePack.transform.GetChild(2).gameObject;   
    //   needle[0].SetActive(true);
    //   needle[1].SetActive(true);
    //   needle[2].SetActive(true);
        //stingerHitEffect.SetActive(false);
        pedalBurst.SetActive(false);
        amberDust.SetActive(false);
        sparkRing.SetActive(false);
        stingRemain.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(input_Stinger)){
            StartCoroutine("Stinger"); 
        }
        if(Input.GetKeyDown(input_AmberDust)){
            StartCoroutine("AmberDust"); 
        }

        if(needle.Count>0&&needle[0]!=null&&needle[0].GetComponent<Stinger>().arrived){
            Debug.Log("penetrate");
            // stingerHitEffect.transform.position = effectPt.transform.position;
            // stingerHitEffect.SetActive(true);
            GameObject e = Instantiate(stingerHitEffect, effectPt.transform.position, Quaternion.identity);
            pedalBurst.SetActive(true);
            stingRemain.SetActive(true);
            HitEffect.Add(e);
            needle[0].GetComponent<Stinger>().arrived = false;
        }
    }

    IEnumerator Stinger(){
        GameObject s = Instantiate(needlePack, StingerEmitPt.transform.position, Quaternion.identity);
        needle.Add(s.transform.GetChild(0).gameObject);  
        needle.Add(s.transform.GetChild(1).gameObject); 
        needle.Add(s.transform.GetChild(2).gameObject);   
        yield return new WaitForSeconds(3f); //wait for animation to finish
        s.GetComponent<Animator>().enabled = false;
        sparkRing.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        
        needle[0].GetComponent<Stinger>().move = true;
        // yield return new WaitForSeconds(1f);
        needle[1].GetComponent<Stinger>().move = true;
        // yield return new WaitForSeconds(1f);
        needle[2].GetComponent<Stinger>().move = true;
        yield return new WaitForSeconds(.1f);
        s.SetActive(false);
        yield return new WaitForSeconds(2f);
        
        Destroy(HitEffect[0]);
        HitEffect.TrimExcess();
        HitEffect.Clear();
        needle.TrimExcess();
        needle.Clear();
        Destroy(s);
        pedalBurst.SetActive(false);
        sparkRing.SetActive(false);
        stingRemain.SetActive(false);
         //s.shoot out in order
    }
    IEnumerator AmberDust(){
        yield return new WaitForSeconds(0f);
        amberDust.SetActive(true);
        yield return new WaitForSeconds(15f);
        amberDust.SetActive(false);
    }
    IEnumerator DashOfVengence(){
        yield return new WaitForSeconds(0f);
    }
    IEnumerator BloomingGold(){
        yield return new WaitForSeconds(0f);
    }

    // public void Project(GameObject i){

    // }
}
