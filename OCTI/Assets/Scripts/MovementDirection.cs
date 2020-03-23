using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MovementDirection
{
    public static Vector2Int Backward
    {
        get
        {
            return new Vector2Int(0, 1);
        }
    }

    public static Vector2Int Forward
    {
        get
        {
            return new Vector2Int(0, -1);
        }
    }

    public static Vector2Int Right
    {
        get
        {
            return new Vector2Int(1, 0);
        }
    }

    public static Vector2Int Left
    {
        get
        {
            return new Vector2Int(-1, 0);
        }
    }

    public static Vector2Int Backwardright
    {
        get
        {
            return new Vector2Int(1, 1);
        }
    }

    public static Vector2Int Backwardleft
    {
        get
        {
            return new Vector2Int(-1, 1);
        }
    }

    public static Vector2Int Forwardright
    {
        get
        {
            return new Vector2Int(1, -1);
        }
    }

    public static Vector2Int Forwardleft
    {
        get
        {
            return new Vector2Int(-1, -1);
        }
    }
}
