using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public static bool gameRunning = false;
    public CanvasGroup BlackCurtain;
    public TMP_Text deathText;
    private static string lastDeathMessage;

    public ChatSystem chatSystem;

    public DatablePlatform[] datablePlatforms;

    public Player player;
    public Transform playerT;

    private static bool firstGame = true;

	void Awake () {
        deathText.text = lastDeathMessage;
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
            instance.StartCoroutine(instance.RestartGame("Dead!"));
        }
        bool platformIsNear = false;
        for (int i = 0; i < datablePlatforms.Length; i++) {
            DatablePlatform dp = datablePlatforms[i];
            float distFromPlayer = Vector3.Distance(dp.transform.position, playerT.position);
            if (distFromPlayer <= 7) {
                if (chatSystem.CurrentPlatform != dp) {
                    chatSystem.CurrentPlatform = dp;
                    if (!dp.Friendly) {
                        chatSystem.FadeIn();
                    }
                }
                platformIsNear = true;
                break;
            }
        }
        if (!platformIsNear) {
            if(chatSystem.CurrentPlatform != null) {
                chatSystem.CurrentPlatform = null;
                chatSystem.FadeOut();
            }
        }
    }

    public static void HandlePlatformHit(RaycastHit2D hit) {
        if (hit.transform.gameObject.layer == DatablePlatform.DATABLE_PLATFORM_LAYER) {
            DatablePlatform dp = hit.transform.gameObject.GetComponent<DatablePlatform>();
            if (dp != null) {
                if (!dp.Friendly) {
                    gameRunning = false;
                    instance.StartCoroutine(instance.RestartGame(dp.deathMessage));
                }
            }
        }
    }

    private static readonly WaitForSeconds wait = new WaitForSeconds(0.3f);
    private const float FadeTime = 0.4f;
    private IEnumerator RestartGame(string deathMessage) {
        lastDeathMessage = deathMessage;
        deathText.text = deathMessage;
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
