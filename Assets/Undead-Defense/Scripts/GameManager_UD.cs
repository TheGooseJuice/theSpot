﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Intro, Menu, Loading, Play, Win, Lose }
public enum GameMode {Normal,Survival}
public class GameManager_UD : MonoBehaviour {

	

	//public
	public static GameManager_UD instance;

	public float m_introTimer = 3.0f;

	public GameState m_currentState;

    public GameObject[] m_gameStates;

	public Dropdown m_mapDropdown;

	public GameObject[] m_maps;	//image of maps

	public GameObject m_lockImage;
	public GameObject m_normalButton;
	public GameObject m_survivalButton;
	//private
	float m_currentIntroTimer;
	
	int m_currentSelectedLevel = 0;

	int[] m_mapUnlockState = new int[3];

	GameMode m_currentGameMode = GameMode.Normal;

	//unlock states
	//0 - locked
	//1 - unlocked - normal
	//2 - unlocked - survival
	int m_map1 = 1;
	int m_map2 = 0;
	int m_map3 = 0;


	void Awake(){
		if (instance == null){
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
        {
            Destroy(gameObject);
        }

		m_currentIntroTimer = 0.0f;

		m_mapUnlockState[0] = 1;
		m_mapUnlockState[1] = 0;
		m_mapUnlockState[2] = 0;

		m_map1 = PlayerPrefs.GetInt("Map1");
		m_map2 = PlayerPrefs.GetInt("Map2");
		m_map3 = PlayerPrefs.GetInt("Map3");

	}
	void Start () {
		m_currentState = GameState.Intro;
		foreach (GameObject go in m_gameStates)
		{
			go.SetActive(false);
		}
		m_gameStates[(int)m_currentState].SetActive(true);
		ChangeSelectedLevel();
	}
	
	// Update is called once per frame
	void Update () {
		switch(m_currentState){
			case GameState.Intro:
				UpdateIntro();
			break;
			case GameState.Menu:
			break;
		}
	}

	void UpdateIntro(){
		if (m_currentIntroTimer > m_introTimer){
			ChangeState(GameState.Menu);
		}else{
			m_currentIntroTimer += Time.deltaTime;
		}
		
	}

	void ChangeState(GameState newState){
		m_gameStates[(int)m_currentState].SetActive(false);
		m_currentState = newState;
		m_gameStates[(int)m_currentState].SetActive(true);
	}

	//when player changes level in the dropdown
	public void ChangeSelectedLevel(){
		m_maps[m_currentSelectedLevel].SetActive(false);
		

		m_currentSelectedLevel = m_mapDropdown.value;

		switch(m_mapUnlockState[m_currentSelectedLevel]){
			case 0://locked
			m_lockImage.SetActive(true);
			m_normalButton.SetActive(false);
			m_survivalButton.SetActive(false);
			//TODO:update score
			break;
			case 1://unlocked - normal
			m_lockImage.SetActive(false);
			m_normalButton.SetActive(true);
			m_survivalButton.SetActive(false);
			//TODO:update score
			break;
			case 2://unlocked - survival
			m_lockImage.SetActive(false);
			m_normalButton.SetActive(true);
			m_survivalButton.SetActive(true);
			//TODO:update score
			break;
		}
		m_maps[m_currentSelectedLevel].SetActive(true);
	}

	public void StartNormal(){
		//TODO:change level
		m_currentGameMode = GameMode.Normal;
		ChangeState(GameState.Loading);
	}

	public void StartSurvival(){
		//TODO:change level
		m_currentGameMode = GameMode.Survival;
		ChangeState(GameState.Loading);
	}


	public void MapReady(){
		ChangeState(GameState.Play);
	}
}