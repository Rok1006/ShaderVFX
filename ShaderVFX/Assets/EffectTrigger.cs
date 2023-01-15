using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//For Calling effects
public class EffectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Slash;

    void Start()
    {
        Slash.SetActive(false);
    }
    public void SlashAppear(){
        Slash.SetActive(true);
    }
}
