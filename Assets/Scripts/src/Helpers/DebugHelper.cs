using System;
using UnityEngine;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable ArrangeTypeMemberModifiers

namespace src.Helpers
{
    public static class DebugHelper
    {
        public static void LogInfo(string message)
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log(message);
            }
        }
        
        public static void LogWarning(string message)
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogWarning(message);
            }
        }
        
        public static void LogError(string message)
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogError(message);
            }
        }
        
        public static void LogException(Exception ex)
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogException(ex);
            }
        }

        public static void DrawRay(Vector3 position, Vector3 direction, Color color)
        {
            if (Debug.isDebugBuild)
            {
                Debug.DrawRay(position, direction, color);
            }
        }
        
        public static void DrawLine(Vector3 position, Vector3 direction, Color color)
        {
            if (Debug.isDebugBuild)
            {
                Debug.DrawLine(position, direction, color);
            }
        }
    }
}