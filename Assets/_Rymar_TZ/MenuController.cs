using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject detailsPanel;
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Material devilmat;
    [SerializeField]
    Slider dissolveSlider;
    [SerializeField]
    Slider distortRangeSlider;
    [SerializeField]
    Slider distortScaleSlider;

    public void DeatilsPanelToggle() {
        menuPanel.SetActive(!menuPanel.activeSelf);
        detailsPanel.SetActive(!detailsPanel.activeSelf);
    }

    public void StartGame() {

        StartCoroutine(LoadAsync(1));
        menuPanel.SetActive(false);
        detailsPanel.SetActive(false);
    }
    IEnumerator LoadAsync(int sceneIndex)
    {
        loadingPanel.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            yield return null;
        }

    }

    public void Qiut()
    {
        Application.Quit();
    }

    private void Update()
    {

        devilmat.SetFloat("_Dissolve", dissolveSlider.value);
        devilmat.SetFloat("_Distort", distortRangeSlider.value);
        devilmat.SetFloat("_Scale2", distortScaleSlider.value);



    }


}
