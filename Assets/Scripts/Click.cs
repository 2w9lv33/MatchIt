﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MatchIt
{
    public class Click : MonoBehaviour
    {
        //GraphicRaycaster
        static GraphicRaycaster m_Raycaster;
        //PointerEventData
        static PointerEventData m_PointerEventData;
        //EventSystem
        static EventSystem m_EventSystem;

        //return Pics be Clicked
        public static Pics ClickOn()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    //return Pics
                    return result.gameObject.GetComponent<Pics>();
                }
            }
            return null;
        }
        void Start()
        {
            //Fetch the Raycaster from the GameObject (the Canvas)
            m_Raycaster = GetComponent<GraphicRaycaster>();
            //Fetch the Event System from the Scene
            m_EventSystem = GetComponent<EventSystem>();
        }

        void Update()
        {

        }
    }
}
