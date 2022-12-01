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

    public void InstallRebar()
    {
        if (GameObject.Find("base-wall"))
        {
            Instantiate(rebar, rebar.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
    }
    public void InstallElectrical()
    {
        if (GameObject.Find("base-wall"))
        {
            Instantiate(electrical, electrical.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
    }
    public void InstallEmbeds()
    {
        if (GameObject.Find("wall-2"))
        {
            Instantiate(embeds, embeds.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
        else
        {
            Destroy(Instantiate(embeds, embeds.transform.position, Quaternion.Euler(0f, 180f, 0f)), 5);

        }
        
    }
    public void InstallFormwork()
    {
        if (GameObject.Find("wall-3"))
        {
            Instantiate(formwork, formwork.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
        
    }
    public void InstallHVACBlocks()
    {
        if (GameObject.Find("wall-4"))
        {
            Instantiate(hvacBlocks, hvacBlocks.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
        
    }
    public void InstallPlumbing()
    {
        if (GameObject.Find("wall-5"))
        {
            Instantiate(plumbing, plumbing.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Start()
    {
        var star = Instantiate(baseWall, baseWall.transform.position, Quaternion.Euler(0f, 180f, 0f));
    }

}
