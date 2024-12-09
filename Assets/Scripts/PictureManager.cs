using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureManager : MonoBehaviour
{
    public Picture PicturePrefab;
    public Transform PicSpawnPosition;
    public Vector2 StartPosition = new Vector2(-1.6f, 2.18f);

    [HideInInspector] public List<Picture> PictureList;

    private Vector2 offset = new Vector2(1.5f, 1.52f);
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnPictureMesh(3, 3, StartPosition, offset, false);
        MovePicure(3, 3, StartPosition, offset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  SpawnPictureMesh(int rows, int columns, Vector2 pos, Vector2 offest, bool scaleDown)
    {
        for(int col  = 0; col < columns; col++)
        {
            for(int row =0;  row < rows; row++)
            {
                var tempPicture = (Picture)Instantiate(PicturePrefab, PicSpawnPosition.position, PicSpawnPosition.transform.rotation);

                tempPicture.name = tempPicture.name + 'c' + col + 'r' + row;
                PictureList.Add(tempPicture);
            }
        }
    }

    private void MovePicure(int rows, int columns, Vector2 pos, Vector2 offset)
    {
        var index = 0;
        for(var  col = 0; col < columns; col++)
        {
            for(int row = 0; row < rows; row++)
            {
                var targetPosition = new Vector3((pos.x + (offset.x * row)), (pos.y - (offset.y * col)), 0.0f);
                StartCoroutine(MoveToPosition(targetPosition, PictureList[index]));
                index++;
            }
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, Picture obj)
    {
        var randomDist = 7;

        while (obj.transform.position != target)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, randomDist * Time.deltaTime);
            yield return 0;
        }
    }
}
