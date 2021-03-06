﻿using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	
	private const string typeName = "SuperSpiel";
	private const string gameName = "BesterRaum";
	
	
	// SERVER
	private void StartServer()
	{
		// Debug.Log("Server gestartet");
		Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
	
	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
		SpawnPlayer ();
		//SpawnChef ();
	}
	
	// CLIENT
	
	private HostData[] hostList;
	
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}
	
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
		Debug.Log ("Versuche mich zu verbinden");
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		SpawnPlayer ();
	}
	
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}
	
	// PLAYER
	public GameObject playerPrefab;
	
	private void SpawnPlayer()
	{
		int startPositionX = Random.Range(-4,4);
		int startPositionZ = Random.Range(-4,4);
		Network.Instantiate(playerPrefab, new Vector3(startPositionX, 5f, startPositionZ), Quaternion.identity, 0);
	}
	
	void Start() 
	{
		
	}
}

