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
            Instantiate(rebar, rebar.transform.position, Quaternion.Euler(0f, 180f, 0f));
    }
    public void InstallElectrical()
    {
            Instantiate(electrical, electrical.transform.position, Quaternion.Euler(0f, 180f, 0f));
    }
    public void InstallEmbeds()
    {
            Instantiate(embeds, embeds.transform.position, Quaternion.Euler(0f, 180f, 0f));
    }
    public void InstallFormwork()
    {
            Instantiate(formwork, formwork.transform.position, Quaternion.Euler(0f, 180f, 0f));
    }
    public void InstallHVACBlocks()
    {
            Instantiate(hvacBlocks, hvacBlocks.transform.position, Quaternion.Euler(0f, 180f, 0f));
    }
    public void InstallPlumbing()
    {
            Instantiate(plumbing, plumbing.transform.position, Quaternion.Euler(0f, 180f, 0f));
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
