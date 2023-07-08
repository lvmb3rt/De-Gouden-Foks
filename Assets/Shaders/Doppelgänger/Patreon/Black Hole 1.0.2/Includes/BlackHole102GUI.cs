#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class BlackHole102GUI : ShaderGUI
{
    private Font VrchatFont = (Font)Resources.Load(@"BankGothic");

    private BindingFlags bindingFlags = BindingFlags.Public |
                                    BindingFlags.NonPublic |
                                    BindingFlags.Instance |
                                    BindingFlags.Static;
    private MaterialProperty _Color1 = null;
    private MaterialProperty _Color2 = null;
    private MaterialProperty _Color3 = null;
    private MaterialProperty _bhs = null;
    private MaterialProperty _gravitation = null;
    private MaterialProperty _gravitationscale = null;
    private MaterialProperty _absorptionspeed = null;
    private MaterialProperty _rotationspeed = null;
    private MaterialProperty _radialdistortion = null;

    private MaterialProperty HUE = null;
    private MaterialProperty _hue = null;
    private MaterialProperty _hueo = null;
    private MaterialProperty _huespeed = null;
    private MaterialProperty _hueoc = null;
    private MaterialProperty _huesaturation = null;

    private MaterialProperty _Stencil = null;
    private MaterialProperty _ReadMask = null;
    private MaterialProperty _WriteMask = null;
    private MaterialProperty _StencilComp = null;
    private MaterialProperty _StencilOp = null;
    private MaterialProperty _StencilFail = null;
    private MaterialProperty _StencilZFail = null;

    private MaterialProperty _Queue = null;
    private MaterialProperty _UseCustomQueue = null;

    private MaterialProperty _ZWrite = null;

    public override void OnGUI(MaterialEditor editor, MaterialProperty[] properties)
    {
        Material material; Shader shader; PrepareGUI(editor, properties, out material, out shader);

        DrawBanner(shader.name.Split('/').Last(), "Open Discord", "https://discord.gg/E3HPmNR");

        EditorGUILayout.BeginVertical("GroupBox");

        Header("Color Settings");

        EditorGUILayout.BeginVertical(GUI.skin.box);
        editor.ShaderProperty(_Color1, MakeLabel(_Color1));
        editor.ShaderProperty(_Color2, MakeLabel(_Color2));
        editor.ShaderProperty(_Color3, MakeLabel(_Color3));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        editor.ShaderProperty(HUE, MakeLabel(HUE));
        if (material.GetInt("HUE") == 1)
        {
            editor.ShaderProperty(_hue, MakeLabel(_hue));
            editor.ShaderProperty(_hueo, MakeLabel(_hueo));
            editor.ShaderProperty(_huespeed, MakeLabel(_huespeed));
            editor.ShaderProperty(_hueoc, MakeLabel(_hueoc));
            editor.ShaderProperty(_huesaturation, MakeLabel(_huesaturation));
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("GroupBox");

        Header("Black Hole Settings");

        EditorGUILayout.BeginVertical(GUI.skin.box);
        editor.ShaderProperty(_bhs, MakeLabel(_bhs));
        editor.ShaderProperty(_gravitation, MakeLabel(_gravitation));
        editor.ShaderProperty(_gravitationscale, MakeLabel(_gravitationscale));
        EditorGUILayout.EndVertical();
        
        EditorGUILayout.BeginVertical(GUI.skin.box);
        editor.ShaderProperty(_radialdistortion, MakeLabel(_radialdistortion));
        editor.ShaderProperty(_absorptionspeed, MakeLabel(_absorptionspeed));
        editor.ShaderProperty(_rotationspeed, MakeLabel(_rotationspeed));
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("GroupBox");

        Header("Advanced Settings");

        EditorGUILayout.BeginVertical(GUI.skin.box);
        editor.ShaderProperty(_Stencil, MakeLabel(_Stencil));
        editor.ShaderProperty(_ReadMask, MakeLabel(_ReadMask));
        editor.ShaderProperty(_WriteMask, MakeLabel(_WriteMask));
        editor.ShaderProperty(_StencilComp, MakeLabel(_StencilComp));
        editor.ShaderProperty(_StencilOp, MakeLabel(_StencilOp));
        editor.ShaderProperty(_StencilFail , MakeLabel(_StencilFail ));
        editor.ShaderProperty(_StencilZFail, MakeLabel(_StencilZFail));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        editor.ShaderProperty(_UseCustomQueue, MakeLabel(_UseCustomQueue));
        EditorGUI.BeginDisabledGroup(material.GetInt("_UseCustomQueue") == 0);
        editor.ShaderProperty(_Queue, MakeLabel(_Queue));
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        editor.ShaderProperty(_ZWrite, MakeLabel(_ZWrite));
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();

        DrawBanner("Patreon", "Open Patreon", "https://www.patreon.com/dopestuff");

        DrawCredits();
    }

    private void PrepareGUI(MaterialEditor editor, MaterialProperty[] properties, out Material material, out Shader shader)
    {
        material = editor.target as Material;
        shader = material.shader;

        //Thanks Xiexe
        foreach (var property in GetType().GetFields(bindingFlags))
        {
            if (property.FieldType == typeof(MaterialProperty))
            {
                try { property.SetValue(this, FindProperty(property.Name, properties)); } catch { }
            }
        }
        //

        if (material.GetInt("_UseCustomQueue") == 1)
        {
            material.renderQueue = Mathf.Clamp(material.GetInt("_Queue"), 0, int.MaxValue); //2147483647
        }
        else
        {
            material.renderQueue = -1;
            material.SetInt("_Queue", 4000);
        }
    }

    private void DrawTexture(MaterialEditor editor, MaterialProperty tex, MaterialProperty prop = null)
    {
        GUILayout.Space(3);
        editor.TexturePropertySingleLine(MakeLabel(tex), tex, prop);
        editor.TextureScaleOffsetProperty(tex);
        GUILayout.Space(3);
    }

    private void DrawBanner(string MainText, string OtherText, string URL)
    {
        GUIStyle OtherStyle = new GUIStyle { font = VrchatFont, alignment = TextAnchor.LowerRight, fontSize = 12, padding = new RectOffset(0, 1, 0, 1) };
        OtherStyle.normal.textColor = new Color(0.9f, 0.9f, 0.9f);

        GUIStyle MainStyle = new GUIStyle { font = VrchatFont, alignment = TextAnchor.MiddleCenter, fontSize = 18, padding = new RectOffset(0, 1, 0, 1) };
        MainStyle.normal.textColor = new Color(1f, 1f, 1f);

        EditorGUILayout.BeginVertical("GroupBox");
        Rect BannerRect = GUILayoutUtility.GetRect(0, int.MaxValue, 35, 35);
        EditorGUI.DrawRect(BannerRect, new Color(0f, 0f, 0f));
        EditorGUI.LabelField(BannerRect, OtherText, OtherStyle);
        EditorGUI.LabelField(BannerRect, MainText, MainStyle);

        if (GUI.Button(BannerRect, string.Empty, new GUIStyle()))
        {
            Application.OpenURL(URL);
        }

        EditorGUILayout.EndVertical();
    }

    private static GUIContent MakeLabel(MaterialProperty property, string tooltip = null)
    {
        GUIContent staticLabel = new GUIContent { text = property.displayName, tooltip = tooltip };
        return staticLabel;
    }

    private void DrawCredits()
    {
        EditorGUILayout.BeginVertical("GroupBox");

        GUIStyle TextStyle = new GUIStyle { font = VrchatFont, fontSize = 15, fontStyle = FontStyle.Italic };
        GUILayout.Label("Shader by:", TextStyle);
        GUILayout.Space(2);
        GUILayout.Label("Doppelgänger#8376", TextStyle);
        GUILayout.Space(6);
        GUILayout.Label("Thanks for help with editor:", TextStyle);
        GUILayout.Space(2);
        GUILayout.Label("Bkp#8336", TextStyle);

        EditorGUILayout.EndVertical();
    }

    private void Header(string name)
    {
        GUIStyle Style = new GUIStyle { font = VrchatFont, fontSize = 18, fontStyle = FontStyle.Italic, alignment = TextAnchor.MiddleLeft };
        GUILayout.Label(name, Style);
        GUILayout.Space(5);
    }
}
#endif