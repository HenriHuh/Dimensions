using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools
{
    public enum CollectibleType
    {
        Bun,
    }

    public enum BooleanType
    {
        False,
        True,
        Toggle
    }


    /// <summary>
    /// Get direction by index 0 - 7
    /// </summary>
    public static Vector3 GetOrdinalDirection(int index)
    {
        index = (index + 8) % 8;

        switch (index)
        {
            case 0:
                return Vector3.forward;
            case 1:
                return new Vector3(1, 0, 1);
            case 2:
                return Vector3.right;
            case 3:
                return new Vector3(1, 0, -1);
            case 4:
                return Vector3.back;
            case 5:
                return new Vector3(-1, 0, -1);
            case 6:
                return Vector3.left;
            case 7:
                return new Vector3(-1, 0, 1);
            default:
                return Vector3.zero;
        }
    }
}

public static class ListExtensions
{

    public static T GetRandom<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
            return default(T);

        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}