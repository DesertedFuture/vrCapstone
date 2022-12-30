using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class installWalls : MonoBehaviour
{
    [SerializeField] private GameObject baseWall;
    [SerializeField] private GameObject rebar;
    [SerializeField] private GameObject electrical;
    [SerializeField] private GameObject embeds;
    [SerializeField] private GameObject formwork;
    [SerializeField] private GameObject hvacBlocks;
    [SerializeField] private GameObject plumbing;
    [SerializeField] private GameObject fullWall;
    /*
     * if else statement to check if any of their own objects exist already
     * 
     */
    //win function
    public void win()
    {
        if(GameObject.Find("hvac(Clone)") && GameObject.Find("plumbing(Clone)"))
        {
            Debug.Log("You Win!! NICE!!!!!!!!!!!!!!!!!!!!!!");
        }
    }
    //utility function to check if everything is done prior to 'curve ball'
    public bool checkEmbedsElectrical()
    {
        if(GameObject.Find("rebar(Clone)") && GameObject.Find("electrical(Clone)") && GameObject.Find("embeds(Clone)") )
        {
            return true;
        }
        return false;
    }
    
    public void dropObject(GameObject t)
    {

    }
    
    public void InstallRebar()
    {
        //if else to prevent duplicate objects of same type
        if( GameObject.Find("rebar(Clone)") )
        {
            GameObject lmao = Instantiate(rebar, rebar.transform.position, Quaternion.Euler(0f, 180f, 0f));
            dropObject(lmao);
        } else
        {
            Instantiate(rebar, rebar.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
    }
    public void InstallElectrical()
    {
        if ( GameObject.Find("electrical(Clone)") )
        {
        }
        else
        {
            if (checkEmbedsElectrical())
            {
                Debug.Log("THIS IS NOW WHERE AN EVENT NEEDS TO OCCUR TO LET STUDENT KNOW THAT THERE NEEDS TO BE A DILVERY");
            }
            Instantiate(electrical, electrical.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
        
    }
    public void InstallEmbeds()
    {
        if ( GameObject.Find("embeds(Clone)") )
        {
        }
        else
        {
            //nested if to make sure it can be called as to flow chart
            if (GameObject.Find("rebar(Clone)"))
            {
                Instantiate(embeds, embeds.transform.position, Quaternion.Euler(0f, 180f, 0f));
                if(checkEmbedsElectrical())
                {
                    Debug.Log("THIS IS NOW WHERE AN EVENT NEEDS TO OCCUR TO LET STUDENT KNOW THAT THERE NEEDS TO BE A DILVERY");
                }
            } else
            {
                //maybe a failure instance
                Debug.Log("Cant install embeds until rebar is placed");
            }
            
        }
        
    }
    public void InstallFormwork()
    {
        if (GameObject.Find("formwork(Clone)"))
        {
        }
        else
        {
            if (checkEmbedsElectrical())
            {
                Instantiate(formwork, formwork.transform.position, Quaternion.Euler(0f, 180f, 0f));
            }
            
        }
        
    }
    public void InstallHVACBlocks()
    {
        
        if ( GameObject.Find("hvac(Clone)") )
        {
        }
        else
        {
            if (GameObject.Find("formwork(Clone)") )
            {
                Instantiate(hvacBlocks, hvacBlocks.transform.position, Quaternion.Euler(0f, 180f, 0f));
                win();
            }
            else
            {
                Debug.Log("You need to have formwork first in order to do this");
            }
            
        }
        
    }
    public void InstallPlumbing()
    {
        if ( GameObject.Find("plumbing(Clone)") )
        {
        }
        else
        {
            if (GameObject.Find("formwork(Clone)") )
            {
                Instantiate(plumbing, plumbing.transform.position, Quaternion.Euler(0f, 180f, 0f));
                win();
            } else
            {
                Debug.Log("You need to have formwork first in order to do this");
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Start()
    {
        Instantiate(baseWall, baseWall.transform.position, Quaternion.Euler(0f, 180f, 0f));        
    }

}
