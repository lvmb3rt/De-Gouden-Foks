Shader "Custom/ViewAnglePBRWithGlow" {
    Properties {
        _Color1 ("Color 1", Color) = (1, 1, 1, 1)
        _Color2 ("Color 2", Color) = (1, 1, 1, 1)
        _Color3 ("Color 3", Color) = (1, 1, 1, 1)
        _Color4 ("Color 4", Color) = (1, 1, 1, 1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MetallicMap ("Metallic Map", 2D) = "white" {}
        _RoughnessMap ("Roughness Map", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _EmissionColor ("Emission Color", Color) = (0, 0, 0, 1)
        _EmissionStrength ("Emission Strength", Range(0, 1)) = 0
        _EnableAudioLink ("Enable AudioLink", Range(0, 1)) = 0
        _SineWaveFrequency ("Sine Wave Frequency", Range(0, 10)) = 1
        _SineWaveSpeed ("Sine Wave Speed", Range(0, 10)) = 1
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert

        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
        };

        sampler2D _MainTex;
        fixed4 _Color1;
        fixed4 _Color2;
        fixed4 _Color3;
        fixed4 _Color4;
        sampler2D _MetallicMap;
        sampler2D _RoughnessMap;
        sampler2D _NormalMap;
        fixed4 _EmissionColor;
        float _EmissionStrength;
        float _EnableAudioLink;
        float _SineWaveFrequency;
        float _SineWaveSpeed;

        void vert(inout appdata_full v) {
            // No changes in the vertex function
        }

        fixed4 InterpolateColor(float t)
        {
            float4 c = lerp(_Color1, _Color2, t);
            c = lerp(c, _Color3, t);
            c = lerp(c, _Color4, t);
            return c;
        }

        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Calculate the view direction
            float3 viewDir = normalize(_WorldSpaceCameraPos - IN.worldPos);
            
            // Calculate the angle between the view direction and the surface normal
            float viewAngle = dot(viewDir, o.Normal);
            
            // Interpolate color based on the view angle
            fixed4 gradientColor = InterpolateColor(viewAngle);
            float3 color = gradientColor.rgb;
            
            // Apply metallic and roughness from maps
            o.Metallic = tex2D(_MetallicMap, IN.uv_MainTex).r;
            o.Smoothness = 1 - tex2D(_RoughnessMap, IN.uv_MainTex).r;
            
            // Apply textures
            fixed4 albedo = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = albedo.rgb * color;
            o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));

            // Calculate emission intensity using a sine wave
            float emissionIntensity = _EmissionStrength;
            
            // Apply AudioLink integration if enabled
            if (_EnableAudioLink > 0) {
                // Add code here to retrieve the audio input from AudioLink
                float audioInput = 0.0; // Replace 0.0 with actual audio input
                
                emissionIntensity *= audioInput;
            }
            
            // Apply sine wave effect
            emissionIntensity *= sin(_Time.y * _SineWaveFrequency + _Time.x * _SineWaveSpeed);
            
            // Apply emission
            o.Emission = _EmissionColor.rgb * emissionIntensity;
        }
        ENDCG
    }
    FallBack "Standard"
}
