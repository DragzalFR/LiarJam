using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : Personage
{
    private Vector3 StartPosition;
    private int keysCount = 0;
    public int UI_Key => keysCount;

    [SerializeField]
    private int LifeCounter = 3;
    public int UI_Life => LifeCounter;

    private float waitTime = 0.0f;
    private bool IsHitAnimation = false;

    void Start()
    {
        StartPosition = transform.position;
    }

    public void BeingHit()
    {
        Time.timeScale = 0.1f;
        LifeCounter--;
        waitTime = 1f;
        IsHitAnimation = true;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {

        // For collecting keys
        if (other.gameObject.CompareTag("Keys")) 
        {
            if(other.gameObject.GetComponent<Key>().PickUp(this.transform.position))
            {
                other.gameObject.SetActive(false);
                keysCount++;
                SetTextCount();
            }
        }

        // For openening doors
        if (other.gameObject.CompareTag("Door"))
        {
            if(other.gameObject.GetComponent<Door>().Open(keysCount)) 
            {
                SetTextCount();
            }
        }
    }

    private void SetTextCount() {
        // Changes value of no of keys collected in the UI
        Debug.Log(" keysCount is " + keysCount);
    }

    private void HitAnimationManager(float timeEllasped)
    {
        waitTime -= timeEllasped;
        if(waitTime < 0.3 && IsHitAnimation)
        {
            if (LifeCounter == 0)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }     

            transform.position = StartPosition;
            IsHitAnimation = false;
        }

        if(waitTime < 0)
        {
            Time.timeScale = 1;
        }

    }

    void Update()
    {
        if (waitTime > 0)
        {
            HitAnimationManager(Time.deltaTime / Time.timeScale);
        }
    }

    void FixedUpdate()
    {
        if (!Input.anyKey)
        {
            MoveTowardDirection(Vector2.zero);
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                MoveTowardDirection(Vector2.up);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                MoveTowardDirection(Vector2.down);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveTowardDirection(Vector2.left);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveTowardDirection(Vector2.right);
            }
        }


    }

   
}

