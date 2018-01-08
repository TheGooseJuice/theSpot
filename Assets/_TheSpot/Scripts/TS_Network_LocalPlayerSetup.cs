﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class TS_Network_LocalPlayerSetup : NetworkBehaviour {

	[SyncVar]
	public string m_PlayerName = null;
	private NetworkInstanceId m_playerNetID;
	private FirstPersonController m_movement;
	private AudioListener m_audio;
	private Camera m_cam;
	private TS_Network_Chat m_chat;
	public Text m_nameText;
	public GameObject m_chatSystem;
	public GameObject m_EventSystem;
	public GameObject m_glasses;
	private static Transform m_camTransform = null; //for billboard effect


	public override void OnStartLocalPlayer() {
		CmdTellServerMyName(PlayerPrefs.GetString("PlayerName"));
		m_movement = GetComponent<FirstPersonController>();
		m_audio = GetComponentInChildren<AudioListener>();
		m_cam = GetComponentInChildren<Camera>();
		m_chat = GetComponentInChildren<TS_Network_Chat>();
		m_movement.enabled = true;
		m_audio.enabled = true;
		m_cam.enabled = true;
		m_chat.enabled = true;
		m_camTransform = m_cam.transform;
		m_chatSystem.SetActive(true);
		m_EventSystem.SetActive(true);
		m_glasses.SetActive(false);
	}

	private void Update() {
		if (m_PlayerName != null && (transform.name == "Networked_Player(Clone)" || transform.name == "")) {
			transform.name = m_PlayerName;
			if (isLocalPlayer) {
				m_nameText.enabled = false;
				m_chat.enabled = true;
				m_chat.SetPlayerName(m_PlayerName);
			} else {
				m_nameText.text = m_PlayerName;				
			}
		}
		RotateTextToFaceCamera();
	}

	private void RotateTextToFaceCamera() {
		if (m_camTransform != null) {
			m_nameText.transform.LookAt(m_camTransform);
			Vector3 rot = m_nameText.transform.rotation.eulerAngles;
			rot.y += 180;
			m_nameText.transform.eulerAngles = rot;
		}
	}

	[Command]
	void CmdTellServerMyName(string name){
		m_PlayerName = name;
	}
}