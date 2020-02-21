using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems
{
    /// <summary>
    /// A basic keyboard implementation of the IInput interface for all the input information a kart needs.
    /// </summary>
    public class TouchInput : MonoBehaviour, IInput
    {
        public float Acceleration
        {
            get { return m_Acceleration; }
        }
        public float Steering
        {
            get { return m_Steering; }
        }
        public bool BoostPressed
        {
            get { return m_BoostPressed; }
        }
        public bool FirePressed
        {
            get { return m_FirePressed; }
        }
        public bool HopPressed
        {
            get { return m_HopPressed; }
        }
        public bool HopHeld
        {
            get { return m_HopHeld; }
        }

        float m_Acceleration;
        float m_Steering;
        bool m_HopPressed;
        bool m_HopHeld;
        bool m_BoostPressed;
        bool m_FirePressed;

        bool m_FixedUpdateHappened;

        private void Start()
        {
            m_Acceleration = 1f;
        }

        void Update()
        {
            // accelerates automatically
            m_Acceleration = 1f;

            if (Input.touchCount > 0)
            {
                // steering
                Touch touch = Input.GetTouch(0);
                if (touch.position.x < 0.5f * Screen.width)
                {
                    m_Steering = -0.5f;
                    if (m_Steering < -1f)
                        m_Steering = -1f;
                }
                else if (touch.position.x > 0.5f * Screen.width)
                {
                    m_Steering = 0.5f;
                    if (m_Steering > 1f)
                        m_Steering = 1f;
                }

                // hop
                if (touch.position.y > 0.5f * Screen.height)
                {
                    m_HopHeld = true;
                }
            }
            else
            {
                m_Steering = 0f;
                m_HopHeld = false;
                m_HopPressed = false;
            }
            
            if (m_FixedUpdateHappened)
            {
                m_FixedUpdateHappened = false;

                m_HopPressed = false;
            }

            // tap to single hop
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && touch.position.y > 0.5f * Screen.height)
                {
                    m_HopPressed = true;
                }
            }

            // we don't use these
            m_FirePressed = false;
            m_BoostPressed = false;
        }

        void FixedUpdate()
        {
            m_FixedUpdateHappened = true;
        }
    }
}
