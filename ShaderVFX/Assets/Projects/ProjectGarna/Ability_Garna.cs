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

    [Header("Ability_DashOfVengenance")]
    int state = 0;
    [SerializeField] private KeyCode input_DashOVen;
    [SerializeField] private GameObject origin;
    [SerializeField] private GameObject midPt;
    [SerializeField] private GameObject attackPt;
    [SerializeField] private GameObject lanceSwingObj;
    [SerializeField] private GameObject lance;
    [SerializeField] private GameObject slashV1;
    [SerializeField] private GameObject slashV2;
    [SerializeField] private GameObject slashV2_2;
    [SerializeField] private GameObject DV_HitEffect;
    Animator lanceAnim;
    private GameObject target;
    [SerializeField] private int speed;
    [SerializeField] private int V2_speed;
    bool moveForward = false;
    public bool slashOut = false;
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
        slashV1.SetActive(false);
        slashV2.SetActive(false);
        slashV2_2.SetActive(false);
        //DV_HitEffect.SetActive(false);

        lanceAnim = lance.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(input_Stinger)){
            StartCoroutine("Stinger"); 
        }
        if(Input.GetKeyDown(input_AmberDust)){
            StartCoroutine("AmberDust"); 
        }
        if(Input.GetKeyDown(input_DashOVen)){
            if(state==0){
                StartCoroutine("DashOfVengence_V1");  
            }else{
                StartCoroutine("DashOfVengence_V2"); 
            }
            
        }
//-----------------------
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
        if(moveForward){
            Move();
            if(this.transform.position == target.transform.position){
                moveForward = false;
            }
        }
        if(slashOut){
            SlashOut();
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
    IEnumerator DashOfVengence_V1(){
        lanceSwingObj.transform.rotation = Quaternion.Euler(-17.863f, 0f, 0f);
        yield return new WaitForSeconds(0f);
        target = attackPt;
        moveForward = true;
        //target = origin;
        yield return new WaitForSeconds(.3f);
        // if(this.transform.position == target.transform.position){
        lanceAnim.SetTrigger("Swing");
        yield return new WaitForSeconds(1f);
        state = 1;
        // }
    }
    IEnumerator DashOfVengence_V2(){
        lanceSwingObj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        yield return new WaitForSeconds(0f);
        target = midPt;
        moveForward = true;
        yield return new WaitForSeconds(.05f);
        lanceAnim.SetTrigger("BackSwing");
        slashV2.SetActive(true);
        yield return new WaitForSeconds(1f);
        ResetDashOfVengence();
    }
    void ResetDashOfVengence(){
        target=origin;
        state = 0;
        moveForward = true;
        slashOut = false;
        lanceAnim.SetTrigger("Reset");
        slashV1.SetActive(false);
        slashV2.SetActive(false);
        slashV2_2.SetActive(false);
        DV_HitEffect.SetActive(false);
        slashV2.transform.position = this.transform.position;
    }
    void Move(){
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, step);
    }
    void SlashOut(){
        slashV2.transform.Translate(Vector3.left * V2_speed * Time.deltaTime);
    }
    
    IEnumerator BloomingGold(){
        yield return new WaitForSeconds(0f);
    }

    

    // public void Project(GameObject i){

    // }
}
