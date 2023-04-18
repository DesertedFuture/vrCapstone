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

    [SerializeField] private GameObject embedsFail;
    [SerializeField] private GameObject HVACFail;
    [SerializeField] private GameObject plumbingFail;


    public string playerData = "";

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

    

    //functions for saving player data
    public void setPlayerData()
    {
        playerData = "Action,Time,Valid Choice?\n";
    }

    public void saveResults()
    {
        string CSVpath = Path.Combine(Application.persistentDataPath, "playerData.csv");
        using (TextWriter writer = File.AppendText(CSVpath))
        {
            writer.WriteLine(playerData);
            Debug.Log(playerData);
        }

    }

    public void addList(string action,float time, bool valid)
    {

        playerData += action + "," + time.ToString("F2") + "," + valid + "\n"; 
        Debug.Log(playerData);
    }


    //functions for dialog box
    public void dialog()
    {
        Dialog myDialog = Dialog.Open(DialogPrefabSmall, DialogButtonType.Yes | DialogButtonType.No, "Nice job on completion!", "Would you like to save data?", true);
        if (myDialog != null)
        {
            myDialog.OnClosed += OnClosedDialogEvent;
        }
    }

    private void OnClosedDialogEvent(DialogResult obj)
    {
        if (obj.Result == DialogButtonType.Yes)
        {
            saveResults();
        }
    }




    //functions for checking win
    public void prepWin()
    {
        //destroys everything except victory model
        Destroy(GameObject.Find("baseFormwork(Clone)"));
        Destroy(GameObject.Find("baseWall(Clone)"));
        Destroy(GameObject.Find("baseRebar(Clone)"));
        Destroy(GameObject.Find("rebarParent(Clone)"));
        Destroy(GameObject.Find("electricalParent(Clone)"));
        Destroy(GameObject.Find("formworkParent(Clone)"));
        Destroy(GameObject.Find("hvacParent(Clone)"));
        Destroy(GameObject.Find("plumbingParent(Clone)"));
        Destroy(GameObject.Find("embedsParent(Clone)"));

    }

    public void checkWin()
    {
        timer = false;
        Debug.Log("finsihed in: " + val);

        if(GameObject.Find("hvacParent(Clone)") && GameObject.Find("plumbingParent(Clone)") && !GameObject.Find("victoryParent(Clone)"))
        {
            prepWin();
            GameObject victoryKeep = Instantiate(victoryParent, victoryParent.transform.position, Quaternion.Euler(0f, 180f, 0f));

            //dialog needs to wait for victory animation to finish
            Invoke("dialog", 15);
        }
    }



    //utility functions for installing / failing objects


    public void failGameObject(GameObject wallComponent)
    {
        string newTemp = wallComponent.name + "(Clone)";
        GameObject wallDestroy = Instantiate(wallComponent, wallComponent.transform.position, Quaternion.Euler(0f, 180f, 0f));
        Destroy(wallDestroy, 10f);
        addList(newTemp, val, false);
    }

    public void installGameObject(GameObject wallComponent){
        string wallComponentName = wallComponent.name + "(Clone)";
        if (!GameObject.Find(wallComponentName)) {
            Debug.Log(wallComponentName); 
            GameObject newWallComponent = Instantiate(wallComponent, wallComponent.transform.position, Quaternion.Euler(0f, 180f, 0f));
            addList(wallComponentName, val, true);

        } else
        {
            addList(wallComponentName, val, false);
        }
    }



    //functions that are callable to user
    public void delivery()
    {
        magicWord = true;
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
        if (GameObject.Find("rebarParent(Clone)")) {
            installGameObject(embeds);
        } else
        {
            failGameObject(embedsFail);
        }
    }

    public void InstallFormwork()
    {
        if (magicWord) 
        {
            Destroy(GameObject.Find("baseRebar(Clone)"));
            installGameObject(formwork);
        }
    }

    public void InstallHVACBlocks()
    {
        if (GameObject.Find("formworkParent(Clone)"))
        {
            installGameObject(hvacBlocks);
        } else
        {
            failGameObject(HVACFail);
        }
        Invoke("checkWin", 15);
    }

    public void InstallPlumbing()
    {
        if (GameObject.Find("formworkParent(Clone)"))
        {
            installGameObject(plumbing);
        }
        else
        {
            failGameObject(plumbingFail);
        }
        Invoke("checkWin", 15);
        
    }

    public void reset()
    {
        //destroy everything and run start from the top
        
        Destroy(GameObject.Find("baseWall(Clone)"));
        Destroy(GameObject.Find("baseFormwork(Clone)"));
        Destroy(GameObject.Find("baseRebar(Clone)"));
        Destroy(GameObject.Find("rebarParent(Clone)"));
        Destroy(GameObject.Find("electricalParent(Clone)"));
        Destroy(GameObject.Find("formworkParent(Clone)"));
        Destroy(GameObject.Find("hvacParent(Clone)"));
        Destroy(GameObject.Find("plumbingParent(Clone)"));
        Destroy(GameObject.Find("embedsParent(Clone)"));
        Destroy(GameObject.Find("victoryParent(Clone)"));
        Destroy(GameObject.Find("hvacFail(Clone)"));
        Destroy(GameObject.Find("plumbingFail(Clone)"));

        timer = false;
        val = 0;
        magicWord = false;
        Invoke("Start", 2);
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
        installGameObject(baseWall);
        installGameObject(baseRebar);
        Debug.Log(Application.persistentDataPath);
        setPlayerData();
    }
}
