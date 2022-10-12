using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class MapBuilder: MonoBehaviour
{
    [SerializeField] private GameObject _parentObject;
    [SerializeField] private TextAsset _mapFile;

    [SerializeField] private Sprite _block;
    [SerializeField] private Sprite _player;

    [SerializeField] private float _tileSize = 0.64f;

    [SerializeField] private int rows = 16;
    [SerializeField] private int columns = 11;

    private string _tileData;
    private const char BLOCK = '#';
    private const char PLAYER = 'p';

    void Start()
    {
        BuildMap();
    }

    public void BuildMap()
    {
        LoadMapData();
        CreateMap();
    }

    /// <summary>
    /// ロードを行う
    /// </summary>
    private void LoadMapData()
    {
        _tileData=_mapFile.text;
    }

    /// <summary>
    /// マップを作る
    /// </summary>
    private void CreateMap()
    {
        //タイルを作り始める初期位置を設定
        Vector2 pos = GetInitPos();

        foreach (var c in _tileData)
        {
            //ブロック
            if (BLOCK == c)
            {
                CreateObj(_block, ref pos);
            }
            //プレイヤー
            else if (PLAYER == c)
            {
                CreateObj(_player, ref pos, 2, "Player");
            }
            //空白
            else if (' ' == c)
            {
                pos.x += _tileSize;
            }
            //改行
            else if ('\n' == c)
            {
                pos.x = -(rows * _tileSize * 0.5f + _tileSize * 0.5f);
                pos.y += -_tileSize;
            }
        }
    }

    /// <summary>
    /// パネルを生成しつつ、位置関係を設定する
    /// </summary>
    /// <param name="sprite">パネルに設定したい画像</param>
    /// <param name="pos">パネルの位置座標</param>
    /// <param name="layer">オブジェクトの描画順</param>
    /// <param name="name">パネルの名前</param>
    private void CreateObj(Sprite sprite, ref Vector2 pos, int layer = 1, string name = "Block")
    {
        var obj = new GameObject(name);
        obj.AddComponent<SpriteRenderer>().sprite = sprite;
        obj.GetComponent<SpriteRenderer>().sortingOrder = layer;

        pos.x += _tileSize;
        obj.transform.position = pos;
        obj.transform.parent = _parentObject.transform;
    }


    /// <summary>
    /// マップを中央にするための位置座標を返す
    /// </summary>
    /// <returns></returns>
    private Vector2 GetInitPos()
    {
        return new Vector2(
            -(rows * _tileSize * 0.5f + _tileSize * 0.5f),
            (columns * _tileSize * 0.5f - _tileSize * 0.5f)
        );
    }
}