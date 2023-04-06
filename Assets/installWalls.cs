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


    [SerializeField]
    [Tooltip("Assign DialogSmall_192x96.prefab")]
    private GameObject dialogPrefabSmall;

    public GameObject DialogPrefabSmall
    {
        get => dialogPrefabSmall;
        set => dialogPrefabSmall = value;
    }

    public static bool magicWord = false;
    public float val = 0;
    public bool timer;

    public string playerData = "Action,Time,Valid Choice?\n";

    public void saveResults()
    {
        string CSVpath = Path.Combine(Application.persistentDataPath, "export.csv");
        using (TextWriter writer = File.AppendText(CSVpath))
        {
            // TODO write text here
            writer.WriteLine(playerData);
            Debug.Log(playerData);
        }

    }
    public void delivary()
    {
        magicWord = true;
    }

    public void addList(string action,float time, bool valid)
    {
        playerData = action + "," + time + "," + valid + "\n"; 
    }

    public void reset()
    {
        //destroy everything and run start from the top
        Destroy(GameObject.Find("rebarParent(Clone)"));
        Destroy(GameObject.Find("rebarBase(Clone)"));
        Destroy(GameObject.Find("electricalParent(Clone)"));
        Destroy(GameObject.Find("formworkParent(Clone)"));
        Destroy(GameObject.Find("hvacParent(Clone)"));
        Destroy(GameObject.Find("plumbingParent(Clone)"));
        Destroy(GameObject.Find("embedsParent(Clone)"));
        Destroy(GameObject.Find("wallBase(Clone)"));
        Destroy(GameObject.Find("victoryPArent(Clone)"));


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
            

            Destroy(GameObject.Find("rebarParent(Clone)"));
            Destroy(GameObject.Find("rebarBase(Clone)"));
            Destroy(GameObject.Find("electricalParent(Clone)"));
            Destroy(GameObject.Find("formworkParent(Clone)"));
            Destroy(GameObject.Find("hvacParent(Clone)"));
            Destroy(GameObject.Find("plumbingParent(Clone)"));
            Destroy(GameObject.Find("embedsParent(Clone)"));
            Destroy(GameObject.Find("wallBase(Clone)"));

            GameObject victoryKeep = Instantiate(victoryParent, victoryParent.transform.position, Quaternion.Euler(0f, 180f, 0f));

            //maybe should wait 5 seconds

            Dialog myDialog = Dialog.Open(DialogPrefabSmall, DialogButtonType.Yes | DialogButtonType.No, "Nice job on completion!", "Would you like to save data?", true);
            if (myDialog != null)
            {
                myDialog.OnClosed += OnClosedDialogEvent;
            }

        }
    }

    public void installGameObject(GameObject wallComponent){
        //i might not need this string but it would be nice
        //string is to help function look for prefab clone that would exist within the scene
        string newTemp = wallComponent.name + "(Clone)";
        if (!GameObject.Find( newTemp)) {
            Debug.Log(newTemp); 
            GameObject lmao = Instantiate(wallComponent, wallComponent.transform.position, Quaternion.Euler(0f, 180f, 0f));
            lmao.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            lmao.GetComponent<Rigidbody>().useGravity = false;
            //rebarKeep.GetComponent<Collider>().enabled = false;
            addList(newTemp, val, true);
            //return lmao;
        } else
        {
            GameObject wallDestroy = Instantiate(wallComponent, wallComponent.transform.position, Quaternion.Euler(0f, 180f, 0f));
            wallDestroy.GetComponent<Rigidbody>().useGravity = true;
            wallDestroy.GetComponent<Collider>().enabled = true;
            Destroy(wallDestroy, 5f);
            addList(newTemp, val, false);
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
        installGameObject(rebar);
    }

    public void InstallElectrical()
    {
        installGameObject(electrical);
    }

    public void InstallEmbeds()
    {
        installGameObject(embeds);

    }

    public void InstallFormwork()
    {
        installGameObject(formwork);

    }

    public void InstallHVACBlocks()
    {
        installGameObject(hvacBlocks);

    }

    public void InstallPlumbing()
    {
        installGameObject(plumbing);
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
