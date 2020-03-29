using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;

public class SendNumberInput : MonoBehaviour
{
    static SocketIOComponent socket;
    [SerializeField] Text textUpdate;
    [SerializeField] InputField numBerInput;
    [SerializeField] InputField nameInput;

    [SerializeField] GameObject winIMG,loseIMG,guessIMG;

    // Start is called before the first frame update
    void Start()
    {
        nameInput.interactable = true;

        socket = GetComponent<SocketIOComponent>();
        socket.On("Connected", OnConnected);
        socket.On("GameStarted", GameStarted);
        socket.On("TooMuch", TooMuch);
        socket.On("TooLess", TooLess);
        socket.On("win", Win);
    }

    private void OnConnected(SocketIOEvent obj)
    {
        Debug.Log("conected");
    }

    void GameStarted(SocketIOEvent e)
    {
        textUpdate.text = "Game Started";
    }

    void TooMuch(SocketIOEvent e)
    {
        textUpdate.text = e.data["status"].str;
    }

    void TooLess(SocketIOEvent e)
    {
        textUpdate.text = e.data["status"].str;
    }

    void Win(SocketIOEvent e)
    {
        textUpdate.text = "The winner is " + e.data["name"].str;
    }

    public void ClickSendData()
    {
       
        if (nameInput.text != "")
        {
            if (numBerInput.text != "")
            {
                try
                {
                    JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
                    jSONObject.AddField("Name", nameInput.text);
                    jSONObject.AddField("Value", numBerInput.text);

                    socket.Emit("Send", jSONObject);
                    numBerInput.text = "";
                    nameInput.interactable = false;

                }
                catch
                {

                }

            }

        }

    }

}


