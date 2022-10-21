using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleLog : MonoBehaviour
{
    public string Message;
    public ITypes Type;

    void DestroyItSelf()
    {
        Destroy(gameObject);
    }

    public void SetDependecies(string message, ITypes type)
    {
        Message = message;
        Type = type;

        gameObject.GetComponent<TMP_Text>().text = Message;
        gameObject.GetComponent<TMP_Text>().color = Type.GetMessageTheme();
    }
}
