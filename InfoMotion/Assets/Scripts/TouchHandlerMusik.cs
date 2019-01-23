using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwipeMenu
{
    
    /// <summary>
    /// Handles touches seperate from swipes. Supports mouse and mobile touch controls.
    /// If a menu item is selected and isn't centred, then the menu item is animated to centre. If
    /// a menu item is centred than its <see cref="MenuItem.OnClick"/> is invoked.
    /// </summary>
    /// 
	public class TouchHandlerMusik : MonoBehaviour {

        /// <summary>
        /// If true, menu selection is handled.
        /// </summary>
        public bool handleTouches = true;

        /// <summary>
        /// The selected menu item has to be centred for selectiion to occur.
        /// </summary>
        public bool requireMenuItemToBeCentredForSelectiion = true;

        private SwipeHandlerMusik _swipeHandler;

        void Start()
        {
			_swipeHandler = GetComponent<SwipeHandlerMusik>();
        }

        void LateUpdate()
        {
            if (!handleTouches)
                return;

            if (_swipeHandler && _swipeHandler.isSwiping)
            {
                return;
            }
           
            if (Input.GetMouseButtonUp(0) && Helper.GetMouseAxis(MouseAxis.x) == 0)
            {
                CheckTouch(Input.mousePosition);
            }
        }

		public void CheckTouch(Vector3 screenPoint)
        {
            Ray touchRay = Camera.main.ScreenPointToRay(screenPoint);
            RaycastHit hit;

            Physics.Raycast(touchRay, out hit);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("MenuItem"))
            {

                var item = hit.collider.GetComponent<MenuItem>();

                if (Menu.instance.MenuCentred(item))
                {

                    Menu.instance.ActivateSelectedMenuItem(item);

                }
                /* else
                {
                    Menu.instance.AnimateToTargetItem(item);

                    if (!requireMenuItemToBeCentredForSelectiion)
                    {
                        Menu.instance.ActivateSelectedMenuItem(item);
                    }
                }
                */
            }
        }

    }
}


