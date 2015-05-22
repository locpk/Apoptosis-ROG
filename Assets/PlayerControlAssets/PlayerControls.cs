using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.EventSystems;
public class PlayerControls : MonoBehaviour
{

    // the speed at which the camera moves;
    public float cameraSpeed;
    // the unit selector
    public GameObject unitSelector;
    // the target selector
    public GameObject targetSelector;
    // the list of selected units
    public System.Collections.Generic.List<GameObject> selectedUnits;

    public System.Collections.Generic.List<GameObject>[] groups;
    // the list of selected targets
    public System.Collections.Generic.List<GameObject> selectedTargets;
    // the units movement script
    public Cell cellMover;

    public float sW = 5;

    public GameObject pin;
    public Texture selectTexture;
    public Texture targetTexture;
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 1.0f;
    //PUBLIC FUNCTIONS-----------------------------



    bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }


    public void SelectingSameType()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            selectedUnits.Clear();
            System.Collections.Generic.List<GameObject> cells = GameObject.FindGameObjectsWithTag("Unit").ToList();

            foreach (var item in cells)
            {
                if (item.gameObject.name == hit.collider.gameObject.name)
                {
                    selectedUnits.Add(item);
                }
            }
        }


    }

    public void SelectN()
    {

        selectedUnits.Clear();
        System.Collections.Generic.List<GameObject> cells = GameObject.FindGameObjectsWithTag("Unit").ToList();

        foreach (var item in cells)
        {
            if (item.GetComponent<PhotonView>().isMine && item.gameObject.name == "Neutral Cell")
            {
                selectedUnits.Add(item);
            }
        }

    }

    public void SelectH()
    {

        selectedUnits.Clear();
        System.Collections.Generic.List<GameObject> cells = GameObject.FindGameObjectsWithTag("Unit").ToList();

        foreach (var item in cells)
        {
            if (item.GetComponent<PhotonView>().isMine && item.gameObject.name == "Heat Cell")
            {
                selectedUnits.Add(item);
            }
        }

    }


    public void SelectC()
    {

        selectedUnits.Clear();
        System.Collections.Generic.List<GameObject> cells = GameObject.FindGameObjectsWithTag("Unit").ToList();

        foreach (var item in cells)
        {
            if (item.GetComponent<PhotonView>().isMine && item.gameObject.name == "Cold Cell")
            {
                selectedUnits.Add(item);
            }
        }

    }



    public void StopAllUnits()
    {
        foreach (var item in selectedUnits)
        {
            item.GetComponent<Cell>().SetTarget(null);
            item.GetComponent<Cell>().SetDestination(item.transform.position);
        }
    }

    public void DuplicateCells()
    {
        foreach (var item in selectedUnits)
        {
            if (item.GetComponent<Cell>().m_currentProteins > 0)
            {
                item.GetComponent<Split>().Divide();
            }
        }
    }
    //END OF PUBLIC FUNCTIONS----------------------


    // Use this for initialization
    void Start()
    {
        selectedUnits.Clear();
        selectedTargets.Clear();
    }

    void OnGUI()
    {
        foreach (var item in selectedUnits)
        {
            if (item != null)
            {
                Vector3 guiPosition = Camera.main.WorldToScreenPoint(item.transform.position);
                Rect rect = new Rect(guiPosition.x - 10, -(guiPosition.y - Screen.height) - 10, 20, 20);
                GUI.DrawTexture(rect, selectTexture);
            }

        }
        foreach (var item in selectedTargets)
        {
            if (item != null)
            {
                Vector3 guiPosition = Camera.main.WorldToScreenPoint(item.transform.position);
                Rect rect = new Rect(guiPosition.x - 10, -(guiPosition.y - Screen.height) - 10, 20, 20);
                GUI.DrawTexture(rect, targetTexture);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {

        //select units
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            
            if (DoubleClick())
            {
                SelectingSameType();
                clicked = 0;
            }

            if (Input.GetMouseButton(0) && clicked != 0) 
            {
                InstantiateUnitSelector();
                
            }
            else if (GameObject.FindGameObjectsWithTag("UnitSelector").Length > 0)
            {
                SelectUnits();
                Destroy(GameObject.FindGameObjectWithTag("UnitSelector"));
            }
                
           



        }
        //camera scroll
        if (Input.GetKey(KeyCode.F))
            ScrollCamera();
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.M))
            {
                InstantiateTargetSelector();
            }
            else if (GameObject.FindGameObjectsWithTag("TargetSelector").Length > 0)
            {
                //set targets
                if (selectedUnits.Count > 0)
                {
                    SelectTargets();
                }
                //set waypoint
                if (selectedTargets.Count == 0)
                {
                    foreach (var item in selectedTargets)
                    {
                        item.GetComponent<Cell>().SetTarget(null);
                    }
                    SetWaypoint();

                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        GameObject.Instantiate(pin, new Vector3(hit.point.x + 0.5f, -1.0f, hit.point.z + 0.5f), Quaternion.Euler(90, 0, 0));
                    }


                }
                Destroy(GameObject.FindGameObjectWithTag("TargetSelector"));
            }
        }
        //multiply selected units
        if (Input.GetKeyUp(KeyCode.D))
        {
            DuplicateCells();
        }
        //stop all actions
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopAllUnits();
        }
        foreach (var item in selectedUnits)
        {
            if (item == null)
            {
                selectedUnits.Remove(item);
            }
        }

        foreach (var item in selectedTargets)
        {
            if (item == null)
            {
                selectedTargets.Remove(item);
            }
        }
    }

    void ScrollCamera()
    {
        //move camera
        Vector3 toPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        toPos.y = Camera.main.transform.position.y + (Input.GetAxis("Vertical") * Camera.main.transform.position.y);
        Vector3 fromPos = Camera.main.transform.position;
        Camera.main.transform.position = Vector3.MoveTowards(fromPos, toPos, Time.deltaTime * cameraSpeed * Vector3.Distance(fromPos, toPos));

        //zoom in
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0) //zoom out
        {
            Camera.main.orthographicSize--;
        }
        if (Camera.main.orthographicSize < 5)
        {
            Camera.main.orthographicSize = 5;
        }

    }

    void InstantiateUnitSelector()
    {

        if (GameObject.FindGameObjectsWithTag("UnitSelector").Length == 0)
        {
            Vector3 instantiateAtPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            instantiateAtPos.y = 0;
            Instantiate(unitSelector, instantiateAtPos, Quaternion.Euler(90, 0, 0));
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        mousePos.y = 0;
        GameObject.FindGameObjectWithTag("UnitSelector").GetComponent<SphereCollider>().radius = Vector3.Distance(mousePos, GameObject.FindGameObjectWithTag("UnitSelector").transform.position);
        GameObject.FindGameObjectWithTag("UnitSelector").transform.localScale = Vector3.one * GameObject.FindGameObjectWithTag("UnitSelector").GetComponent<SphereCollider>().radius * 10;

    }

    void InstantiateTargetSelector()
    {
        if (GameObject.FindGameObjectsWithTag("TargetSelector").Length == 0)
        {
            Vector3 instantiateAtPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            instantiateAtPos.y = 0;
            Instantiate(targetSelector, instantiateAtPos, Quaternion.Euler(90, 0, 0));
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        mousePos.y = 0;
        GameObject.FindGameObjectWithTag("TargetSelector").GetComponent<SphereCollider>().radius = Vector3.Distance(mousePos, GameObject.FindGameObjectWithTag("TargetSelector").transform.position);
        GameObject.FindGameObjectWithTag("TargetSelector").transform.localScale = Vector3.one * GameObject.FindGameObjectWithTag("TargetSelector").GetComponent<SphereCollider>().radius * 10;
    }

    void SelectUnits()
    {

        selectedUnits.Clear();
        System.Collections.Generic.List<GameObject> cells = GameObject.FindGameObjectsWithTag("Unit").ToList();
        for (int i = 0; i < cells.Count; i++)
        {
            Vector3 unitPos = new Vector3(cells[i].transform.position.x, 0, cells[i].transform.position.z);
            if (GameObject.FindGameObjectWithTag("UnitSelector").GetComponent<SphereCollider>().radius >= Vector3.Distance(GameObject.FindGameObjectWithTag("UnitSelector").transform.position, unitPos) - cells[i].GetComponent<SphereCollider>().radius * 4)
            {
                selectedUnits.Add(cells[i]);
            }
        }
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            while (i < selectedUnits.Count && !selectedUnits[i].GetComponent<PhotonView>().isMine)
            {
                selectedUnits.RemoveAt(i);
            }
        }


    }

    void SelectTargets()
    {
        selectedTargets.Clear();
        System.Collections.Generic.List<GameObject> targets = GameObject.FindGameObjectsWithTag("Target").ToList();

        for (int i = 0; i < targets.Count; i++)
        {
            targets[i] = targets[i].transform.parent.gameObject;
        }

        for (int i = 0; i < targets.Count; i++)
        {
            Vector3 targetPos = new Vector3(targets[i].transform.position.x, 0, targets[i].transform.position.z);
            if (GameObject.FindGameObjectWithTag("TargetSelector").GetComponent<SphereCollider>().radius >= Vector3.Distance(GameObject.FindGameObjectWithTag("TargetSelector").transform.position, targetPos) - targets[i].GetComponent<SphereCollider>().radius * 4)
            {
                selectedTargets.Add(targets[i]);
            }
        }

        for (int i = 0; i < selectedTargets.Count; i++)
        {
            while (i < selectedTargets.Count && selectedTargets[i].tag != "Protein" && selectedTargets[i].GetComponent<PhotonView>().isMine)
            {
                selectedTargets.RemoveAt(i);
            }
        }

        foreach (var item in selectedUnits)
        {
            if (selectedTargets.Count > 0)
            {

                item.GetComponent<Cell>().SetTarget(selectedTargets[0]);
            }
        }
    }

    void SetWaypoint()
    {
        foreach (var item in selectedUnits)
        {
            item.GetComponent<Cell>().SetTarget(null);
            item.GetComponent<Cell>().SetDestination();
        }
    }
}
