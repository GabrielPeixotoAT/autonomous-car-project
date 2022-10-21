using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConsole : MonoBehaviour
{
    public GameObject MessagePrefab;

    public void WriteMessage(string message, ITypes type)
    {
        MessagePrefab.GetComponent<ConsoleLog>().SetDependecies(message, type);
        var obj = Instantiate(MessagePrefab, gameObject.transform);
        obj.transform.SetAsFirstSibling();
    }
}
