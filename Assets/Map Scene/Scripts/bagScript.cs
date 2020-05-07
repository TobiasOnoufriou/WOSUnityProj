using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bagScript : MonoBehaviour {
    Animator panelScript;
    public Animator bagAnimation;
    private bool open = false;
    // Use this for initialization
    void Start() {
        panelScript = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update() {

    }
    public void openBag()
    {
        if (!open)
        {
            open = true;
            this.gameObject.active = open;
            if (panelScript != null)
            {
                panelScript.SetBool("openBag", open);
                bagAnimation.SetBool("openBag", open);
            }
         

}
        else{
            open = false;
            StartCoroutine(waitForAnim(open));
        }
        
    }
    IEnumerator waitForAnim(bool open)
    {
        
        if (panelScript != null)
            panelScript.SetBool("openBag", open);
        bagAnimation.SetBool("openBag", open);
        yield return new WaitForSeconds(panelScript.GetCurrentAnimatorClipInfo(0).Length);
        
        this.gameObject.active = false;

    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Dungeon");
        if (col.gameObject.tag == "MapUI")
        {
            Destroy(col.gameObject);
        }
    }
    
}
