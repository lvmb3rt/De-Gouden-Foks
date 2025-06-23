Shader "Custom/ProceduralWaterCurvedFoam"
{
    Properties
    {
        _BaseColor ("Water Color", Color) = (0.1, 0.4, 0.7, 1)
        _FoamColor ("Foam Color", Color) = (1,1,1,1)
        _FlowSpeed ("Flow Speed", Range(0, 5)) = 1
        _WaveScale ("Wave Scale", Range(0.1, 5)) = 1
        _WaveStrength ("Wave Height", Range(0, 1)) = 0.1
        _SplashPoint ("Splash Center", Vector) = (0,0,0,0)
        _SplashRadius ("Splash Radius", Float) = 2
        _FoamIntensity ("Foam Intensity", Range(0, 1)) = 1
        _FoamStripeScale ("Foam Stripe Scale", Range(5, 50)) = 20
        _FoamStripeSpeed ("Foam Stripe Speed", Range(0, 10)) = 4
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _BaseColor;
            float4 _FoamColor;
            float _FlowSpeed;
            float _WaveScale;
            float _WaveStrength;
            float4 _SplashPoint;
            float _SplashRadius;
            float _FoamIntensity;
            float _FoamStripeScale;
            float _FoamStripeSpeed;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
            };

            float noise(float2 p)
            {
                return sin(p.x) * cos(p.y);
            }

            float3 fakeNormal(float2 uv, float time)
            {
                float eps = 0.01;
                float wave1 = noise(uv * _WaveScale + time);
                float wave2 = noise((uv + float2(eps, 0)) * _WaveScale + time);
                float wave3 = noise((uv + float2(0, eps)) * _WaveScale + time);

                float dx = (wave2 - wave1) / eps;
                float dy = (wave3 - wave1) / eps;

                float3 n = normalize(float3(-dx, 1.0, -dy));
                return n;
            }

            float slopeFlow(float3 normal)
            {
                return 1.0 - saturate(dot(normal, float3(0, 1, 0)));
            }

            float foamMask(float3 worldPos, float3 splashPoint, float splashRadius)
            {
                float dist = distance(worldPos, splashPoint);
                return saturate(1.0 - dist / splashRadius);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = normalize(mul(v.normal, (float3x3)unity_ObjectToWorld));
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float t = _Time.y * _FlowSpeed;

                float slope = slopeFlow(i.worldNormal);
                float foamSlope = slope;
                float foamSplash = foamMask(i.worldPos, _SplashPoint.xyz, _SplashRadius);
                float foamBase = saturate(foamSlope + foamSplash) * _FoamIntensity;

                // Foam Flow Along Inverted Slope Direction with Noise Offset
                float3 slopeDir = normalize(-float3(i.worldNormal.x, 0, i.worldNormal.z));
                float foamFlowCoord = dot(i.worldPos.xz, slopeDir.xz) * _FoamStripeScale + t * _FoamStripeSpeed;
                float stripeOffset = sin(dot(i.worldPos.xz, float2(3.1, 2.7))) * 0.5 + 0.5;
                foamFlowCoord += stripeOffset * 1.5;

                float foamPattern = sin(foamFlowCoord) * 0.5 + 0.5;
                foamPattern = step(0.5, foamPattern);
                float foamAmount = foamBase * foamPattern;

                // Procedural normal
                float3 n = fakeNormal(i.uv, t) * _WaveStrength + float3(0, 1, 0);
                n = normalize(n);

                // Fake lighting
                float3 lightDir = normalize(float3(0.4, 1, 0.3));
                float NdotL = saturate(dot(n, lightDir));
                float3 waterLit = _BaseColor.rgb * (0.4 + 0.6 * NdotL);

                float3 col = lerp(waterLit, _FoamColor.rgb, foamAmount);
                float alpha = 0.6 + foamAmount * 0.4;

                return float4(col, alpha);
            }
            ENDCG
        }
    }
}
