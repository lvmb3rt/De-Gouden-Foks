Shader "Custom/CustomShader" {
    Properties {
        _MainTex("Main Texture", 2D) = "white" {}
        _AlphaMask("Alpha Mask", 2D) = "white" {}
        _PackedMap("Packed Map", 2D) = "white" {}
        _DetailPackedMap("Detail Packed Map", 2D) = "white" {}
        _EmissionMap("Emission Map", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}
        _DetailAlbedoMap("Detail Albedo Map", 2D) = "white" {}
        _DetailNormalMap("Detail Normal Map", 2D) = "bump" {}
        _DetailMask("Detail Mask", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
        _DetailColor("Detail Color", Color) = (1, 1, 1, 1)
        _UseAlphaMask("Use Alpha Mask", Range(0, 1)) = 0
        _AlphaMaskOpacity("Alpha Mask Opacity", Range(0, 1)) = 1
        _UseDetailMap("Use Detail Map", Range(0, 1)) = 0
        _UseDetailMask("Use Detail Mask", Range(0, 1)) = 0
        _DetailWorkflow("Detail Workflow", Range(0, 1)) = 0
        _RoughnessChannel("Roughness Channel", Range(0, 2)) = 0
        _MetallicChannel("Metallic Channel", Range(0, 2)) = 0
        _OcclusionChannel("Occlusion Channel", Range(0, 2)) = 0
        _HeightChannel("Height Channel", Range(0, 2)) = 0
        _HeightMap("Height Map", 2D) = "white" {}
        _Workflow("Workflow", Range(0, 1)) = 0
        _RoughnessMult("Roughness Multiplier", Range(0, 1)) = 1
        _MetallicMult("Metallic Multiplier", Range(0, 1)) = 1
        _OcclusionMult("Occlusion Multiplier", Range(0, 1)) = 1
        _HeightMult("Height Multiplier", Range(0, 1)) = 1
        _DetailRoughnessMult("Detail Roughness Multiplier", Range(0, 1)) = 1
        _DetailMetallicMult("Detail Metallic Multiplier", Range(0, 1)) = 1
        _DetailOcclusionMult("Detail Occlusion Multiplier", Range(0, 1)) = 1
        _DetailHeightMult("Detail Height Multiplier", Range(0, 1)) = 1
        _NormalStrength("Normal Strength", Range(0, 1)) = 1
        _Shininess("Shininess", Range(0, 1)) = 0.5
        _EmissionColor("Emission Color", Color) = (0, 0, 0, 1)
        _EmissionColorUI("Emission Color UI", Color) = (1, 1, 1, 1)
        _Smoothness("Smoothness", Range(0, 1)) = 0.5
        _GlossMapScale("Gloss Map Scale", Range(0, 1)) = 1
        _SpecularHighlights("Specular Highlights", Range(0, 1)) = 1
        _GlossyReflections("Glossy Reflections", Range(0, 1)) = 1
        _UseBumpMap("Use Bump Map", Range(0, 1)) = 1
        _BumpMap("Bump Map", 2D) = "bump" {}
        _UseParallax("Use Parallax", Range(0, 1)) = 0
        _Parallax("Parallax", Range(0, 1)) = 0.02
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        struct Input {
            float2 uv_MainTex;
            float2 uv_AlphaMask;
            float2 uv_PackedMap;
            float2 uv_DetailPackedMap;
            float2 uv_EmissionMap;
            float2 uv_NormalMap;
            float2 uv_DetailAlbedoMap;
            float2 uv_DetailNormalMap;
            float2 uv_DetailMask;
            float2 uv_HeightMap;
            float2 uv_BumpMap;
            float2 uv_Detail;
            float3 viewDir;
        };

        sampler2D _MainTex;
        sampler2D _AlphaMask;
        sampler2D _PackedMap;
        sampler2D _DetailPackedMap;
        sampler2D _EmissionMap;
        sampler2D _NormalMap;
        sampler2D _DetailAlbedoMap;
        sampler2D _DetailNormalMap;
        sampler2D _DetailMask;
        sampler2D _HeightMap;
        sampler2D _BumpMap;

        float4 _Color;
        float4 _DetailColor;
        float _UseAlphaMask;
        float _AlphaMaskOpacity;
        float _UseDetailMap;
        float _UseDetailMask;
        float _DetailWorkflow;
        float _RoughnessChannel;
        float _MetallicChannel;
        float _OcclusionChannel;
        float _HeightChannel;
        sampler2D _Detail;
        float _Workflow;
        float _RoughnessMult;
        float _MetallicMult;
        float _OcclusionMult;
        float _HeightMult;
        float _DetailRoughnessMult;
        float _DetailMetallicMult;
        float _DetailOcclusionMult;
        float _DetailHeightMult;
        float _NormalStrength;
        float _Shininess;
        float4 _EmissionColor;
        float4 _EmissionColorUI;
        float _Smoothness;
        float _GlossMapScale;
        float _SpecularHighlights;
        float _GlossyReflections;
        float _UseBumpMap;
        float _UseParallax;
        float _Parallax;

        void surf(Input IN, inout SurfaceOutputStandard o) {
            // Base color
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            fixed4 packed = tex2D(_PackedMap, IN.uv_PackedMap);

            if (_UseAlphaMask == 1) {
                c.a *= tex2D(_AlphaMask, IN.uv_AlphaMask).r * _AlphaMaskOpacity;
            }

            // Detail map
            if (_UseDetailMap == 1) {
                fixed4 detailColor = tex2D(_Detail, IN.uv_DetailPackedMap) * _DetailColor;

                if (_DetailWorkflow == 0) {
                    c.rgb = lerp(c.rgb, detailColor.rgb, detailColor.a);
                } else {
                    c.rgb = detailColor.rgb;
                }
            }

            // Detail mask
            if (_UseDetailMask == 1) {
                fixed4 detailMask = tex2D(_DetailMask, IN.uv_DetailMask);
                c.a *= detailMask.r;
            }

            // Emission
            fixed3 emission = tex2D(_EmissionMap, IN.uv_EmissionMap).rgb * _EmissionColor.rgb;
            emission *= _EmissionColor.a;
            c.rgb += emission;

            // Normal mapping
            fixed3 normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
            fixed3 tangent = UnpackNormal(packed.xy);
            fixed3 binormal = UnpackNormal(packed.zw);
            o.Normal = mul(normal, tangent, binormal);

            // Specular
            o.Specular = _Shininess;
            o.Specular *= _SpecularHighlights;
            o.Specular *= _GlossyReflections;

            // Glossiness/Smoothness
            o.Smoothness = _Smoothness;
            o.Gloss = tex2D(_DetailAlbedoMap, IN.uv_DetailAlbedoMap).a * _GlossMapScale;

            // Bump mapping
            if (_UseBumpMap == 1) {
                fixed3 bump = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
                o.Normal = perturb_normal(o.Normal, IN.viewDir, bump * _NormalStrength);
            }

            // Parallax mapping
            if (_UseParallax == 1) {
                float parallax = tex2D(_HeightMap, IN.uv_HeightMap).r * _Parallax;
                IN.viewDir.xy /= IN.viewDir.z;
                IN.viewDir.xy *= parallax;
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}
