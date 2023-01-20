using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is for all effect
public class Ability_Garna : MonoBehaviour
{
    [SerializeField] private GameObject Lines_BK;
    [SerializeField] private GameObject Lines_FR;
    [Header("Ability_Stinger")]
    [SerializeField] private KeyCode input_Stinger;
    [SerializeField] private GameObject needlePack; //needle prefab
    [SerializeField] private GameObject StingerEmitPt;
    public List<GameObject> needle = new List<GameObject>();
    public GameObject stingerHitEffect; //enemy get hit effect
    [SerializeField] private GameObject effectPt;
    public List<GameObject> HitEffect = new List<GameObject>();
    [SerializeField] private GameObject sparkRing;
    [SerializeField] private GameObject ST_HitEffect;
    private GameObject target;
    [SerializeField] private int speed;

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
    public GameObject slashV1;
    [SerializeField] private GameObject slashV2;
    public GameObject slashV2_2;
    public GameObject DV_HitEffect;
    [SerializeField] private GameObject DV_HitEffect_2;
    Animator lanceAnim;
    [SerializeField] private int V2_speed;
    bool moveForward = false;
    public bool slashOut = false;

    [Header("Ability_BloomingGold_V1")]
    [SerializeField] private KeyCode input_BloomingGold_v1;
    [SerializeField] private GameObject flowerPrefab;
    [SerializeField] private GameObject smallFlowerPrefab;
    [SerializeField] private GameObject lancePrefab;
    [SerializeField] private GameObject groundFlower;
    [SerializeField] private int BG_speed;
    public GameObject[] Location;
    public bool[] ready;
    public List<GameObject> Flower = new List<GameObject>();

    void Start()
    {
        ST_HitEffect.SetActive(false);
        amberDust.SetActive(false);
        sparkRing.SetActive(false);
        slashV1.SetActive(false);
        slashV2.SetActive(false);
        slashV2_2.SetActive(false);
        DV_HitEffect_2.SetActive(false);
        Lines_BK.SetActive(false);
        Lines_FR.SetActive(false);
        groundFlower.SetActive(false);
        

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
                Lines_BK.SetActive(true);
            }else{
                StartCoroutine("DashOfVengence_V2");
                Lines_FR.SetActive(true);
            } 
        }
        if(Input.GetKeyDown(input_BloomingGold_v1)){
            StartCoroutine("BloomingGold"); 
        }
//-----------------------
        if(needle.Count>0&&needle[0]!=null&&needle[0].GetComponent<Stinger>().arrived){
            Debug.Log("penetrate");
            // stingerHitEffect.transform.position = effectPt.transform.position;
            // stingerHitEffect.SetActive(true);
            GameObject e = Instantiate(stingerHitEffect, effectPt.transform.position, Quaternion.identity);
            ST_HitEffect.SetActive(true);
            //stingRemain.SetActive(true);
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
//Ability - Stinger
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
        ST_HitEffect.SetActive(false);
        sparkRing.SetActive(false);
        //stingRemain.SetActive(false);
         //s.shoot out in order
    }
//Ability - AmberDust
    IEnumerator AmberDust(){
        yield return new WaitForSeconds(0f);
        amberDust.SetActive(true);
        yield return new WaitForSeconds(15f);
        amberDust.SetActive(false);
    }
//Ability - DashOfVengence
    IEnumerator DashOfVengence_V1(){
        lanceSwingObj.transform.rotation = Quaternion.Euler(-17.863f, 0f, 0f);
        yield return new WaitForSeconds(0f);
        target = attackPt;
        moveForward = true;
        yield return new WaitForSeconds(.3f);
        lanceAnim.SetTrigger("Swing");
        yield return new WaitForSeconds(1f);
        state = 1;
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
        DV_HitEffect_2.SetActive(false);
        slashV2.transform.position = this.transform.position;
        Lines_BK.SetActive(false);
        Lines_FR.SetActive(false);
    }
    void Move(){
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, step);
    }
    void SlashOut(){
        slashV2.transform.Translate(Vector3.left * V2_speed * Time.deltaTime);
    }
//Ability - Blooming Gold
    IEnumerator BloomingGold(){
        yield return new WaitForSeconds(0f);
        groundFlower.SetActive(true);
        GenerateSmallFlowers();
        RandomZ();

        GenerateFlowers(0,.7f);
        yield return new WaitForSeconds(.5f);
        GenerateFlowers(1,.8f);
        yield return new WaitForSeconds(.5f);
        GenerateFlowers(2,.9f);
        yield return new WaitForSeconds(.5f);
        GenerateFlowers(3,1);
        
    }
    void GenerateSmallFlowers(){
        for(int i = 0; i<10; i++){
            float ranS = Random.Range(0.05f, 0.3f);
            Vector3 location = new Vector3(Random.Range(-14, 14), 3.36f ,Random.Range(-4, 4));
            GameObject f0 = Instantiate(smallFlowerPrefab, location, Quaternion.identity);
            f0.transform.localScale = new Vector3(ranS,ranS,ranS);
        }
    }
    void GenerateFlowers(int i, float size){
        GameObject f = Instantiate(flowerPrefab, Location[i].transform.position, Quaternion.identity);
        Flower.Add(f);
        f.transform.localScale = new Vector3(size,size,size);
        f.transform.GetChild(1).gameObject.SetActive(true);
        GenerateLance(f);
    }
    void GenerateLance(GameObject flower){
        GameObject pt = flower.transform.GetChild(2).gameObject;
        GameObject L = Instantiate(lancePrefab, pt.transform.position, Quaternion.identity);
        L.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        // ready[i] = true;
    }
    private void RandomZ(){
        float r0 = Random.Range(-1,1);
        Location[0].transform.position = new Vector3(Location[0].transform.position.x, Location[0].transform.position.y, r0);
        float r1 = Random.Range(-1,1);
        Location[1].transform.position = new Vector3(Location[1].transform.position.x, Location[1].transform.position.y, r1);
        float r2 = Random.Range(-1,1);
        Location[2].transform.position = new Vector3(Location[2].transform.position.x, Location[2].transform.position.y, r2);
        float r3 = Random.Range(-1,1);
        Location[3].transform.position = new Vector3(Location[3].transform.position.x, Location[3].transform.position.y, r3);
    }
    void LanceOut(GameObject a){
        a.transform.Translate(Vector3.left * BG_speed * Time.deltaTime);
    }
    void Reset(){
       //trim clear list
    }
    // private void GetRandomX(float x, float y, float z){
    //     int ranX = Random.Range(-1,1);
    //     y = -3.2f;
    //     return Vector3(x,y,z)
    // }
 

    

    // public void Project(GameObject i){

    // }
}
