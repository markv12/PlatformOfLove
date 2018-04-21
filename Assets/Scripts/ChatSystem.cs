using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatSystem : MonoBehaviour {

    private DatablePlatform currentPlatform;
    public DatablePlatform CurrentPlatform {
        get {
            return currentPlatform;
        }
        set {
            currentPlatform = value;
            if (currentPlatform != null) {
                transform.position = currentPlatform.transform.position + new Vector3(0, 2.5f, 0);
                ShowDialogNode(currentPlatform.rootNode);

            } else {
                currentNode = null;
            }
        }
    }

    public TMP_Text PlatformSpeech;
    public TMP_Text[] Options;

    public CanvasGroup canvasGroup;


    void Update() {
        if(currentNode != null) {
            if(Input.anyKeyDown && ContainsNumber(Input.inputString)){
                char numberPressed = Input.inputString.ToCharArray()[0];
                int n = (int)char.GetNumericValue(numberPressed) - 1;
                if(n < currentNode.options.Length) {
                    ShowDialogNode(currentNode.options[n].dest);
                }
            }
        }
    }


	public void ShowDialogNode(DialogNode node) {
        PlatformSpeech.text = node.text;
        for (int i = 0; i < Options.Length; i++) {
            if(node.options != null && i < node.options.Length) {
                Options[i].text = (i+1).ToString() + ". " + node.options[i].text;
            } else {
                Options[i].text = "";
            }
        }
        currentNode = node;
        if(node.speshi == SpeshI.MakeFriendly) {
            CurrentPlatform.Friendly = true;
        }
    }
    private DialogNode currentNode;

    private Coroutine fadeRoutine = null;
    public void FadeIn() {
        if(fadeRoutine != null) {
            StopCoroutine(fadeRoutine);
            fadeRoutine = null;
        }
        fadeRoutine = StartCoroutine(_FadeIn());
    }

    public void FadeOut() {
        if (fadeRoutine != null) {
            StopCoroutine(fadeRoutine);
            fadeRoutine = null;
        }
        fadeRoutine = StartCoroutine(_FadeOut());
    }

    private const float FadeTime = 0.5f;
    private IEnumerator _FadeIn() {
        float progress = 0;
        float elapsedTime = 0;
        float startAlpha = canvasGroup.alpha;
        while (progress <= 1) {
            elapsedTime += Time.deltaTime;
            progress = elapsedTime / FadeTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, progress);
            yield return null;
        }
        canvasGroup.alpha = 1;
        fadeRoutine = null;
    }

    private IEnumerator _FadeOut() {
        float progress = 0;
        float elapsedTime = 0;
        float startAlpha = canvasGroup.alpha;
        while (progress <= 1) {
            elapsedTime += Time.deltaTime;
            progress = elapsedTime / FadeTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, progress);
            yield return null;
        }
        canvasGroup.alpha = 0;
        fadeRoutine = null;
    }

    private static bool ContainsNumber(string s) {
        char[] input = s.ToCharArray();
        for (int i = 0; i < input.Length; i++) {
            if (char.IsNumber(input[i])) {
                return true;
            }
        }
        return false;
    }
}
