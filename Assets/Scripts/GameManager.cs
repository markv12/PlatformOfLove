﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public static bool gameRunning = false;
    public CanvasGroup BlackCurtain;

    public Player player;
    public Transform playerT;

    private static bool firstGame = true;

	void Awake () {
        instance = this;
        playerT = player.transform;
        if (!firstGame) {
            StartCoroutine(FadeToGame());
        } else {
            firstGame = false;
            gameRunning = true;
        }
	}

    void Update() {
        if(playerT.position.y <= -7) {
            instance.StartCoroutine(instance.RestartGame());
        }
    }

    public static void HandlePlatformHit(RaycastHit2D hit) {
        if (hit.transform.gameObject.layer == DatablePlatform.DATABLE_PLATFORM_LAYER) {
            DatablePlatform dp = hit.transform.gameObject.GetComponent<DatablePlatform>();
            if (dp != null) {
                if (!dp.friendly) {
                    gameRunning = false;
                    instance.StartCoroutine(instance.RestartGame());
                }
            }
        }
    }

    private static readonly WaitForSeconds wait = new WaitForSeconds(0.3f);
    private const float FadeTime = 0.5f;
    private IEnumerator RestartGame() {
        float progress = 0;
        float elapsedTime = 0;
        while (progress <= 1) {
            elapsedTime += Time.deltaTime;
            progress = elapsedTime / FadeTime;
            BlackCurtain.alpha = progress;
            yield return null;
        }
        BlackCurtain.alpha = 1;
        yield return null;
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator FadeToGame() {
        BlackCurtain.alpha = 1;
        gameRunning = false;
        yield return wait;
        float progress = 0;
        float elapsedTime = 0;
        while (progress <= 1) {
            elapsedTime += Time.deltaTime;
            progress = elapsedTime / FadeTime;
            BlackCurtain.alpha = 1 - progress;
            yield return null;
        }
        BlackCurtain.alpha = 0;
        gameRunning = true;
    }

}