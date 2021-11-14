using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{  
    GameObject player;
    UIManager ui;
    [SerializeField] Material[] materials;
    private int colorNum;
    Renderer rend;
 
    void Awake()
    {
        rend = GetComponent<Renderer>();
        player = FindObjectOfType<UIManager>().gameObject;
        ui = player.GetComponent<UIManager>();
        colorNum = Random.Range(0,3);
        rend.material = materials[colorNum];
    }

    void Hit()
    {
        ui.GetScore(1);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Renderer _rend = other.gameObject.GetComponent<Renderer>();
            if(_rend.material.color == rend.material.color)
            {
                Hit();
            }
        }

        if(other.tag == "Plane")
        {
            ui.DamageHP(10);
            Destroy(gameObject);
        }
    }
}
