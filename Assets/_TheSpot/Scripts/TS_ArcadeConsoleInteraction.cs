﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TS_ArcadeConsoleInteraction : MonoBehaviour, InteractableObject_UD {

	public RectTransform m_hoverMenu;
	private float m_scaleBigTime = 0.5f;
	private float m_scaleSmallTime = 0.5f;
	private bool m_scalingUp = false;
	private bool m_scalingDown = false;

	void InteractableObject_UD.Interact() {
		Debug.Log("interacting with " + gameObject.name);
	}

	void InteractableObject_UD.OnBeginRaycast() {
		if (m_scalingDown) {
			StopCoroutine(ScaleCanvasSmall());
			m_scalingDown = false;
		}
		StartCoroutine(ScaleCanvasBig());
	}

	void InteractableObject_UD.OnEndRaycast() {
		if (m_scalingUp) {
			StopCoroutine(ScaleCanvasBig());
			m_scalingUp = false;
		}
		StartCoroutine(ScaleCanvasSmall());
	}

	IEnumerator ScaleCanvasBig() {
		m_scalingUp = true;
		float startScale = m_hoverMenu.localScale.x;
		for (float t = startScale * m_scaleBigTime; t < m_scaleBigTime; t+=Time.deltaTime) {
			float scale = t/m_scaleBigTime;
			m_hoverMenu.localScale = new Vector3(scale,scale,scale);
			yield return null;
		}
		m_scalingUp = false;
	}

	IEnumerator ScaleCanvasSmall() {
		m_scalingDown = true;
		float startScale = m_hoverMenu.localScale.x;
		for (float t = startScale * m_scaleSmallTime; t > 0; t-=Time.deltaTime) {
			float scale = t/m_scaleSmallTime;
			m_hoverMenu.localScale = new Vector3(scale,scale,scale);
			yield return null;
		}
		m_scalingDown = false;
	}
}
