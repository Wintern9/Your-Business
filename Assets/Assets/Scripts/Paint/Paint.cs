using UnityEngine;
using System.Collections;

public class Paint : MonoBehaviour
{

    public SpriteRenderer brush; // ������ ��������
    public Color brushColor = Color.red;
    [Range(0.01f, 0.05f)] public float brushSize = 0.05f;
    public Camera cameraRT; // ������ ����� ��������� � ���������
    public int sizeRT = 1024; // ������ ��������
    public MeshRenderer canvasObject; // �����, �� �������� �� ����� �������� �����
    public MeshRenderer planeRT; // �����, ����������� �������

    private RenderTexture renderTexture;
    private Vector3 position;
    private int counter = 0, maxCount = 1000; // ������� � ����. ���������� ��������
    private bool isSave;

    void Awake()
    {
        position.z = brush.transform.position.z;
        Clear();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !isSave)
        {
            brush.gameObject.SetActive(true);
            Draw();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Clear();
        }
        else if (isSave)
        {
            brush.gameObject.SetActive(false);
            Save();
        }
        else
        {
            brush.gameObject.SetActive(false);
        }

    }

    // ���������� �������� ������� � ������� ��������
    // ����� ������� �������� � �������� � �����, ����� ������ ���������
    // ��� ���������� ��� �����������, ����� �� ��������� ������� ����� ��������
    void Save()
    {
        counter = 0;
        RenderTexture.active = renderTexture;
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;
        planeRT.material.mainTexture = tex;
        foreach (Transform child in planeRT.transform)
        {
            Destroy(child.gameObject);
        }
        isSave = false;
    }

    void Clear() // �������� ��
    {
        counter = 0;
        foreach (Transform child in planeRT.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(planeRT.material.mainTexture);
        Destroy(renderTexture);
        renderTexture = new RenderTexture(sizeRT, sizeRT, 24, RenderTextureFormat.ARGB32);
        cameraRT.targetTexture = renderTexture;
        canvasObject.material.mainTexture = renderTexture;
    }

    void Draw() // ��������� (������������ �����)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            SpriteRenderer s = Instantiate(brush) as SpriteRenderer;
            Vector2 uv = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            position.x = uv.x - cameraRT.orthographicSize;
            position.y = uv.y - cameraRT.orthographicSize;
            s.color = brushColor;
            s.transform.localPosition = position;
            s.transform.localScale = Vector3.one * brushSize;
            s.transform.parent = planeRT.transform;
            counter++;

            if (counter > maxCount)
            {
                isSave = true;
            }
        }
    }
}