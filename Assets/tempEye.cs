using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempEye : MonoBehaviour
{
    bool active = false;
    // Start is called before the first frame update
    float timer = 0;
    public void setActive()
    {
        active = true;
    }
    public void deselectActive()
    {
        active = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
   public void Update()
    {
        if (active) { timer += Time.deltaTime; Debug.Log(timer); }
        
    }
}