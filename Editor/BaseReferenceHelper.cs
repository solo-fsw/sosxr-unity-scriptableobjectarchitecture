using System.Collections;
using System.Reflection;
using ScriptableObjectArchitecture;
using UnityEngine;
using Type = System.Type;


/// <summary>Static utility for reflecting on <see cref="BaseReference"/> fields to extract the concrete reference type and underlying value type at editor time.</summary>
public static class BaseReferenceHelper
{
    private const BindingFlags NonPublicBindingsFlag = BindingFlags.Instance | BindingFlags.NonPublic;
    private const string ConstantValueName = "_constantValue";


    public static Type GetReferenceType(FieldInfo fieldInfo)
    {
        return fieldInfo.FieldType;
    }


    public static Type GetValueType(FieldInfo fieldInfo)
    {
        var referenceType = GetReferenceType(fieldInfo);

        if (referenceType.IsArray)
        {
            referenceType = referenceType.GetElementType();
        }
        else if (IsList(referenceType))
        {
            Debug.Log("Is list!");
            referenceType = referenceType.GetGenericArguments()[0];
        }

        var constantValueField = referenceType.GetField(ConstantValueName, NonPublicBindingsFlag);

        return constantValueField.FieldType;
    }


    private static bool IsList(Type referenceType)
    {
        return typeof(IList).IsAssignableFrom(referenceType);
    }
}