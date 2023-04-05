// CSE486 AR Capstone
// Scott Snyder
//
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Linq;
using Microsoft.MixedReality.Toolkit.UI;

#if WINDOWS_UWP
using Windows.Storage;
using Windows.System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
#endif


public class installWalls : MonoBehaviour
{
    [SerializeField] private GameObject baseWall;
    [SerializeField] private GameObject baseRebar;
    [SerializeField] private GameObject rebar;
    [SerializeField] private GameObject electrical;
    [SerializeField] private GameObject embeds;
    [SerializeField] private GameObject formwork;
    [SerializeField] private GameObject hvacBlocks;
    [SerializeField] private GameObject plumbing;
    [SerializeField] private GameObject victoryParent;


    [SerializeField] private GameObject rebarFail;
    [SerializeField] private GameObject electricalFail;
    [SerializeField] private GameObject embedsFail;
    [SerializeField] private GameObject formworkFail;
    [SerializeField] private GameObject hvacBlocksFail;
    [SerializeField] private GameObject plumbingFail;



    [Serializable]
    public class Player
    {
        public List<string> playerActions = new List<string>();
        public List<bool> playerValid = new List<bool>();
        public List<double> playerTime = new List<double>();
    }

    [SerializeField]
    [Tooltip("Assign DialogSmall_192x96.prefab")]
    private GameObject dialogPrefabSmall;

    /// <summary>
    /// Small Dialog example prefab to display
    /// </summary>
    public GameObject DialogPrefabSmall
    {
        get => dialogPrefabSmall;
        set => dialogPrefabSmall = value;
    }


    public static bool magicWord = false;
    public float val = 0;
    public bool timer;
    public static List<string> listActions = new List<string>();
    public static List<bool> listValid = new List<bool>();
    public static List<double> listTime = new List<double>();


    public void saveResults()
    {
        Player newPlayer = new Player();
        newPlayer.playerActions = listActions;
        newPlayer.playerTime = listTime;
        newPlayer.playerValid = listValid;
        string jsonString = JsonUtility.ToJson(newPlayer, true);

        //Application.persistentDataPath
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        using (TextWriter writer = File.AppendText(path))
        {
            // TODO write text here
            writer.WriteLine(jsonString);
        }

    }
    public void delivary()
    {
        magicWord = true;
    }

    public void addList(string s, bool f, float t)
    {
        listActions.Add(s);
        listValid.Add(f);
        listTime.Add(Math.Round(t, 2));
    }

    public void reset()
    {
        //destroy everything and run start from the top
        Destroy(GameObject.Find("rebarParent(Clone)"));
        Destroy(GameObject.Find("electricalParent(Clone)"));
        Destroy(GameObject.Find("formworkParent(Clone)"));
        Destroy(GameObject.Find("hvacParent(Clone)"));
        Destroy(GameObject.Find("plumbingParent(Clone)"));
        Destroy(GameObject.Find("embedsParent(Clone)"));
        Destroy(GameObject.Find("wallBase(Clone)"));


        Start();

    }


    public void notifyDelivery()
    {
        if (checkEER())
        {
            Debug.Log("THIS IS NOW WHERE AN EVENT NEEDS TO OCCUR TO LET STUDENT KNOW THAT THERE NEEDS TO BE A DILVERY");
            //!!!!!!!!!!!!!!!!!drop down notification at least!!!!!!!!!!!!!!!!
        }


    }

    //used to see if you can install formwork
    public bool checkEER() 
    { 
        if (GameObject.Find("rebarParent(Clone)") && GameObject.Find("electricalParent(Clone)") && GameObject.Find("embedsParent(Clone)"))
        {
            return true;
            
        }
        return false;
    }

