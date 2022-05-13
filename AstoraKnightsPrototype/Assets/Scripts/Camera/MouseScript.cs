using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{

    public Texture2D cursorTexture;
    CursorMode cursorMode = CursorMode.ForceSoftware;
    Vector2 hotSpot = Vector2.zero;

    [SerializeField] GameObject mousePoint;
    GameObject instantiateMouse;

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(cursorTexture,hotSpot,cursorMode);

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider is TerrainCollider)
                {
                    Vector3 temp = hit.point;
                    temp.y = 0.25f;

                    if(instantiateMouse == null)
                    {
                        instantiateMouse = Instantiate(mousePoint) as GameObject;
                        instantiateMouse.transform.position = temp;
                    }
                    else
                    {
                        Destroy(instantiateMouse);
                        instantiateMouse = Instantiate(mousePoint) as GameObject;
                        instantiateMouse.transform.position = temp;
                    }

                }
            }

        }

    }
}
