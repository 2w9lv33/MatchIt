using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Match : MonoBehaviour {
    Pics[,] pics = new Pics[8,8];
    public Image[] images = new Image[3];
    public GameObject[] gameObjects = new GameObject[3];
    int r, g, b,n;
    // Use this for initialization
    void Start()
    {
        r = g = b = 12;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                pics[i, j] = new Pics();
                pics[i, j].row = i;
                pics[i, j].col = j;
                pics[i, j].alive = false;
                pics[i, j].pic = 0;
            }
        }
        for (int i = 1; i < 7; i++)
        {
            for (int j = 1; j < 7; j++)
            {
                pics[i, j] = new Pics();
                pics[i, j].row = i;
                pics[i, j].col = j;
                pics[i, j].alive = true;
                n = Random.Range(1, 4);
                while (pics[i, j].pic == 0)
                {
                    if(n == 1 && r != 0)
                    {
                        GameObject prefabInstance = Instantiate(gameObjects[0]);
                        prefabInstance.transform.parent = GameObject.Find("Panel").gameObject.transform;
                        prefabInstance.transform.localPosition = GameObject.Find("Panel").gameObject.transform.localPosition;
                        prefabInstance.GetComponent<Transform>().localScale = Vector3.one;
                        pics[i, j].pic = 1;
                        r--;
                        break;
                    }
                    if (n == 2 && g != 0)
                    {
                        GameObject prefabInstance = Instantiate(gameObjects[1]);
                        prefabInstance.transform.parent = GameObject.Find("Panel").gameObject.transform;
                        prefabInstance.transform.localPosition = GameObject.Find("Panel").gameObject.transform.localPosition;
                        prefabInstance.GetComponent<Transform>().localScale = Vector3.one;
                        pics[i, j].pic = 2;
                        g--;
                        break;
                    }
                    if (n == 3 && b != 0)
                    {
                        GameObject prefabInstance = Instantiate(gameObjects[2]);
                        prefabInstance.transform.parent = GameObject.Find("Panel").gameObject.transform;
                        prefabInstance.transform.localPosition = GameObject.Find("Panel").gameObject.transform.localPosition;
                        prefabInstance.GetComponent<Transform>().localScale = Vector3.one;
                        pics[i, j].pic = 3;
                        b--;
                        break;
                    }
                    n = Random.Range(1, 4); 
                }
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
