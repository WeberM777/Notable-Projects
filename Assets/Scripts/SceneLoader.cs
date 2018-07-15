using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader> {

    public float progress;

    public void LoadNextScene(int index)
    {
        progress = 0;
        StartCoroutine(LoadScenes(index));
    }

    public void LoadNextScene(string name)
    {
        progress = 0;
        StartCoroutine(LoadScenes(name));
    }

    private IEnumerator LoadScenes(int index)
    {
        if(!SceneManager.GetActiveScene().name.Equals("Loading"))
        {
            yield return SceneManager.LoadSceneAsync("Loading");
        }

        yield return StartCoroutine(LoadScene(index));
    }

    private IEnumerator LoadScenes(string name)
    {
        if (!SceneManager.GetActiveScene().name.Equals("Loading"))
        {
            yield return SceneManager.LoadSceneAsync("Loading");
        }

        yield return StartCoroutine(LoadScene(name));
    }

    private IEnumerator LoadScene(int index)
    {
        var sceneAsync = SceneManager.LoadSceneAsync(index);

        sceneAsync.allowSceneActivation = false;

        while (!sceneAsync.isDone)
        {
            progress = sceneAsync.progress;
            if (sceneAsync.progress >= 0.9f)
            {

                progress = 1f;
                // we finally show the scene
                sceneAsync.allowSceneActivation = true;
            }

            yield return null;
        }

    }

    private IEnumerator LoadScene(string name)
    {
        var sceneAsync = SceneManager.LoadSceneAsync(name);

        sceneAsync.allowSceneActivation = false;

        while (!sceneAsync.isDone)
        {
            progress = sceneAsync.progress;
            if (sceneAsync.progress >= 0.9f)
            {

                progress = 1f;
                // we finally show the scene
                sceneAsync.allowSceneActivation = true;
            }

            yield return null;
        }

    }


}
