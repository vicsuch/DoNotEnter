using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentoInfo
{
    public string[] nombre;
    public float[] time;
    public int[] monedas;

    public int[] TopScores(int cantScores)
    {
        if (monedas.Length == 0) { return new int[0]; }
        if (cantScores > monedas.Length) { cantScores = monedas.Length; }
        List<int> top3 = new List<int>();
        for(int i = 0; i < cantScores; i++)
        {
            int max = 0;
            while(AlreadyInside(max, top3) && max < monedas.Length)
            {
                max++;
            }
            for (int j = 1; j < monedas.Length; j++)
            {
                if (monedas[j] > monedas[max] && !AlreadyInside(j, top3))
                {
                    max = j;
                }
            }
            if(AlreadyInside(max, top3))
            {
                break;
            }
            else
            {
                top3.Add(max);
            }
        }
        int[] output = new int[top3.Count];
        for(int i = 0; i < output.Length; i++)
        {
            output[i] = top3[i];
        }
        return output;
    }

    public bool AlreadyInside(int index, List<int> top3)
    {
        for (int j = 0; j < top3.Count; j++)
        {
            if (index == top3[j])
            {
                return true;
            }
        }
        return false;
    }
}
