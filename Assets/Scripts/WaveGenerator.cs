using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveGenerator : MonoBehaviour
{
    [SerializeField] private Transform cubePrefab;
    [SerializeField, Range(10, 100)] private int resolution;
    [SerializeField] private FunctionLibrary.FunctionName function;

    private Transform[] points;

    private void Awake()
    {
        points = new Transform[resolution * resolution];
    }

    // Start is called before the first frame update
    void Start()
    {
        WaveInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        float time = Time.time;
        float step = 2f / resolution;

        float v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            { 
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }

            float u = (x + 0.5f) * step - 1f;

            points[i].localPosition = f(u, v, time);
        }
    }

    private void WaveInitialization()
    {
        float step = 2f / resolution;
        var scale = Vector3.one * step;

        for (int i = 0; i < points.Length; i++)
        {
            Transform temp = points[i] = Instantiate(cubePrefab);
            temp.localScale = scale;

            temp.SetParent(transform, false);
        }
    }
}
