using UnityEngine;
using System.Collections.Generic;

public static class TransformExtension
{
    const string _SAVE_SEPARATOR = "_";

    public static string CreateSaveString(this Transform transform, bool isLocal, bool isSavePosition, bool isSaveRotation, bool isSaveScale)
    {
        if (isLocal)
        {
            return CreateLocalSaveString(transform, isSavePosition, isSaveRotation, isSaveScale); 
        }

        return CreateWorldSaveString(transform, isSavePosition, isSaveRotation, isSaveScale);
    }

    private static string CreateLocalSaveString(Transform transform, bool isSavePosition, bool isSaveRotation, bool isSaveScale)
    {
        string data = "";

        if (isSavePosition)
        {
            Vector3 pos = transform.localPosition;
            data += pos.x.ToString() + _SAVE_SEPARATOR;
            data += pos.y.ToString() + _SAVE_SEPARATOR;
            data += pos.z.ToString() + _SAVE_SEPARATOR;
        }

        if (isSaveRotation)
        {
            Vector3 rot = transform.localEulerAngles;
            data += rot.x.ToString() + _SAVE_SEPARATOR;
            data += rot.y.ToString() + _SAVE_SEPARATOR;
            data += rot.z.ToString() + _SAVE_SEPARATOR;
        }

        if (isSaveScale)
        {
            Vector3 scale = transform.localScale;
            data += scale.x.ToString() + _SAVE_SEPARATOR;
            data += scale.y.ToString() + _SAVE_SEPARATOR;
            data += scale.z.ToString() + _SAVE_SEPARATOR;
        }

        return data;
    }

    private static string CreateWorldSaveString(Transform transform, bool isSavePosition, bool isSaveRotation, bool isSaveScale)
    {
        string data = "";

        if (isSavePosition)
        {
            Vector3 pos = transform.position;
            data += pos.x.ToString() + _SAVE_SEPARATOR;
            data += pos.y.ToString() + _SAVE_SEPARATOR;
            data += pos.z.ToString() + _SAVE_SEPARATOR;
        }

        if (isSaveRotation)
        {
            Vector3 rot = transform.eulerAngles;
            data += rot.x.ToString() + _SAVE_SEPARATOR;
            data += rot.y.ToString() + _SAVE_SEPARATOR;
            data += rot.z.ToString() + _SAVE_SEPARATOR;
        }

        if (isSaveScale)
        {
            Vector3 scale = transform.localScale;
            data += scale.x.ToString() + _SAVE_SEPARATOR;
            data += scale.y.ToString() + _SAVE_SEPARATOR;
            data += scale.z.ToString() + _SAVE_SEPARATOR;
        }

        return data;
    }

    public static void SetupFromSaveString(this Transform transform, string data, bool isLocal, 
                                           bool isLoadPosition, bool isLoadRotation, bool isLoadScale
                                           )
    {
        if (isLocal)
        {
            SetupLocalFromString(transform, data, isLoadPosition, isLoadRotation, isLoadScale);
            return;
        }

        SetupWorldFromString(transform, data, isLoadPosition, isLoadRotation, isLoadScale);
    }

    private static void SetupLocalFromString(Transform transform, string data,
                                             bool isLoadPosition, bool isLoadRotation, bool isLoadScale
                                            )
    {
        string[] dataList = data.Split(_SAVE_SEPARATOR[0]);
        int index = 0;

        if (isLoadPosition)
        {
            transform.localPosition = new Vector3(float.Parse(dataList[index++]),
                                                  float.Parse(dataList[index++]),
                                                  float.Parse(dataList[index++])
                                                  );
        }

        if (isLoadRotation)
        {
            transform.localEulerAngles = new Vector3(float.Parse(dataList[index++]),
                                                    float.Parse(dataList[index++]),
                                                    float.Parse(dataList[index++])
                                                    );
        }

        if (isLoadScale)
        {
            transform.localScale = new Vector3(float.Parse(dataList[index++]),
                                               float.Parse(dataList[index++]),
                                               float.Parse(dataList[index++])
                                               );
        }
    }

    private static void SetupWorldFromString(Transform transform, string data,
                                             bool isLoadPosition, bool isLoadRotation, bool isLoadScale
                                             )
    {
        string[] dataList = data.Split(_SAVE_SEPARATOR[0]);
        int index = 0;

        if (isLoadPosition)
        {
            transform.position = new Vector3(float.Parse(dataList[index++]),
                                             float.Parse(dataList[index++]),
                                             float.Parse(dataList[index++])
                                             );
        }

        if (isLoadRotation)
        {
            transform.eulerAngles = new Vector3(float.Parse(dataList[index++]),
                                                float.Parse(dataList[index++]),
                                                float.Parse(dataList[index++])
                                                );
        }

        if (isLoadScale)
        {
            transform.localScale = new Vector3(float.Parse(dataList[index++]),
                                               float.Parse(dataList[index++]),
                                               float.Parse(dataList[index++])
                                               );
        }
    }

    public static void ResetLocal(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static void ResetLocal(this Transform transform, bool isResetPosition, bool isResetRotation, bool isResetScale)
    {
        if (isResetPosition)
        {
            transform.localPosition = Vector3.zero;
        }

        if (isResetRotation)
        {
            transform.localRotation = Quaternion.identity;
        }

        if (isResetScale)
        {
            transform.localScale = Vector3.one;
        }
    }

    public static void ResetWorld(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;        
    }

    public static void ResetWorld(this Transform transform, bool isResetPosition, bool isResetRotation)
    {
        if (isResetPosition)
        {
            transform.position = Vector3.zero;
        }

        if ( isResetRotation )
        {
        transform.rotation = Quaternion.identity;
            }
    }
}
