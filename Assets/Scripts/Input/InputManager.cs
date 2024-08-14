using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public Vector2 HorizontalDirection { get; private set; }
        public Action OnFireEvent;

        [SerializeField] private InputConfig _inputConfig;

        private void Update()
        {
            CheckFireKey();
            SetDirection();
        }

        private void CheckFireKey()
        {
            if (Input.GetKeyDown(_inputConfig.FireKey))
                OnFireEvent?.Invoke();
        }


        private void SetDirection()
        {
            float xPos = 0f;
            if (Input.GetKey(_inputConfig.LeftKey))
            {
                xPos = -1;
            }
            else if (Input.GetKey(_inputConfig.RightKey))
            {
                xPos = 1;
            }
            else
            {
                xPos = 0;
            }

            this.HorizontalDirection = new(xPos, 0);
        }
    }
}