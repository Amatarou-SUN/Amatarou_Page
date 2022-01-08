using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject target;

    public bool AllowLookAt;

    public bool RotateKeepX;
    public bool RotateKeepY;
    public bool RotateKeepZ;

    private bool Child0iscamera = true; //子オブジェクトナンバー1がカメラ
    // Start is called before the first frame update
    void Start()
    {
        if (!(transform.childCount == 0)) //このゲームオブジェクトに子オブジェクトがあるか
        {
            try //子オブジェクトがカメラかどうか判定
            {
                if (Child0iscamera) //子オブジェクトナンバー1がカメラなら
                {
                    transform.GetChild(0).GetComponent<Camera>(); //カメラを取得 (カメラじゃなかったらエラー)
                }
            }
            catch //エラー
            {
                Child0iscamera = false; //子オブジェクトがカメラではない
            }
        }
        else //子オブジェクトがない
        {
            Child0iscamera = false; //子オブジェクトがカメラではない
        }
    }

    // Update is called once per frame
    void Update()
    {
        //このゲームオブジェクトの位置をターゲットと同じにする
        transform.position = target.transform.position;
        //回転軸Xをキープするなら
        if (RotateKeepX)
        {
            transform.eulerAngles = new Vector3(target.transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z); //回転軸Xをターゲットと同じにする
        }
        //回転軸Yをキープするなら
        if (RotateKeepY)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, target.transform.eulerAngles.y, transform.eulerAngles.z); //回転軸Xをターゲットと同じにする
        }
        //回転軸Zをキープするなら
        if (RotateKeepZ)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, target.transform.eulerAngles.z); //回転軸Zをターゲットと同じにする
        }

        if (Child0iscamera) //子オブジェクトナンバー1がカメラなら
        {
            if (AllowLookAt) //カメラをターゲットにLookAtさせるか
            {
                transform.GetChild(0).GetComponent<Transform>().LookAt(target.transform); //カメラをターゲットにLookAt
            }
        }
    }
}
