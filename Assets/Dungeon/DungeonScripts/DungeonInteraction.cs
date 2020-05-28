using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DungeonInteraction : MonoBehaviour {
    private bool playerEntered = false;
   //Player stuff like rigidbody
    private Rigidbody pRigidbody;
    private Animator pAnimation;

    private Vector2 startPos;
    private Vector2 direction;
    private bool directionChosen;
    private Color originalMat;

    private float pStartHealth;

    public Renderer bossMesh;
    public GameObject BossPosition;
    public Image pHealthBar;

    private int hits = 4;

    //Attack Bars 
    public Animator firstDagger, secondDagger, thirdDagger;

    public bool slidePart = false;
    // Use this for initialization
    void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
        pRigidbody = GetComponent<Rigidbody>();
        pAnimation = GetComponent<Animator>();
        originalMat = bossMesh.material.color;
        pStartHealth = DungeonManager.Instance.pHealth;
    }
	// Update is called once per frame
    void AttackBoss()
    {
        StartCoroutine(hitBoss());
    }
    void SwipeDirection(Vector2 direction){
        Debug.Log(direction);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(angle > 0 && angle < 90)
        {
            
        }
        else if(angle >= 90 && angle < 180)
        {

        }
        else if(angle >= 180 && angle < 270)
        {

        }
        else if(angle >= 270 && angle < 360)
        {
                
        }
    }
	void Update () {
        /*if (Input.GetMouseButtonDown(0))
        {
            AttackBoss();
        }*/
        if(Input.touchCount > 0 && playerEntered)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    
                    startPos = touch.position;
                    directionChosen = false;
                    break;
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    AttackBoss();
                    break;
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
        }
        if (directionChosen)
        {
            SwipeDirection(direction); 
        }
    }
    void FixedUpdate()
    {
        if (!playerEntered)
        {
            playerEntered = movePlayerTowardsBoss();
            slidePart = playerEntered;
        }
    }
    public void playerLoseHealth()
    {
        Debug.Log(DungeonManager.Instance.pHealth);
        DungeonManager.Instance.pHealth = DungeonManager.Instance.pHealth - 20;
        pHealthBar.fillAmount = DungeonManager.Instance.pHealth / pStartHealth;
        if(DungeonManager.Instance.pHealth <= 0)
        {
            //Die.
        }
    }
    bool movePlayerTowardsBoss()
    {
        if(Vector3.Distance(pRigidbody.position, BossPosition.transform.position ) > 10f)
        {
            Vector3 dist = BossPosition.transform.position - transform.position;
            dist.y = 0;

            Vector3 tgtVel = Vector3.ClampMagnitude(1.0f * dist, 5.0f);
            Vector3 error = tgtVel - pRigidbody.velocity;

            Vector3 force = Vector3.ClampMagnitude(5f * error, 5.0f);
            pRigidbody.AddForce(force);
            pAnimation.SetBool("RunningStill", false);
            pAnimation.Play("Running");

            return false;
        }else
        {
            pAnimation.SetBool("RunningStill", true);
            return true;
        }

    }
    
    IEnumerator hitBoss()
    {
        if (hits > 0)
        {
            hits--;
            switch (hits)
            {
                case 3:
                    firstDagger.Play("barAnimation");
                    break;
                case 2:
                    secondDagger.Play("barAnimation");
                    break;
                case 1:
                    thirdDagger.Play("barAnimation");
                    break;
            }
        }
        
        if (playerEntered && hits > 1)
        {
            
            BossPosition.GetComponent<BossScript>().bossLossHealth();
            pAnimation.Play("Attack");
            yield return new WaitForSeconds(0.7f);
            bossMesh.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            bossMesh.GetComponent<Renderer>().material.color = originalMat;
        }
        if (hits < 4)
        {
            hits++;
        }




    }
}
