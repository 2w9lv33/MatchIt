using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace MatchIt {

    public class Match : MonoBehaviour {
        //new pics
        Pics[,] pics = new Pics[8, 8];
        //images
        public Image[] images = new Image[3];
        //get prefabs
        public GameObject[] gameObjects = new GameObject[3];
        //flags
        int r, g, b, n;
        //temps
        Pics[] tempPics = new Pics[2];
        // check if hold a picture 0 = none ,1 = one ,2 = two
        int hold;
        //test ray And camera
        Ray ray;
        public Camera _CurCamera;
        // Use this for initialization
        void Start()
        {
            hold = 0;
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
                        if (n == 1 && r != 0)
                        {
                            GameObject prefabInstance = Instantiate(gameObjects[0]);
                            prefabInstance.transform.parent = GameObject.Find("Panel").gameObject.transform;
                            prefabInstance.transform.localPosition = GameObject.Find("Panel").gameObject.transform.localPosition;
                            prefabInstance.GetComponent<Transform>().localScale = Vector3.one;

                            prefabInstance.GetComponent<Pics>().row = i;
                            prefabInstance.GetComponent<Pics>().col = j;
                            prefabInstance.GetComponent<Pics>().alive = true;
                            prefabInstance.GetComponent<Pics>().pic = 1;
                            prefabInstance.GetComponent<Pics>().image = prefabInstance;

                            pics[i, j].pic = 1;
                            pics[i, j].image = prefabInstance;
                            r--;
                            break;
                        }
                        if (n == 2 && g != 0)
                        {
                            GameObject prefabInstance = Instantiate(gameObjects[1]);
                            prefabInstance.transform.parent = GameObject.Find("Panel").gameObject.transform;
                            prefabInstance.transform.localPosition = GameObject.Find("Panel").gameObject.transform.localPosition;
                            prefabInstance.GetComponent<Transform>().localScale = Vector3.one;

                            prefabInstance.GetComponent<Pics>().row = i;
                            prefabInstance.GetComponent<Pics>().col = j;
                            prefabInstance.GetComponent<Pics>().alive = true;
                            prefabInstance.GetComponent<Pics>().pic = 2;
                            prefabInstance.GetComponent<Pics>().image = prefabInstance;

                            pics[i, j].pic = 2;
                            pics[i, j].image = prefabInstance;
                            g--;
                            break;
                        }
                        if (n == 3 && b != 0)
                        {
                            GameObject prefabInstance = Instantiate(gameObjects[2]);
                            prefabInstance.transform.parent = GameObject.Find("Panel").gameObject.transform;
                            prefabInstance.transform.localPosition = GameObject.Find("Panel").gameObject.transform.localPosition;
                            prefabInstance.GetComponent<Transform>().localScale = Vector3.one;

                            prefabInstance.GetComponent<Pics>().row = i;
                            prefabInstance.GetComponent<Pics>().col = j;
                            prefabInstance.GetComponent<Pics>().alive = true;
                            prefabInstance.GetComponent<Pics>().pic = 3;
                            prefabInstance.GetComponent<Pics>().image = prefabInstance;

                            pics[i, j].pic = 3;
                            pics[i, j].image = prefabInstance;
                            b--;
                            break;
                        }
                        n = Random.Range(1, 4);
                    }
                }
            }
        }
        // Update is called once per frame
        void Update() {
            if (Input.GetMouseButtonDown(0))
            {
                //todo
                ray = _CurCamera.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(_CurCamera.transform.position, ray.direction * 100, Color.green, 10);
                if (Click.ClickOn()!=null)
                {
                    tempPics[hold] = Click.ClickOn();
                    print("row:" + tempPics[hold].row + " col:" + tempPics[hold].col);
                    hold++;
                }
            }
            if (hold == 2)
            {
                //todo
                if (tempPics[0] != tempPics[1])
                {
                    List<int> p1c = CatchColInRow(pics, tempPics[0]);
                    List<int> p1r = CatchRowInCol(pics, tempPics[0]);
                    List<int> p2c = CatchColInRow(pics, tempPics[1]);
                    List<int> p2r = CatchRowInCol(pics, tempPics[1]);
                    List<int> samecol = CatchSameCol(p1c, p2c);
                    List<int> samerow = CatchSameRow(p1r, p2r);
                    if (MatchStart(samerow, samecol, pics, tempPics[0], tempPics[1]))
                    {
                        tempPics[0].GetComponent<Pics>().image.GetComponent<Image>().color = Color.white;
                        tempPics[0].GetComponent<Pics>().alive = false;
                        tempPics[1].GetComponent<Pics>().image.GetComponent<Image>().color = Color.white;
                        tempPics[1].GetComponent<Pics>().alive = false;
                    }
                }
                hold = 0;
            }
        }

        List<int> CatchColInRow(Pics[,] pics, Pics pic)
        {
            int row, col;
            col = pic.col;
            row = pic.row;
            pics[row, col].alive = false;
            List<int> list = new List<int>();
            while (col >= 0 && !pics[row, col].alive)
            {
                list.Add(col);
                col--;
            }
            col = pic.col;
            row = pic.row;
            col++;
            while (col <= 7 && !pics[row, col].alive)
            {
                list.Add(col);
                col++;
            }
            list.Sort();
            return list;
        }

        List<int> CatchRowInCol(Pics[,] pics, Pics pic)
        {
            int row, col;
            col = pic.col;
            row = pic.row;
            pics[row, col].alive = false;
            List<int> list = new List<int>();
            while (row >= 0 && !pics[row, col].alive)
            {
                list.Add(row);
                row--;
            }
            col = pic.col;
            row = pic.row;
            row++;
            while (row <= 7 && !pics[row, col].alive)
            {
                list.Add(row);
                row++;
            }
            list.Sort();
            return list;
        }

        List<int> CatchSameCol(List<int> col1, List<int> col2)
        {
            List<int> list = new List<int>();
            foreach (int col_1 in col1)
            {
                foreach (int col_2 in col2)
                {
                    if (col_1 == col_2) list.Add(col_1);
                }
            }
            return list;
        }

        List<int> CatchSameRow(List<int> row1, List<int> row2)
        {
            List<int> list = new List<int>();
            foreach (int row_1 in row1)
            {
                foreach (int row_2 in row2)
                {
                    if (row_1 == row_2) list.Add(row_1);
                }
            }
            return list;
        }

        bool MatchStart(List<int> row, List<int> col, Pics[,] pics, Pics pic1, Pics pic2)
        {
            int r, c;
            int row1, col1,row2,col2;
            col1 = pic1.col;
            row1 = pic1.row;
            col2 = pic2.col;
            row2 = pic2.row;
            if (pic1.pic == pic2.pic)
            {
                foreach (int _row in row)
                {
                    if (pic1.col <= pic2.col)
                    {
                        r = _row;
                        c = pic2.col;
                        while (!pics[r, c].alive)
                        {
                            c--;
                            if (c < 0) break;
                        }
                        c++;
                        if (c <= pic1.col) return true;
                    }
                    else
                    {
                        r = _row;
                        c = pic1.col;
                        while (!pics[r, c].alive)
                        {
                            c--;
                            if (c < 0) break;
                        }
                        c++;
                        if (c <= pic2.col) return true;
                    }
                }

                foreach (int _col in col)
                {
                    if (pic1.row <= pic2.row)
                    {
                        r = pic2.row;
                        c = _col;
                        while (!pics[r, c].alive)
                        {
                            r--;
                            if (r < 0) break;
                        }
                        r++;
                        if (r <= pic1.row) return true;
                    }
                    else
                    {
                        r = pic1.row;
                        c = _col;
                        while (!pics[r, c].alive)
                        {
                            r--;
                            if (r < 0) break;
                        }
                        r++;
                        if (r <= pic2.row) return true;
                    }
                }
            }
            pics[row1,col1].alive = true;
            pics[row2,col2].alive = true;
            return false;
        }
    }
}