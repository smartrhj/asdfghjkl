using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueSystem2_TS_ : MonoBehaviour
{
    // public string gameScene = "GameScene";

    public Text txtName;
    public Text txtSentence;

    Queue<string> sentences = new Queue<string>();

    public Animator anim;

    public GameObject image;

    public void Begin(Dialogue info)
    {
        anim.SetBool("isOpen", true);

        sentences.Clear();

        txtName.text = info.name;

        foreach (var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }

        Next();
    }

    public void Next()
    {
        if (sentences.Count == 0)
        {
            End();
            return;
        }

        // txtSentence.text = sentences.Dequeue();
        txtSentence.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }

    IEnumerator TypeSentence(string sentence)
    {
        foreach (var letter in sentence)
        {
            txtSentence.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void End()
    {
        anim.SetBool("isOpen", false);

        txtSentence.text = string.Empty;

        // Invoke("nextScene", 0.5f);

        image.GetComponent<FadeOut>().fadeout();
        StartCoroutine(nextScene());

        //SceneManager.LoadScene("GameScene");
        // GetComponent<FadeScript>().Fade();
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("GameScene3");
    }
}
