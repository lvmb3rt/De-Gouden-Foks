Shader "Mochie/Glass" {
    Properties {
        _GrabpassTint("Grabpass Tint", Color) = (1,1,1,1)
        _SpecularityTint("Specularity Tint", Color) = (1,1,1,1)
        _BaseColorTint("Base Color Tint", Color) = (1,1,1,1)

        _BaseColor("Base Color", 2D) = "black" {}
        _RoughnessMap("Roughness Map", 2D) = "white" {}
        _MetallicMap("Metallic Map", 2D) = "white" {}
        _OcclusionMap("Occlusion Map", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}
        _Roughness("Roughness", Range(0,1)) = 0
        _Metallic("Metallic", Range(0,1)) = 0
        _Occlusion("Occlusion", Range(0,1)) = 1
        _NormalStrength("Normal Strength", Float) = 1
        [KeywordEnum(ULTRA, HIGH, MED, LOW)]BlurQuality("Blur Quality", Int) = 1
        _Blur("Blur Strength", Float) = 1
        _Refraction("Refraction Strength", Float) = 5
        [ToggleUI]_RefractMeshNormals("Refract Mesh Normals", Int) = 0

        [Toggle(_RAIN_ON)]_RainToggle("Enable", Int) = 0
        [HideInInspector]_RainSheet("Texture Sheet", 2D) = "black" {}
        [HideInInspector]_Rows("Rows", Float) = 8
        [HideInInspector]_Columns("Columns", Float) = 8
        _Speed("Speed", Float) = 60
        _XScale("X Scale", Float) = 1.5
        _YScale("Y Scale", Float) = 1.5
        _Strength("Normal Strength", Float) = 0.3

        [Toggle(_REFLECTIONS_ON)]_ReflectionsToggle("Reflections", Int) = 1
        [Toggle(_SPECULAR_HIGHLIGHTS_ON)]_SpecularToggle("Specular Highlights", Int) = 1
        [Toggle(_LIT_BASECOLOR_ON)]_LitBaseColor("Lit Base Color", Int) = 1
        [Enum(UnityEngine.Rendering.CullMode)]_Culling("Culling", Int) = 2
        [Enum(Grabpass,0, Premultiplied,1, Off,2)]_BlendMode("Transparency", Int) = 0
        [HideInInspector]_SrcBlend("Src Blend", Int) = 1
        [HideInInspector]_DstBlend("Dst Blend", Int) = 0
    }
    SubShader {
        Tags { 
            "RenderType"="Transparent"
            "Queue"="Transparent"
            "ForceNoShadowCaster"="True"
            "IgnoreProjector"="True"
        }
        GrabPass {
            Tags {"LightMode"="Always"}
            "_GlassGrab"
        }
        Cull [_Culling]
        Blend [_SrcBlend] [_DstBlend]
        ZWrite Off

        Pass {
            Name "ForwardBase"
            Tags {"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _RAIN_ON _REFLECTIONS_ON _SPECULAR_HIGHLIGHTS_ON _LIT_BASECOLOR_ON
            #include "UnityCG.cginc"

            // Properties
            sampler2D _BaseColor;
            sampler2D _RoughnessMap;
            sampler2D _MetallicMap;
            sampler2D _OcclusionMap;
            sampler2D _NormalMap;
            float _Roughness;
            float _Metallic;
            float _Occlusion;
            float _NormalStrength;
            float4 _GrabpassTint;
            float4 _SpecularityTint;
            float4 _BaseColorTint;

            // Structs
            struct v2f {
                float2 uv : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
                float3 normal : TEXCOORD3;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            // Vertex shader
            v2f vert (appdata_full v, uint id : SV_InstanceID) {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.uv = v.texcoord;
                o.viewDir = UnityWorldSpaceViewDir(o.worldPos);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            // Fragment shader
            fixed4 frag (v2f i, bool isFrontFace : SV_IsFrontFace) : SV_Target {

                // Refraction
                float3 refractedDir = refract(normalize(i.viewDir), normalize(i.normal), _Refraction);

                // Normal mapping
                float3 normalMap = UnpackNormal(tex2D(_NormalMap, i.uv));
                normalMap = normalize(normalMap * _NormalStrength);
                float3 normal = normalize(normalMap);

                // Roughness and metallic
                float roughness = _Roughness;
                float metallic = _Metallic;

                // Base color
                float4 baseColorTex = tex2D(_BaseColor, i.uv) * _BaseColorTint;
                float3 baseColor = baseColorTex.rgb * baseColorTex.a;

                // Roughness map
                float roughnessMapValue = tex2D(_RoughnessMap, i.uv).r;
                roughness *= roughnessMapValue;

                // Metallic map
                float metallicMapValue = tex2D(_MetallicMap, i.uv).r;
                metallic *= metallicMapValue;

                // Occlusion map
                float occlusionMapValue = tex2D(_OcclusionMap, i.uv).r;
                occlusionMapValue *= _Occlusion;

                // Calculate final color
                float3 finalColor = baseColor;

                // Reflections
                #ifdef _REFLECTIONS_ON
                    float4 reflection = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, reflect(i.viewDir, normal));
                    finalColor += reflection.rgb;
                #endif

                // Specular highlights
                #ifdef _SPECULAR_HIGHLIGHTS_ON
                    float3 specColor = _SpecularityTint.rgb;
                    float specIntensity = _SpecularityTint.a;
                    float specPower = 1.0;
                    float spec = specIntensity * pow(saturate(dot(reflect(i.viewDir, normal), i.normal)), specPower);
                    finalColor += spec * specColor;
                #endif

                // Lit base color
                #ifdef _LIT_BASECOLOR_ON
                    float3 lightDir = normalize(float3(0, 0, -1));
                    float3 litColor = float3(1, 1, 1);
                    float3 litBaseColor = max(0, dot(lightDir, i.normal)) * litColor;
                    finalColor *= litBaseColor;
                #endif

                // Apply occlusion
                finalColor *= occlusionMapValue;

                // Apply grabpass tint
                finalColor *= _GrabpassTint.rgb;

                return float4(finalColor, 1.0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
