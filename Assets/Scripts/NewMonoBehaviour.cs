using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class NewMonoBehaviour : MonoBehaviour
{
    public int currentIndex = 0; //keeps track of the index for words

    public GameObject Phrase1;
    public GameObject Day;
    public GameObject Night;

    public SpriteRenderer p1;
    public SpriteRenderer p2;
    public SpriteRenderer p3;
    public SpriteRenderer p4;
    public SpriteRenderer p5;
    public SpriteRenderer p6;
    public SpriteRenderer p7;
    public SpriteRenderer p8;
    public SpriteRenderer p9;
    public SpriteRenderer p10;
    public SpriteRenderer p11;
    public SpriteRenderer p12;
    public SpriteRenderer p13;
    public SpriteRenderer p14;
    public SpriteRenderer p15;
    public SpriteRenderer p16;
    public SpriteRenderer p17;
    public SpriteRenderer p18;
    public SpriteRenderer p19;
    public SpriteRenderer p20;
    public SpriteRenderer p21;
    public SpriteRenderer p22;
    public SpriteRenderer p23;
    public SpriteRenderer p24;
    public SpriteRenderer p25;
    public SpriteRenderer p26;
    Animator animator;

    // Use this for initialization
    void Start()
    {
        Night.gameObject.SetActive(false);
       
        Phrase1 = GameObject.FindWithTag("Phrase1");
        Day = GameObject.FindWithTag("StoryDayTimeExtras");
        animator = GetComponent<Animator>();
    }


    //next button push
    public void nextMouseClick()
    {

        if (currentIndex <= 17)
        {
            currentIndex++;
        }
        //loads everything for that screen
        allTheStuff();
    }

    //previous button push
    public void previousMouseClick()
    {
        if (currentIndex <= 17)
        {
            currentIndex--;
        }
        //loads everthing for that screen
        allTheStuff();
    }

    //all the components for each phrase
    public void allTheStuff()
    {
     
        if (currentIndex == 1)
        {
            Phrase1.gameObject.SetActive(false);
            p1.enabled = true;
            animator.Play("PloyBananaLeaves");

        }
        if (currentIndex == 2){
            p1.enabled = false;

            p2.enabled = true;
            p3.enabled = true;
            p4.enabled = true;
            p5.enabled = true;
        }
        //if(currentIndex==3){} - Just a question
        if (currentIndex == 4)
        {
            p2.enabled = false;
            p3.enabled = false;
            p4.enabled = false;
            p5.enabled = false;
           
            p6.enabled = true;
            animator.Play("peachAni");
        }
        if (currentIndex == 5)
        {
            p6.enabled = false;

            p7.enabled = true;
            p8.enabled = true;
            animator.Play("peachBreadAn");
            animator.Play("PeachBro");
        }
        //if (currentIndex == 6){} - Just a question
        if (currentIndex == 7)
        {
            p7.enabled = false;
            p8.enabled = false;

            p9.enabled = true;
            animator.Play("PeachWish");
        }
        //if (currentIndex == 8){} - Just a question
        if (currentIndex == 9)
        {
            p9.enabled = false;
            Day.gameObject.SetActive(false);
            //Night.gameObject.SetActive(true);

            p10.enabled = true;
            p11.enabled = true;
            p12.enabled = true;
            p13.enabled = true;
            p14.enabled = true;
            p15.enabled = true;
            animator.Play("K4");
            animator.Play("K3");
            animator.Play("K2");
            animator.Play("K1");
        }
        if (currentIndex == 10)
        {
            
            p16.enabled = true;
            p17.enabled = true;
            p18.enabled = true;
            p19.enabled = true;
            p20.enabled = true;
            p21.enabled = true;
            p22.enabled = true;
            animator.Play("K4");
            animator.Play("K3");
            animator.Play("K2");
            animator.Play("K1");
        }
        //if (currentIndex == 11){} phrase 7 & 8
        if (currentIndex == 12)
        {
         
            p23.enabled = true;
            animator.Play("ployRiver");
            animator.Play("K4");
            animator.Play("K3");
            animator.Play("K2");
            animator.Play("K1");
        }
        //if (currentIndex == 13){} same
        //if (currentIndex == 14){} same
        if (currentIndex == 15)
        {
            p16.enabled = false;
            p17.enabled = false;
            p18.enabled = false;
            p19.enabled = false;
            p20.enabled = false;
            p21.enabled = false;
            p22.enabled = false;
            p23.enabled = false;

            p24.enabled = true;
            p25.enabled = true;
            animator.Play("peach1Wish");
            animator.Play("ployTalking");

        }
        if (currentIndex == 17) 
        {
            p24.enabled = false;
            p26.enabled = true; 
        }
            
    }
}
