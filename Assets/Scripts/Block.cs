using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class Block : MonoBehaviour
{
    public Material[] mat;
    float lastFall = 0;
    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector3 v = gridMatrix.roundVec3(child.position);
            if (!gridMatrix.insideBorder(v))
                return false;
            if (gridMatrix.grid[(int)v.x, (int)v.y, (int)v.z] != null &&
                gridMatrix.grid[(int)v.x, (int)v.y, (int)v.z].parent != transform)
                return false;
        }
        return true;
    }
    
    void updateGrid()
    {
        for(int y=0;y<gridMatrix.h ; y++)
            for(int x=0;x<gridMatrix.w ; x++)
                for(int z=0;z<gridMatrix.b ; z++)
                {
                    if (gridMatrix.grid[x, y, z] != null)
                        if (gridMatrix.grid[x, y, z].parent == transform)
                            gridMatrix.grid[x, y, z] = null;
                }
        foreach (Transform child in transform)
        {
            Vector3 v = gridMatrix.roundVec3(child.position);
            gridMatrix.grid[(int)v.x, (int)v.y, (int)v.z] = child;
        }
    }
    void moveX()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (isValidGridPos())
                updateGrid();
            else
                transform.position += new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (isValidGridPos())
                updateGrid();
            else
                transform.position += new Vector3(-1, 0, 0);
        }
    }
    void moveZ()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -1);
            if (isValidGridPos())
               updateGrid();
            else
                transform.position += new Vector3(0, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, 1);
            if (isValidGridPos())
                updateGrid();
            else
                transform.position += new Vector3(0, 0, -1);
        }
    }

    void rotBlock()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(0, 0, -90);
            if (isValidGridPos())
                updateGrid();
            else
                transform.Rotate(0, 0, 90);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(-90, 0, 0);
            if (isValidGridPos())
                updateGrid();
            else
                transform.Rotate(90, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.Rotate(0, 90, 0);
            if (isValidGridPos())
                updateGrid();
            else
                transform.Rotate(0, -90, 0);
        }
    }
    void Fall()
    {
        if(Input.GetKeyDown(KeyCode.X) || Time.time - lastFall >=1)
        {
            transform.position += new Vector3(0, -1, 0);
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                gridMatrix.deleteFullLayer();
                FindObjectOfType<Spawner>().spawnNext(); //
                enabled = false;
            }
            lastFall = Time.time;
        }
    }
    void Start()
    {
        int ind = Random.Range(0, mat.Length);
        int ran1 = Random.Range(0, 3);
        int ran2 = Random.Range(0, 3);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            print(i);
            GameObject Go = gameObject.transform.GetChild(i).gameObject;
            Go.GetComponent<Renderer>().material = mat[ind];
        }
        transform.Rotate(90*ran1, 0, 90*ran2);
    }
    void Update()
    {
        moveX();
        moveZ();
        rotBlock();
        Fall();
        if (!isValidGridPos())
        {
            Spawner.GO = "GAME OVER";
            Spawner.never_started = true;

            gridMatrix.score = 0;

            Invoke("feedDog", 5);

            Destroy(gameObject);
        }
    }
    void feedDog()
    {
        SceneManager.LoadScene(0);
    }
}
