using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadingscene : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Authentication Page")]
    public GameObject MainMenuLoadingScreen;

    [Header("Main Menu")]
    public GameObject LoadingScreenContainer;
    public GameObject[] LoadingScreen;
    public GameObject Tap;

    public async void LoadScene(int SceneID)
    {
        var scene = SceneManager.LoadSceneAsync(SceneID);
        scene.allowSceneActivation = false;
        if (SceneID == 1)
        {
            MainMenuLoadingScreen.SetActive(true);
            while (scene.progress < 0.9f)
            {
                // Update the loading bar based on the scene's current load progress.
                //LoadingBar.value = scene.progress / 0.9f; // Normalizing to 0-1 scale for the loading bar
                await Task.Delay(100); // A short delay to reduce spamming the update, making the UI smoother
            }
            scene.allowSceneActivation = true;
        }
        //for tutorial scene
        else if (SceneID == 8)
        {
            //MainMenuLoadingScreen.SetActive(true);
            while (scene.progress < 0.9f)
            {
                // Update the loading bar based on the scene's current load progress.
                //LoadingBar.value = scene.progress / 0.9f; // Normalizing to 0-1 scale for the loading bar
                await Task.Delay(100); // A short delay to reduce spamming the update, making the UI smoother
            }
            scene.allowSceneActivation = true;
        }
        else
        {

            LoadingScreenContainer.SetActive(true);
            if (SceneID - 2 >= 0 && SceneID - 2 < LoadingScreen.Length)
            {
                LoadingScreen[SceneID - 2].SetActive(true);
            }

            // It's better to update the loading bar in real-time rather than waiting for a fixed delay
            while (scene.progress < 0.9f)
            {
                // Update the loading bar based on the scene's current load progress.
                //LoadingBar.value = scene.progress / 0.9f; // Normalizing to 0-1 scale for the loading bar
                await Task.Delay(100); // A short delay to reduce spamming the update, making the UI smoother
            }

            // Once the loop exits, it means the scene is almost ready (90% loaded).
            Tap.SetActive(true); // Make the Tap button visible

            // Ensure any previous listeners are removed to prevent multiple subscriptions
            Tap.GetComponent<Button>().onClick.RemoveAllListeners();
            // Add a listener to the Tap button to activate the scene when clicked
            Tap.GetComponent<Button>().onClick.AddListener(() => scene.allowSceneActivation = true);
        }
    }
}

