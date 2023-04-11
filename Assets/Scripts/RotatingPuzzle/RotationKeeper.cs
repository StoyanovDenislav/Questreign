using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class RotationKeeper : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    List<Button> buttonsInScene = new List<Button>();
    public List<int> rotations = new List<int>();

    private string key = "image1";

    AsyncOperationHandle<Texture2D> targetImageHandle;

    CheckTimeAndCompletion checkIfSolved { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        buttonsInScene = FindObjectsOfType<Button>().ToList();
        StartCoroutine(ImageInstantiate());
        StartButtons();
        checkIfSolved = FindObjectOfType<CheckTimeAndCompletion>();
      
    }


    public void StartButtons()
    {
        foreach (var button in buttonsInScene)
        {
            if (button.CompareTag("RotatingPiecePuzzle"))
            {
                buttons.Add(button);
            }
        }
        buttons = buttons.OrderBy(go => go.name).ToList();
        for (int i = 0; i < buttons.Count; i++)
        {
            rotations.Add(0);
        }
    }

    public IEnumerator ImageInstantiate()
    {
        targetImageHandle = Addressables.LoadAssetAsync<Texture2D>(key);

        yield return targetImageHandle;

        if (targetImageHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Texture2D pngTexture = targetImageHandle.Result;
            Vector2Int sliceCount = new Vector2Int(3, 3);
            int sliceWidth = pngTexture.width / sliceCount.x;
            int sliceHeight = pngTexture.height / sliceCount.y;
            for (int i = 0; i < buttons.Count; i++)
            {
                int x = i % sliceCount.x;
                int y = sliceCount.y - 1 - (i / sliceCount.x);
                Rect sliceRect = new Rect(x * sliceWidth, y * sliceHeight, sliceWidth, sliceHeight);
                Sprite sliceSprite = Sprite.Create(pngTexture, sliceRect, Vector2.zero);

                buttons[i].image.sprite = sliceSprite;
            }

            yield return new WaitUntil(() => checkIfSolved.puzzleIsDone);

            Addressables.Release(targetImageHandle);
        }
    }
}