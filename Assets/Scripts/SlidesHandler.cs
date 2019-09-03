using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlidesHandler : MonoBehaviour
{
    private int currentSlide = 0;
    
    public GameObject[] sequenceOfSlides;
    // public SceneAsset sceneToLoadNext;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject slide in sequenceOfSlides)
        {
            slide.SetActive(false);
        }

        sequenceOfSlides[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ChangeSlide();
        }
    }

    public void ChangeSlide() 
    {
        Debug.Log("currentSlide" + currentSlide);
        if(currentSlide < (sequenceOfSlides.Length - 1)) 
        {
            sequenceOfSlides[currentSlide].SetActive(false);
            sequenceOfSlides[++currentSlide].SetActive(true);
        }
        else 
        {
            try{
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            catch
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void MoveToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
