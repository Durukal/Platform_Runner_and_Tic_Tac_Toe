using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int n = 3;
    private GameObject[,] grid;

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new GameObject[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                GameObject cell = new GameObject("cell" + i + j);
                cell.AddComponent<SpriteRenderer>();
                cell.transform.position = new Vector3(i, j, 0);
                grid[i, j] = cell;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.FloorToInt(mousePos.x);
            int y = Mathf.FloorToInt(mousePos.y);

            if (x >= 0 && x < n && y >= 0 && y < n)
            {
                GameObject cell = grid[x, y];
                cell.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("X");

                List<GameObject> toRemove = new List<GameObject>();
                toRemove.AddRange(GetHorizontal(x, y));
                toRemove.AddRange(GetVertical(x, y));

                foreach (GameObject c in toRemove)
                {
                    grid[(int)c.transform.position.x, (int)c.transform.position.y] = null;
                    Destroy(c);
                }
            }
        }
    }

    List<GameObject> GetHorizontal(int x, int y)
    {
        List<GameObject> result = new List<GameObject>();
        int count = 0;

        for (int i = x; i >= 0; i--)
        {
            if (grid[i, y] == null)
                break;

            count++;
        }

        for (int i = x + 1; i < n; i++)
        {
            if (grid[i, y] == null)
                break;

            count++;
        }

        if (count >= 3)
        {
            for (int i = x; i >= x - count + 1; i--)
            {
                result.Add(grid[i, y]);
            }
        }

        return result;
    }

    List<GameObject> GetVertical(int x, int y)
    {
        List<GameObject> result = new List<GameObject>();
        int count = 0;

        for (int i = y; i >= 0; i--)
        {
            if (grid[x, i] == null)
                break;

            count++;
        }

        for (int i = y + 1; i < n; i++)
        {
            if (grid[x, i] == null)
                count++;
        }

        if (count >= 3)
        {
            for (int i = y; i >= y - count + 1; i--)
            {
                result.Add(grid[x, i]);
            }
        }

        return result;
    }
}dd
