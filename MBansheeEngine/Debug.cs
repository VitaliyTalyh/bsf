﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace BansheeEngine
{
    public sealed class Debug
    {
        public static void Log(object message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message.ToString());
            sb.Append(GetStackTrace());

            Internal_Log(sb.ToString());
        }

        public static void LogWarning(object message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message.ToString());
            sb.Append(GetStackTrace());

            Internal_LogWarning(sb.ToString());
        }

        public static void LogError(object message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message.ToString());
            sb.Append(GetStackTrace());

            Internal_LogError(sb.ToString());
        }

        public static string GetStackTrace()
        {
            StackTrace stackTrace = new StackTrace(1, true);

            StackFrame[] frames = stackTrace.GetFrames();
            if (frames == null)
                return "";

            StringBuilder sb = new StringBuilder();
            foreach (var frame in frames)
            {
                MethodBase method = frame.GetMethod();
                if (method == null)
                    continue;

                Type parentType = method.DeclaringType;
                if (parentType == null)
                    continue;

                sb.Append("\tat " + parentType.Name + "." + method.Name + "(");

                ParameterInfo[] methodParams = method.GetParameters();
                for(int i = 0; i < methodParams.Length; i++)
                {
                    if (i > 0)
                        sb.Append(", ");

                    sb.Append(methodParams[i].ParameterType.Name);
                }

                sb.Append(")");

                string ns = parentType.Namespace;
                string fileName = frame.GetFileName();
                if (!string.IsNullOrEmpty(fileName))
                {
                    int line = frame.GetFileLineNumber();
                    int column = frame.GetFileColumnNumber();

                    sb.Append(" in " + fileName + ", line " + line + ", column " + column + ", namespace " + ns);
                }
                else
                {
                    if (!string.IsNullOrEmpty(ns))
                        sb.Append(" in namespace " + ns);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern Component Internal_Log(string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern Component Internal_LogWarning(string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern Component Internal_LogError(string message);
    }
}
