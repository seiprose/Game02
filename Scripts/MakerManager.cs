using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakerManager : MonoBehaviour
{
    [SerializeField] GameObject[] makers;
    private float currentTime;
    private int checkDir;

    public bool isStart = false;
    
    void Start()
    {

    }

    void Update()
    {
        if(isStart)
        {
            checkDir = Random.Range(0,2);
            MakeTarget();
        }
    }

    public void GameStart()
    {
        isStart = true;
    }

    public void GameEnd()
    {
        isStart = false;
    }

    void MakeTarget()
    {  
        currentTime += Time.deltaTime;
        if(currentTime >= 3f)
        {
            IMaker maker = makers[checkDir].GetComponent<IMaker>();
            maker.Make();
            currentTime = 0;
        }
    }
}
