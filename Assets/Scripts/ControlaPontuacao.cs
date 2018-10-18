using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControlaPontuacao : MonoBehaviour {

    [HideInInspector]
    public int pontos = 0;
    [HideInInspector]
    public int numAcertos = 0;
    private ControlaInterface controlaInterface;

    private void Start()
    {
        controlaInterface = FindObjectOfType<ControlaInterface>() as ControlaInterface;
        this.pontos = 0;
        this.numAcertos = 0;
    }
    

    public bool ChecarAcerto(Transform carta1, Transform carta2)
    {
        MeshRenderer childRenderer1 = carta1.GetChild(0).GetComponent<MeshRenderer>();
        MeshRenderer childRenderer2 = carta2.GetChild(0).GetComponent<MeshRenderer>();

        bool acertou = childRenderer1.material.name == childRenderer2.material.name;

        if (acertou)
        {
            numAcertos++;
            AdicionarPontos();
            controlaInterface.AtualizaPontuacao(this.pontos);
            return true;
        }
        else
        {
            RetirarPontos();
            controlaInterface.AtualizaPontuacao(this.pontos);
            return false;
        }
    }

    private void AdicionarPontos ()
    {
        this.pontos += 50;
    }

    private void RetirarPontos ()
    {
        this.pontos -= 5;
    }
}