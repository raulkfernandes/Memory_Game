using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirarNoClique : MonoBehaviour
{
    private Transform[] baralho = new Transform[2];
    private int contadorDeCliques = 0;
    private ControlaPontuacao controlaPontuacao;
    private ControlaInterface controlaInterface;
    [SerializeField]
    private float tempoParaComecar = 3f;

    private void Start()
    {
        controlaPontuacao = FindObjectOfType<ControlaPontuacao>() as ControlaPontuacao;
        controlaInterface = FindObjectOfType<ControlaInterface>() as ControlaInterface;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad >= tempoParaComecar)
        {
            controlaInterface.AtualizaCronometro(Time.time);

            if (contadorDeCliques < 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.DrawRay(raio.origin, raio.direction * 200, Color.red);
                    RaycastHit hit;

                    if (Physics.Raycast(raio, out hit))
                    {
                        VirarCarta(hit.transform);
                    }
                }
            }
        }
    }

    private void VirarCarta(Transform hitTransform)
    {
        baralho[contadorDeCliques] = hitTransform;
        contadorDeCliques++;
        hitTransform.GetComponent<Animator>().SetInteger("contadorDeCliques", 1);

        if(contadorDeCliques == 2)
        {
            if(controlaPontuacao.ChecarAcerto(baralho[0], baralho[1]))
            {
                contadorDeCliques = 0;
            }
            else
            {
                StartCoroutine(DesvirarCartas());
            }
        }
    }

    IEnumerator DesvirarCartas()
    {
        yield return new WaitForSeconds(2);
        baralho[0].GetComponent<Animator>().SetInteger("contadorDeCliques", 3);
        baralho[1].GetComponent<Animator>().SetInteger("contadorDeCliques", 3);
        contadorDeCliques = 0;
    }

}
