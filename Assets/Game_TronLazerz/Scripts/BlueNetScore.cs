﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueNetScore : MonoBehaviour {
public ScoreManager m_sm;

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			Destroy(other.gameObject);
			m_sm.scored_Red = true;
			
		}
	}
}
