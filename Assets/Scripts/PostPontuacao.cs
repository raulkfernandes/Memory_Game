using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class PostPontuacao : MonoBehaviour {

    [SerializeField] private List<PontuacaoFinal> listaPontuacaoFinal;

    private void Awake()
    {
        listaPontuacaoFinal = new List<PontuacaoFinal>();
    }

    public int AdicionarPontucaoFinal (string tempoFinal, int pontosFinal)
    {
        var id = this.listaPontuacaoFinal.Count * UnityEngine.Random.Range(1, 100000);

        var novoColocado = new PontuacaoFinal(id, tempoFinal, pontosFinal);
        this.listaPontuacaoFinal.Add(novoColocado);
        this.listaPontuacaoFinal.Sort();
        StartCoroutine(PostarJson());

        return id;
    }

    IEnumerator PostarJson()
    {
        var textoJson = JsonUtility.ToJson(this);
        {
            using (UnityWebRequest www = UnityWebRequest.Post("https://us-central1-huddle-team.cloudfunctions.net/api/memory/raulfelipe2@gmail.com", textoJson))
            {
                //www.SetRequestHeader("Accept", "application/json");
                //www.uploadHandler.contentType = "application/json";

                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Json upload complete!");
                }
            }
        }
    }
}

[Serializable]
public class PontuacaoFinal : IComparable
{
    public int id;
    public string tempoFinal;
    public int pontosFinal;

    public PontuacaoFinal(int id, string tempoFinal, int pontosFinal)
    {
        this.id = id;
        this.tempoFinal = tempoFinal;
        this.pontosFinal = pontosFinal;
    }

    public int CompareTo(object obj)
    {
        var outroObjeto = obj as PontuacaoFinal;
        return outroObjeto.pontosFinal.CompareTo(this.pontosFinal);
    }
}
