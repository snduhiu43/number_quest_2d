using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureManager : MonoBehaviour
{
    public Picture PicturePrefab;
    public Transform PicSpawnPosition;
    public Vector2 StartPosition = new Vector2(-1.6f, 2.18f);

    [HideInInspector] public List<Picture> PictureList = new List<Picture>();

    private Vector2 offset = new Vector2(1.5f, 1.52f);

    private List<Material> materialList = new List<Material>();
    private List<string> texturePathList = new List<string>();
    private Material firstMaterial;
    private string firstTexturePath;

    void Start()
    {
        LoadMaterials();
        SpawnPictureMesh(3, 3, StartPosition, offset, false);
        MovePicture(3, 3, StartPosition, offset);
    }

    private void LoadMaterials()
    {
        var materialFilePath = GameSettings.Instance.GetMaterialDirectoryName();
        var textureFilePath = GameSettings.Instance.GetLevelNumTextureDirectoryName();
        var levelNumber = (int)GameSettings.Instance.GetLevelNumber();
        const string matBaseName = "num";
        var firstMaterialName = "back";

        // Load the first material
        var backMaterialPath = materialFilePath + firstMaterialName;
        firstMaterial = Resources.Load(backMaterialPath, typeof(Material)) as Material;
        firstTexturePath = textureFilePath + firstMaterialName;

        if (firstMaterial == null)
        {
            Debug.LogError($"Failed to load first material: {backMaterialPath}");
        }

        // Load level-specific materials
        for (var index = 0; index <= levelNumber; index++)
        {
            var currentFilePath = materialFilePath + matBaseName + index;
            Material mat = Resources.Load(currentFilePath, typeof(Material)) as Material;
            if (mat != null)
            {
                materialList.Add(mat);
            }
            else
            {
                Debug.LogError($"Failed to load material: {currentFilePath}");
            }

            var currentTextureFilePath = textureFilePath + matBaseName + index;
            texturePathList.Add(currentTextureFilePath);
        }
    }

    public void SpawnPictureMesh(int rows, int columns, Vector2 pos, Vector2 offset, bool scaleDown)
    {
        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                var spawnPos = new Vector3(pos.x + (offset.x * row), pos.y - (offset.y * col), 0);
                var tempPicture = Instantiate(PicturePrefab, spawnPos, PicSpawnPosition.rotation);
                tempPicture.name = $"{tempPicture.name}_c{col}_r{row}";
                PictureList.Add(tempPicture);
            }
        }

        ApplyTextures();
    }

    private void ApplyTextures()
    {
        var appliedTimes = new int[materialList.Count];

        foreach (var picture in PictureList)
        {
            var rndIndex = Random.Range(0, materialList.Count);

            // Ensure no material is applied more than once
            while (appliedTimes[rndIndex] >= 1)
            {
                rndIndex = Random.Range(0, materialList.Count);
            }

            picture.SetFirstMaterial(firstMaterial, firstTexturePath);
            picture.ApplyFirstMaterial();
            picture.SetSecondMaterial(materialList[rndIndex], texturePathList[rndIndex]);
            appliedTimes[rndIndex]++;
        }
    }

    private void MovePicture(int rows, int columns, Vector2 pos, Vector2 offset)
    {
        var index = 0;
        for (var col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                var targetPosition = new Vector3((pos.x + (offset.x * row)), (pos.y - (offset.y * col)), 0.0f);
                StartCoroutine(MoveToPosition(targetPosition, PictureList[index]));
                index++;
            }
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, Picture obj)
    {
        var speed = 7f;

        while (Vector3.Distance(obj.transform.position, target) > 0.01f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }
}
