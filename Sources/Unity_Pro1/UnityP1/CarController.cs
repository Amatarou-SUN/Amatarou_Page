using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelsInfo
{
    public WheelCollider left;
    public WheelCollider right;
    public bool motor;
    public bool steering;
    public bool Brake;
}

public class CarController : MonoBehaviour
{
    //参考・引用元:https://docs.unity3d.com/ja/2019.4/Manual/WheelColliderTutorial.html
    public List<WheelsInfo> wheels; //ホイールの情報
    public float MaxTorque;
    public float MaxBrakeTorque;
    public float MaxSteerAngle;
    // 対応する視覚的なホイールを見つけます
    // Transform を正しく適用します
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float torque = MaxTorque * Input.GetAxis("Vertical"); //ホイールにかけるトルク
        float brake = MaxBrakeTorque * -Input.GetAxis("Vertical");
        float steering = MaxSteerAngle * Input.GetAxis("Horizontal"); //ホイールのステアリング角

        foreach (WheelsInfo wheel in wheels) //情報に入ったホイールを稼働させる
        {
            if (torque > 0)
            {
                //前進時にブレーキを0にする (改善の余地あり)
                wheel.left.brakeTorque = 0; //左ホイールのブレーキトルク制御
                wheel.right.brakeTorque = 0;//右ホイールのブレーキトルク制御

                if (wheel.motor) //もしホイールにトルクをかけていい設定なら
                {
                    wheel.left.motorTorque = torque; //左ホイールのトルク制御
                    wheel.right.motorTorque = torque;//右ホイールのトルク制御
                }
            }

            if (brake > 0)
            {
                //ブレーキ時にトルクを0にする (改善の余地あり)
                wheel.left.motorTorque = 0; //左ホイールのトルク制御
                wheel.right.motorTorque = 0;//右ホイールのトルク制御

                if (wheel.Brake) //もしホイールにトルクをかけていい設定なら
                {
                    wheel.left.brakeTorque = brake; //左ホイールのブレーキトルク制御
                    wheel.right.brakeTorque = brake;//右ホイールのブレーキトルク制御
                }
            }

            if (wheel.steering) //もしホイールをステアリングさせていい設定なら
            {
                wheel.left.steerAngle = steering; //左ホイールのステアリング角制御
                wheel.right.steerAngle = steering;//右ホイールのステアリング角制御
            }
            ApplyLocalPositionToVisuals(wheel.left); //左ホイールに対応する視覚的なホイール(?)
            ApplyLocalPositionToVisuals(wheel.right);//左ホイールに対応する視覚的なホイール(?)
            //P.S. ホイールがゲシュタルト崩壊してきた
        }
    }
}
