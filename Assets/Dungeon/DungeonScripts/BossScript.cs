using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossScript : MonoBehaviour {
    public GameObject Camera;
    public Image bHealthBar;
    public Renderer pMesh;
    public GameObject player;
    public GameObject hintLeft, hintCenter, hintRight;
    private int position = 0;

    private bool[] battackHint;

    private Animator bAnimator;
   
    private Color originalMat;
    private Color originalMatPlate;
    private float startbHealth;
    // Use this for initialization
    void Start () {
        bAnimator = GetComponent<Animator>();
        InvokeRepeating("attackHint", 3.0f, 2.0f);
        InvokeRepeating("attackPlayer", 5.0f, 4.0f);
        originalMat = pMesh.material.color;
        originalMatPlate = hintLeft.GetComponent<Renderer>().material.color;
        startbHealth = DungeonManager.Instance.bHealth;
        battackHint = new bool[] { false, false, false };
    }
	    
    void attackPlayer()
    {
        //Play Enemy Animation.
        //Either choose between left, middle, right swipe.
        //Right swipe {true, false, false}.
        //Left swipe {false, false, true}.
        //middle swipe {false, true, false}.
        //int position = 0;//Random.Range(0, 3);
        Debug.Log(position);
        battackHint[position] = false;
        DungeonManager.Instance.bAttack[position] = true;
        bAnimator.Play("Attack");
        StartCoroutine(WaitTillFinish(position));

    }
    void attackHint()
    {
        position = Random.Range(0, 3);
        battackHint[position] = true;
        StartCoroutine(hintFlash());
    }
    public void bossLossHealth()
    {
        StartCoroutine(waitForHit());  
    }
    IEnumerator hintFlash()
    {
        for (int i = 0; i < 10; i++)
        {
            switch (position)
            {
                case 0:
                    hintLeft.GetComponent<Renderer>().material.color = Color.red;
                    hintLeft.GetComponent<Renderer>().material.color = originalMatPlate;
                    yield return new WaitForSeconds(0.2f);
                    break;
                case 1:
                    hintCenter.GetComponent<Renderer>().material.color = Color.red;
                    hintCenter.GetComponent<Renderer>().material.color = originalMatPlate;
                    yield return new WaitForSeconds(0.2f);
                    break;
                case 2:
                    hintRight.GetComponent<Renderer>().material.color = Color.red;
                    hintRight.GetComponent<Renderer>().material.color = originalMatPlate;
                    yield return new WaitForSeconds(1.5f);
                    break;
                default:
                    break;
            }
        }
        yield return new WaitForSeconds(1.5f);
    }
    IEnumerator waitForHit()
    {
        yield return new WaitForSeconds(0.7f);
        DungeonManager.Instance.bHealth = DungeonManager.Instance.bHealth - 20;
        bHealthBar.fillAmount = DungeonManager.Instance.bHealth / startbHealth;
    }
    IEnumerator WaitTillFinish(int position)
    {
        if(DungeonManager.Instance.bAttack[DungeonManager.Instance.pLane] == true)
        {
            StartCoroutine(playerHit());
        }
        yield return new WaitForSeconds(0.5f);
        DungeonManager.Instance.bAttack[position] = false;
    }
    IEnumerator playerHit() {
        yield return new WaitForSeconds(0.2f);
        player.GetComponent<DungeonInteraction>().playerLoseHealth();
        pMesh.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        pMesh.GetComponent<Renderer>().material.color = originalMat;
    }

	// Update is called once per frame
	void Update () {
		if(DungeonManager.Instance.bHealth <= 0)
        {
            Destroy(this.gameObject);
            Camera.GetComponent<Animator>().SetBool("DungeonEnded", true);
        }
    }
}
