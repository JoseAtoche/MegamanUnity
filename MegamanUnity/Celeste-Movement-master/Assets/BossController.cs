using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
      private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int aleatorio = Random.Range(0, 10);
        switch (aleatorio) { 
        
        
        
        }



        anim.SetBool("guillotine", true);
    }
}