    public void checkWin()
    {
        timer = false;
        Debug.Log("finsihed in: " + val);

        if(GameObject.Find("hvacParent(Clone)") && GameObject.Find("plumbingParent(Clone)") && !GameObject.Find("victoryParent(Clone)"))
        {
            GameObject victoryKeep = Instantiate(rebar, rebar.transform.position, Quaternion.Euler(0f, 180f, 0f));
            victoryKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            victoryKeep.GetComponent<Rigidbody>().useGravity = false;

            Destroy(GameObject.Find("rebarParent(Clone)"));
            Destroy(GameObject.Find("electricalParent(Clone)"));
            Destroy(GameObject.Find("formworkParent(Clone)"));
            Destroy(GameObject.Find("hvacParent(Clone)"));
            Destroy(GameObject.Find("plumbingParent(Clone)"));
            Destroy(GameObject.Find("embedsParent(Clone)"));
            Destroy(GameObject.Find("wallBase(Clone)"));


            Dialog myDialog = Dialog.Open(DialogPrefabSmall, DialogButtonType.Yes | DialogButtonType.No, "Nice job on completion!", "Would you like to save data?", true);
            if (myDialog != null)
            {
                myDialog.OnClosed += OnClosedDialogEvent;
            }

        }
    }
    private void OnClosedDialogEvent(DialogResult obj)
    {
        if (obj.Result == DialogButtonType.Yes)
        {
            //save results
            saveResults();
            
        }
    }
    public void InstallRebar()
    {
        //if else to prevent duplicate objects of same type
        if ( !GameObject.Find("rebarParent(Clone)"))
        {
            Destroy(GameObject.Find("rebarBase(Clone)"));
            GameObject rebarKeep = Instantiate(rebar, rebar.transform.position, Quaternion.Euler(0f, 180f, 0f));
            rebarKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            rebarKeep.GetComponent<Rigidbody>().useGravity = false;
            //rebarKeep.GetComponent<Collider>().enabled = false;
            addList("Install Rebar", true, val);
        } 
        else
        {
            GameObject rebarDestroy = Instantiate(rebarFail, rebar.transform.position, Quaternion.Euler(0f, 180f, 0f));
            rebarDestroy.GetComponent<Rigidbody>().useGravity = true;
            rebarDestroy.GetComponent<Collider>().enabled = true;
            Destroy(rebarDestroy, 5f);
            addList("Install Rebar", false, val);
        }
    }
    public void InstallElectrical()
    {
        if ( !GameObject.Find("electricalParent(Clone)") )
        {
            GameObject electricalKeep = Instantiate(electrical, electrical.transform.position, Quaternion.Euler(0f, 180f, 0f));
            electricalKeep.GetComponent<Rigidbody>().useGravity = false;
            electricalKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //electricalKeep.GetComponent<Collider>().enabled = false;
            notifyDelivery();
            addList("Install Electrical", true, val);
        }
        else
        {
            GameObject electricalDestroy = Instantiate(electricalFail, electrical.transform.position, Quaternion.Euler(0f, 180f, 0f));
            electricalDestroy.GetComponent<Rigidbody>().useGravity = true;
            electricalDestroy.GetComponent<Collider>().enabled = true;
            Destroy(electricalDestroy, 5f);
            addList("Install Electrical", false, val);

        }
        
    }
    public void InstallEmbeds()
    {
        if ( !GameObject.Find("embedsParent(Clone)") && GameObject.Find("rebarParent(Clone)"))
        {
            GameObject embedsKeep = Instantiate(embeds, embeds.transform.position, Quaternion.Euler(0f, 180f, 0f));
            embedsKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            embedsKeep.GetComponent<Rigidbody>().useGravity = false;
            //embedsKeep.GetComponent<Collider>().enabled = false;
            notifyDelivery();
            addList("Install Embeds", true, val);
        }
        else
        {
            GameObject emebedsDestroy = Instantiate(embedsFail, embeds.transform.position, Quaternion.Euler(0f, 180f, 0f));
            emebedsDestroy.GetComponent<Collider>().enabled = true;
            emebedsDestroy.GetComponent<Rigidbody>().useGravity = true;
            Destroy(emebedsDestroy, 5f);
            Debug.Log("Cant install embeds until rebar is placed");
            addList("Install Embeds", false, val);
        }
        
    }
    public void InstallFormwork()
    {
        if (!GameObject.Find("formworkParent(Clone)") && checkEER() && magicWord)
        {
            GameObject formworkKeep = Instantiate(formwork, formwork.transform.position, Quaternion.Euler(0f, 180f, 0f));
            formworkKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            formworkKeep.GetComponent<Rigidbody>().useGravity = false;
            formworkKeep.GetComponent<Collider>().enabled = false;
            addList("Install Formwork", true, val);
        }
        else
        {
            GameObject formworkDestroy = Instantiate(formworkFail, formwork.transform.position, Quaternion.Euler(0f, 180f, 0f));
            formworkDestroy.GetComponent<Rigidbody>().useGravity = true;
            formworkDestroy.GetComponent<Collider>().enabled = true;

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
            hvacKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            hvacKeep.GetComponent<Rigidbody>().useGravity = false;
            hvacKeep.GetComponent<Collider>().enabled = false;
            addList("Install HVAC blocks", true, val);
            checkWin();
        }
        else
        {
            GameObject hvacDestroy = Instantiate(hvacBlocksFail, hvacBlocks.transform.position, Quaternion.Euler(0f, 180f, 0f));
            hvacDestroy.GetComponent<Rigidbody>().useGravity = true;
            hvacDestroy.GetComponent<Collider>().enabled = true;
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
            plumbingKeep.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            plumbingKeep.GetComponent<Rigidbody>().useGravity = false;
            plumbingKeep.GetComponent<Collider>().enabled = false;
            checkWin();
            addList("Install Pluming", true, val);
        }
        else
        {
            GameObject plumbingDestroy = Instantiate(plumbingFail, plumbing.transform.position, Quaternion.Euler(0f, 180f, 0f));
            plumbingDestroy.GetComponent<Rigidbody>().useGravity = true;
            plumbingDestroy.GetComponent<Collider>().enabled = true;
            Destroy(plumbingDestroy, 5f);
            addList("Install Pluming", false, val);
            if (!GameObject.Find("formworkParent(Clone)"))
            {
                Debug.Log("You need to have formwork first in order to do this");
            }
            
        }
        
    }
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
        GameObject wallRebar = Instantiate(baseRebar, baseRebar.transform.position, Quaternion.Euler(0f, 180f, 0f));
        wallStart.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        wallStart.GetComponent<Collider>().enabled = false;
        Debug.Log(Application.persistentDataPath);

    }

}
