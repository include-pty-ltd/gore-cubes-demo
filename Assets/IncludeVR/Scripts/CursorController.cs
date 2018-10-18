using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Include.UnityScript
{
    public class CursorController : MonoBehaviour
    {

        public bool visible;
        public bool grow;
        public Color color;

        Renderer r;
        Animator a;

        // Use this for initialization
        void Awake()
        {
            r = GetComponent<Renderer>();
            a = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (visible && !r.enabled)
            {
                r.enabled = true;
            }
            else if (!visible && r.enabled)
            {
                r.enabled = false;
            }
            if (r.material.color != color)
            {
                r.material.color = color;
            }
            //if (grow)
            //{
            //    a.SetFloat("GrowRate", 0.5f);
            //} else
            //{
            //    a.SetFloat("GrowRate", -0.5f);
            //}
        }
    }
}