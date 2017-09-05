using UnityEngine;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using System;
using System.Text;
using BestHTTP.SignalR;
using BestHTTP;
using BestHTTP.SignalR.Hubs;
using BestHTTP.SignalR.Messages;
using BestHTTP.SignalR.JsonEncoders;
using System.Collections.Generic;



    public class ConnectionManager : MonoBehaviour
    {
        public static ConnectionManager Instance;
        string HUB_NAME ="SignalRDemo";
        string CLIENTID = "ClientId";
        string GETREQUEST = "GetRequest";
        string ACK_CONNECTED = "receiveAcknowledgement";
        string CHALLENGEACCEPTED = "ChallengeAccepted";
        string INPUTRECIVEC = "OnInputRecived";
//       string baseUrl = "http://52.33.40.224/SignalRDemo\";// "http://localhost:1921/SignalRDemo";// "http://52.33.40.224/SignalRDemo";//"http://localhost:1921/SignalRDemo";
     string baseUrl = "http://52.33.40.224/SignalRDemo";
        public string friedID ="2";
        public string myID = "1";
        public enum SignalRConectionStatus
        {
            None = 0,
            DisConnected,
            Connected,
        }
        private SignalRConectionStatus curSignalRConectionStatus;
        public static Connection signalRConnection;
        public static Hub _newHub;
        public Coroutine signalRCoroutine;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            } else
            {
                DestroyImmediate(this.gameObject);
                return;
            }
            
        }

        public void MakeConnection ()
        {
            signalRConnection = null;
       
            if (signalRConnection == null) {
                try {
                    Uri uri = new Uri (baseUrl);
                    _newHub = new Hub (HUB_NAME);
                    signalRConnection = new Connection (uri, _newHub);
                    signalRConnection.JsonEncoder = new LitJsonEncoder ();

                    signalRConnection.OnStateChanged += OnSignalRStatusChange;
                    signalRConnection.OnError += OnSignalRErrorOccur;
                    signalRConnection.OnConnected += OnSignalRConnected;
                    signalRConnection.OnClosed += (con) => OnSignalRClosed ();
                    signalRConnection.OnReconnected += onSignalRReconnected;

                    Dictionary<string, string> dict = new Dictionary<string, string> ();
                    dict.Add (CLIENTID, myID);
                    signalRConnection.AdditionalQueryParams = dict;
                    signalRCoroutine = StartCoroutine ("OpenSignalRConnection");
                    AllOperations (); 

                } catch (Exception e) {
                }
            }      
        }
  
        void OnSignalRStatusChange (Connection conection, ConnectionStates oldState, ConnectionStates newState)
        {

        }

        void OnSignalRErrorOccur (Connection connection, string error)
        {
        }

        void  OnSignalRConnected (Connection connection)
        {
//			ConnectionStatus.instance.ConnectionMsg("Start Play");
//			ConnectionStatus.instance.startPlayBtn.interactable = true;
            curSignalRConectionStatus = SignalRConectionStatus.Connected;
            Debug.Log ("Signal R connected");
            UIHandler.instance.SignalRConnectionDone();
        }

        public void OnSignalRClosed ()
        {

        }
        void OnApplicationQuit()
        {
            signalRConnection.Close();
            Debug.Log("Application Quit");
        }


        void onSignalRReconnected (Connection connection)
        {
            Debug.Log ("Signal R connection Reconnected");
        }

        public IEnumerator OpenSignalRConnection ()
        {
            signalRConnection.Open ();
            while (true) {
                yield return new WaitForSeconds (10f);
                try {
                    if (signalRConnection.State == ConnectionStates.Closed) {
                        signalRConnection.Open ();
                    }
                } catch (Exception e) {
                    Debug.LogError ("Exception SignalR Open " + e.Message);
                }
            }
        }

        public void AllOperations ()
        {
            signalRConnection [HUB_NAME].On (GETREQUEST, OnReceiveMatchDetails);
            signalRConnection [HUB_NAME].On (ACK_CONNECTED, Ack);
            signalRConnection [HUB_NAME].On (CHALLENGEACCEPTED, ChallengeAccepted);
            signalRConnection [HUB_NAME].On (INPUTRECIVEC, OnInputRecived);
        }
    List <string> usersID = new List<string>();
        public void OnSendRequest(string i)
        {
            usersID.Clear();
            usersID.Add(myID);
            usersID.Add(friedID);
			TTTPlayerManager.instace.curPlayer = TTTPlayerManager.ePlayer.one;
             signalRConnection[HUB_NAME].Call("SendRequest",usersID);
            UIHandler.instance.gamePlayUI.SetActive(true);
            UIHandler.instance.menuUI.SetActive(false);
            UIHandler.instance.loadng.SetActive(false);
//			ConnectionStatus.instance.startPlayBtn.interactable = false;
        }

        public void OnReceiveMatchDetails(Hub hub, MethodCallMessage msg)
        {
                var str = msg.Arguments [0] as object[];
                friedID =str[0].ToString();
                UIHandler.instance.requestPanel.SetActive(true);

        }

        public void IacceptChallage()
        {
            usersID.Clear();
            usersID.Add(myID);
            usersID.Add(friedID);
            signalRConnection[HUB_NAME].Call("IacceptedChallenge",usersID);
            GameManager.instance.currGameMode = GameManager.eGameMode.onlineMultiPlayer;


        }
        public void ChallengeAccepted(Hub hub, MethodCallMessage msg)
        {
           Debug.Log("Accepted");
              GameManager.instance.currGameMode = GameManager.eGameMode.onlineMultiPlayer;
        }
         List <string> inputData = new List<string>();
        public void OnInPutDone(int a,int type)
        {
            inputData.Clear();
            inputData.Add(friedID);
            inputData.Add(a+"");
            inputData.Add(type+"");
            signalRConnection[HUB_NAME].Call("InPutTaken",inputData);
        }

        public void OnInputRecived(Hub hub, MethodCallMessage msg)
        {
            var str = msg.Arguments [0] as object[];
            Debug.Log(str[2].ToString());
     
            if (str[2].ToString() == "0")
            {
                int a = Convert.ToInt32(str[1]);
                 InputHandler.instance.UnetGridSelected(a);
            }
            else if(str[2].ToString() == "1")
            {
                 int a = Convert.ToInt32(str[1]);
                  InputHandler.instance.UnetOnInputTaken(a);
            }
            else if(str[2].ToString() == "2")
            {
                InputHandler.instance.UnetOnInputTracker_Closed();
            }
        }
        public void Ack(Hub hub, MethodCallMessage msg)
        {
            Debug.Log("Ack");
        }


}