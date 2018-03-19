using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIColorPick : MonoBehaviour
{

    // Use this for initialization
    public Animator GreenColorAnim;
    public Animator BlueColorAnim;
    public Animator RedColorAnim;
    public Animator YellowColorAnim;

    public void RunGreenAnim()
    {
        LoadMenuScene(1);
        GreenColorAnim.SetTrigger("Active");
    }
    public void RunBlueAnim()
    {
        LoadMenuScene(1);
        BlueColorAnim.SetTrigger("Active");
    }
    public void RunRedAnim()
    {
        LoadMenuScene(1);
        RedColorAnim.SetTrigger("Active");
    }
    public void RunYellowAnim()
    {
        LoadMenuScene(1);
        YellowColorAnim.SetTrigger("Active");
    }
    public void LoadMenuScene(float delay)
    {
        StartCoroutine(LoadMenuSceneDelay(delay));
    }
    private IEnumerator LoadMenuSceneDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Menu");
    }
}