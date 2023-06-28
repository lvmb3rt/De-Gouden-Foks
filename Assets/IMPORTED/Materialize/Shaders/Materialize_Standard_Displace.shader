Shader "Materialize/Materialize_Standard_Displace_Emission" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpTex("Normal", 2D) = "bump" {}
        _MetallicTex("Metallic", 2D) = "black" {}
        _SmoothnessTex("Smoothness", 2D) = "black" {}
        _EdgeTex("Edge", 2D) = "grey" {}
        _AOTex("Ambient Occlusion", 2D) = "white" {}
        _EmissionTex("Emission", 2D) = "black" {}
        
        _Glossiness ("Smoothness", Range(0,5)) = 1.0
        _Metallic ("Metallic", Range(0,5)) = 1.0
        _AOPower ("AO Power", Range(0,5) ) = 1.0
        _EdgePower ("Edge Power", Range(0,5) ) = 1.0
        
        [Toggle(FLIP_NORMAL)] _FlipNormal("Flip Normal Y", Float) = 0
        
        _DisplacementTex("Displacement", 2D) = "grey" {}
        _Parallax ("Height", Range (0.0, 3.0)) = 0.5
        _ParallaxBias ("Height Bias", Range (0.0, 1.0)) = 0.5
        _EdgeLength ("Edge length", Range(3,50)) = 3
    }
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0
        #pragma multi_compile_fwdbase
        #pragma multi_compile_forwardadd
        #pragma multi_compile_shadowcaster

        #pragma shader_feature FLIP_NORMAL

        #include "UnityCG.cginc"
 
        sampler2D _MainTex;
        sampler2D _BumpTex;
        sampler2D _MetallicTex;
        sampler2D _SmoothnessTex;
        sampler2D _EdgeTex;
        sampler2D _AOTex;
        sampler2D _EmissionTex;

        float4 _Color;
        float _Glossiness;
        float _Metallic;
        float _AOPower;
        float _EdgePower;
        
        float _Parallax;
        float _ParallaxBias;
        float _EdgeLength;

        struct Input {
            float2 uv_MainTex;
            float2 uv_BumpTex;
            float2 uv_MetallicTex;
            float2 uv_SmoothnessTex;
            float2 uv_EdgeTex;
            float2 uv_AOTex;
            float2 uv_EmissionTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb * _Color.rgb;
            o.Metallic = tex2D(_MetallicTex, IN.uv_MetallicTex).r * _Metallic;
            o.Smoothness = tex2D(_SmoothnessTex, IN.uv_SmoothnessTex).r * _Glossiness;
            o.Normal = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));
            o.Occlusion = tex2D(_AOTex, IN.uv_AOTex).r * _AOPower;
            o.Emission = tex2D(_EmissionTex, IN.uv_EmissionTex).rgb;
        }
 
        ENDCG
    }
    FallBack "Diffuse"
}
