// CSE486 AR Capstone
// Scott Snyder
//
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

    public float val = 0;
    public bool timer;
    public static List<string> listActions = new List<string>();
    public static List<bool> listValid = new List<bool>();
    public static List<float> listTime = new List<float>();

    public void addList(string s, bool f, float t)
    {
        listActions.Add(s);
        listValid.Add(f);
        listTime.Add(t);
    }
    public void printList()
    {
        Debug.Log("Actions \t Correctness \t Timen\n");
        for (int i = 0; i < listActions.Count; i++)
        {
            Debug.Log(listActions[i] + "\t" + listValid[i] + "\t" +listTime[i] );

        }
    }
    public void reset()
    {
        Destroy(GameObject.Find("rebarParent(Clone)"));
        Destroy(GameObject.Find("electricalParent(Clone)"));
        Destroy(GameObject.Find("formworkParent(Clone)"));
        Destroy(GameObject.Find("hvacParent(Clone)"));
        Destroy(GameObject.Find("plumbingParent(Clone)"));

    }

    public void notifyDelivery()
    {
        if (checkEER())
        {
            Debug.Log("THIS IS NOW WHERE AN EVENT NEEDS TO OCCUR TO LET STUDENT KNOW THAT THERE NEEDS TO BE A DILVERY");
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!drop down notification at least!!!!!!!!!!!!!!!!
        }


    }


    //HVAC blocks and plumbing is dependant on formwork

    //formwork depends on previous three being installed

    //embeds are dependant on rebar

    //rebar and electrical are free to be installed at anytime
    public bool checkEER()
    { 
        if (GameObject.Find("rebarParent(Clone)") && GameObject.Find("electricalParent(Clone)") && GameObject.Find("embedsParent(Clone)"))
        {
            return true;
            
        }
        return false;
    }
        //win function
    public void checkWin()
    {
        timer = false;
        Debug.Log("finsihed in: " + val);

        if(GameObject.Find("hvacParent(Clone)") && GameObject.Find("plumbingParent(Clone)"))
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!You Win!! NICE!!!!!!!!!!!!!!!!!!!!!!");
            printList();  
        }
    }
  
    public void InstallRebar()
    {
        //if else to prevent duplicate objects of same type
        if ( !GameObject.Find("rebarParent(Clone)"))
        {
            GameObject rebarKeep = Instantiate(rebar, rebar.transform.position, Quaternion.Euler(0f, 180f, 0f));
            rebarKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            rebarKeep.GetComponent<Rigidbody>().useGravity = false;
            rebarKeep.GetComponent<Collider>().enabled = false;
            addList("Install Rebar", true, val);
        } 
        else
        {
            GameObject rebarDestroy = Instantiate(rebar, rebar.transform.position, Quaternion.Euler(0f, 180f, 0f));
            rebarDestroy.GetComponent<Rigidbody>().useGravity = true;
            rebarDestroy.GetComponent<Collider>().enabled = true;
            rebarDestroy.GetComponent<Animator>().enabled = false;
            Destroy(rebarDestroy, 5f);
            addList("Install Rebar", false, val);
        }
    }
    public void InstallElectrical()
    {
        if ( !GameObject.Find("electricalParent(Clone)")  && GameObject.Find("rebarParent(Clone)"))
        {
            GameObject electricalKeep = Instantiate(electrical, electrical.transform.position, Quaternion.Euler(0f, 180f, 0f));
            electricalKeep.GetComponent<Rigidbody>().useGravity = false;
            electricalKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            electricalKeep.GetComponent<Collider>().enabled = false;
            notifyDelivery();
            addList("Install Electrical", true, val);
        }
        else
        {
            GameObject electricalDestroy = Instantiate(electrical, electrical.transform.position, Quaternion.Euler(0f, 180f, 0f));
            electricalDestroy.GetComponent<Rigidbody>().useGravity = true;
            electricalDestroy.GetComponent<Collider>().enabled = true;
            electricalDestroy.GetComponent<Animator>().enabled = false;
            Destroy(electricalDestroy, 5f);
            addList("Install Electrical", false, val);

        }
        
    }
    public void InstallEmbeds()
    {
        //if true then its intended to do nothing 
        if ( !GameObject.Find("embedsParent(Clone)") && GameObject.Find("rebarParent(Clone)"))
        {
            GameObject embedsKeep = Instantiate(embeds, embeds.transform.position, Quaternion.Euler(0f, 180f, 0f));
            embedsKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            embedsKeep.GetComponent<Rigidbody>().useGravity = false;
            embedsKeep.GetComponent<Collider>().enabled = false;
            notifyDelivery();
            addList("Install Embeds", true, val);
        }
        else
        {
            GameObject emebedsDestroy = Instantiate(embeds, embeds.transform.position, Quaternion.Euler(0f, 180f, 0f));
            emebedsDestroy.GetComponent<Rigidbody>().useGravity = true;
            emebedsDestroy.GetComponent<Collider>().enabled = true;
            emebedsDestroy.GetComponent<Animator>().enabled = false;
            Destroy(emebedsDestroy, 5f);
            Debug.Log("Cant install embeds until rebar is placed");
            addList("Install Embeds", false, val);
        }
        
    }
    public void InstallFormwork()
    {
        if (!GameObject.Find("formworkParent(Clone)") && checkEER())
        {
            GameObject formworkKeep = Instantiate(formwork, formwork.transform.position, Quaternion.Euler(0f, 180f, 0f));
            formworkKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            formworkKeep.GetComponent<Rigidbody>().useGravity = false;
            formworkKeep.GetComponent<Collider>().enabled = false;
            addList("Install Formwork", true, val);
        }
        else
        {
            GameObject formworkDestroy = Instantiate(formwork, formwork.transform.position, Quaternion.Euler(0f, 180f, 0f));
            formworkDestroy.GetComponent<Rigidbody>().useGravity = true;
            formworkDestroy.GetComponent<Collider>().enabled = true;
            formworkDestroy.GetComponent<Animator>().enabled = false;
            Destroy(formworkDestroy, 5f);
            addList("Install Formwork", false, val);
            Debug.Log("Cant install formwork until Other conditions are met");
        }
        
    }
    public void InstallHVACBlocks()
    {
        
        if ( !GameObject.Find("hvacParent(Clone)")  && (GameObject.Find("formworkParent(Clone)") ) )
        {
            GameObject hvacKeep = Instantiate(hvacBlocks, hvacBlocks.transform.position, Quaternion.Euler(0f, 180f, 0f));
            hvacKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            hvacKeep.GetComponent<Rigidbody>().useGravity = false;
            hvacKeep.GetComponent<Collider>().enabled = false;
            addList("Install HVAC blocks", true, val);
            checkWin();
        }
        else
        {
            GameObject hvacDestroy = Instantiate(hvacBlocks, hvacBlocks.transform.position, Quaternion.Euler(0f, 180f, 0f));
            hvacDestroy.GetComponent<Rigidbody>().useGravity = true;
            hvacDestroy.GetComponent<Collider>().enabled = true;
            hvacDestroy.GetComponent<Animator>().enabled = false; ;
            Destroy(hvacDestroy, 5f);
            addList("Install HVAC blocks", false, val);
            if (!GameObject.Find("formworkParent(Clone)"))
            {
                Debug.Log("You need to have formwork first in order to do this");
            }
        }
        
    }
    public void InstallPlumbing()
    {
        if ( !GameObject.Find("plumbingParent(Clone)")  && GameObject.Find("formworkParent(Clone)"))
        {
            GameObject plumbingKeep = Instantiate(plumbing, plumbing.transform.position, Quaternion.Euler(0f, 180f, 0f));
            plumbingKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            plumbingKeep.GetComponent<Rigidbody>().useGravity = false;
            plumbingKeep.GetComponent<Collider>().enabled = false;
            checkWin();
            addList("Install Pluming", true, val);
        }
        else
        {
            GameObject plumbingDestroy = Instantiate(plumbing, plumbing.transform.position, Quaternion.Euler(0f, 180f, 0f));
            plumbingDestroy.GetComponent<Rigidbody>().useGravity = true;
            plumbingDestroy.GetComponent<Collider>().enabled = true;
            plumbingDestroy.GetComponent<Animator>().enabled = false;
            Destroy(plumbingDestroy, 5f);
            addList("Install Pluming", false, val);
            if (!GameObject.Find("formworkParent(Clone)"))
            {
                Debug.Log("You need to have formwork first in order to do this");
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer)
        {
            val += Time.deltaTime;
        }
        
    }

    void Start()
    {
        timer = true;
        GameObject wallStart = Instantiate(baseWall, baseWall.transform.position, Quaternion.Euler(0f, 180f, 0f));
        wallStart.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        wallStart.GetComponent<Collider>().enabled = false;
    }

}
