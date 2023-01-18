using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//For Calling effects
public class EffectTrigger : MonoBehaviour
{
    [SerializeField] private Ability_Garna AG;
    [SerializeField] private GameObject Slash;
    [SerializeField] private GameObject DV_VFX;
    [SerializeField] private GameObject DV_VFX_Stay;

    void Start()
    {
        Slash.SetActive(false);
        DV_VFX.SetActive(false);
        DV_VFX_Stay.SetActive(false);
    }
    public void SlashAppear(){
        Slash.SetActive(true);
    }
    public void DOV_VFX(){
        DV_VFX.SetActive(true);
    }
    public void DOV_V2(){
        AG.slashOut = true;
        DV_VFX_Stay.SetActive(true);
    }
}
