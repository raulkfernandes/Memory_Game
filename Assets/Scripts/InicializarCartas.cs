using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InicializarCartas : MonoBehaviour {

    [SerializeField]
    private Material[] figuras;
    private GameObject[] cartas;
    private int[] indices;

    void Start()
    {
        cartas = new GameObject[this.transform.childCount];
        indices = new int[cartas.Length];

        AtribuirCartas();
        RandomizarFiguras();
	}

    private void AtribuirCartas()
    {
        for (int i = 0; i < cartas.Length; i++)
        {
            cartas[i] = transform.GetChild(i).gameObject;
        }
    }

    void MisturarIndices(int[] indices)
    {
        for (int t = 0; t < indices.Length; t++)
        {
            int tmp = indices[t];
            int r = UnityEngine.Random.Range(t, indices.Length);
            indices[t] = indices[r];
            indices[r] = tmp;
        }
    }

    private void RandomizarFiguras()
    {
        for (int i = 0; i < cartas.Length; i++)
        {
            indices[i] = i;
        }

        MisturarIndices(indices);

        for (int i = 0; i < cartas.Length; i ++)
        {
            Transform child = cartas[i].transform.GetChild(0);
            child.GetComponent<MeshRenderer>().material = figuras[indices[i]];
        }
    }

}
