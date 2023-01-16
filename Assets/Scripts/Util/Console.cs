using UnityEngine;
using System.Collections.Generic;

public class Colors
{
    public string[] stringColors = { "#e67e22", "#d35400" };
    public string[] booleanColors = { "#9b59b6", "#8e44ad" };
    public string[] numberColors = { "#3498db", "#2980b9" };
    public string[] neutralColors = { "#2ecc71", "#27ae60" };
}

public static class Console
{

    public static void Log(params object[] args)
    {
        Colors colors = new Colors();
        string format = "";
        for (int i = 0; i < args.Length; i++)
        {
            string color = colors.neutralColors[i % colors.neutralColors.Length];
            switch (args[i].GetType().ToString().Replace("System.", ""))
            {
                case "Char":
                case "String":
                    color = colors.stringColors[i % colors.stringColors.Length];
                    break;
                case "Boolean":
                    color = colors.booleanColors[i % colors.booleanColors.Length];
                    break;
                case "Decimal":
                case "Int32":
                case "Int16":
                case "Single":
                    color = colors.numberColors[i % colors.numberColors.Length];
                    break;
            }
            format += string.Format(" <color={0}>{1}</color>", color, "{" + i + "}");
        }
        Debug.Log(string.Format("<color=#FF2222>" + format + "</color>", args));
    }
    public static void Warn(params object[] args)
    {
        string format = "";
        for (int i = 0; i < args.Length; i++)
            format += "{" + i + "} ";

        Debug.Log(string.Format("<color=#FF2222>" + format + "</color>", args));
    }

}