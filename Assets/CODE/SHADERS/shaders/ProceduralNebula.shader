Shader "Skybox/ProceduralNebulaWithScar"
{
    Properties
    {
        [Header(Nebula Settings)]
        _Color1("Nebula Color 1", Color) = (0.1, 0.1, 0.1, 1)
        _Color2("Nebula Color 2", Color) = (0.0, 1.0, 1.0, 1)
        _Color3("Nebula Color 3", Color) = (1.0, 0.2, 0.8, 1)
        _NebulaScale("Nebula Scale", Range(0.1, 10)) = 2.0
        _Intensity("Nebula Intensity", Range(0, 5)) = 1.5
        _WarpStrength("Warp Strength", Range(0.1, 3)) = 0.8
        _FlowSpeed("Flow Speed", Range(0, 1)) = 0.1
        _PulseSpeed("Pulse Speed", Range(0, 0.5)) = 0.1

        [Header(Stars)]
        _StarColor("Star Color", Color) = (1, 1, 1, 1)
        _StarDensity("Star Density", Range(0, 10)) = 5.0
        _StarBrightness("Star Brightness", Range(0, 5)) = 1.0
        _StarSize("Star Size", Range(0.1, 3)) = 1.0
        _TwinkleSpeed("Twinkle Speed", Range(0, 2)) = 0.5

        [Header(Scar Band)]
        _ClusterColor1("Scar Color 1", Color) = (1, 0.8, 0.5, 1)
        _ClusterColor2("Scar Color 2", Color) = (0.5, 0.6, 1, 1)
        _ClusterIntensity("Scar Intensity", Range(0, 10)) = 2.0
        _ClusterWidth("Scar Width", Range(0.1, 2)) = 0.5
        _ClusterLength("Scar Length", Range(0.1, 2)) = 1.00
        [Header(Scar Animation)]
        _ScarRotationX ("Scar Rotation X", Range(0, 360)) = 0.0
        _ScarRotationY ("Scar Rotation Y", Range(0, 360)) = 0.0
        _ScarRotationZ ("Scar Rotation Z", Range(0, 360)) = 0.0
        _ScarPositionX ("Scar Position X", Range(-1, 1)) = 0.0
        _ScarPositionY ("Scar Position Y", Range(-1, 1)) = 0.0
        _ScarPositionZ ("Scar Position Z", Range(-1, 1)) = 0.0
        [Space(10)]
        _AnimationSpeed ("Animation Speed", Range(0, 2)) = 0.5
        _RotationAmount ("Rotation Intensity", Range(0, 2)) = 1.0
        _PositionAmount ("Position Intensity", Range(0, 2)) = 1.0
        
        [Header(AudioLink)]
        [ToggleUI] _AudioLinkEnabled ("Enable AudioLink", Float) = 1.0
        _AudioLinkBand ("AudioLink Band", Range(0, 3)) = 0
        _AudioLinkStrength ("AudioLink Strength", Range(0, 5)) = 1.0
        _AudioLinkSmoothing ("AudioLink Smoothing", Range(0, 1)) = 0.3
        _AudioLinkMinValue ("AudioLink Min Value", Range(0, 1)) = 0.2
        _AudioLinkPulseAmount ("Pulse Amount", Range(0, 2)) = 0.5
        _AudioLinkPulseSpeed ("Pulse Speed", Range(0.1, 5)) = 1.0
        _ColorScrollSpeed ("Color Scroll Speed", Range(0, 2)) = 0.5
        _ColorScrollAmount ("Color Scroll Amount", Range(0, 1)) = 0.2
    }

    SubShader
    {
        Tags { "Queue" = "Background" "RenderType" = "Skybox" }
        Cull Off ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // Uncomment for debug visualization:
            // #define DEBUG_AUDIOLINK
            #include "UnityCG.cginc"
            #include "Packages/com.llealloo.audiolink/Runtime/Shaders/AudioLink.cginc"
            
            // Voronoi mask to shape AudioLink response
            float hash21(float2 p) {
                p = frac(p * float2(123.34, 345.45));
                p += dot(p, p + 34.345);
                return frac(p.x * p.y);
            }

            float voronoi(float3 p) {
                float3 g = floor(p);
                float3 f = frac(p);
                float res = 1.0;
                for (int z = -1; z <= 1; z++) {
                    for (int y = -1; y <= 1; y++) {
                        for (int x = -1; x <= 1; x++) {
                            float3 b = float3(x, y, z);
                            float3 r = b - f + hash21(g.xy + b.xy);
                            float d = dot(r, r);
                            res = min(res, d);
                        }
                    }
                }
                return sqrt(res);
            }

            float fbmMask(float3 p) {
                return saturate(1.0 - voronoi(p * 2.5));
            }

            // Fractal Brownian Motion mask for audio reactivity
            float fbmMaskOld(float3 p) {
                float val = 0, amp = 0.5;
                for (int i = 0; i < 4; i++) {
                    val += amp * (sin(dot(p, float3(12.9898, 78.233, 45.164))) * 0.5 + 0.5);
                    p *= 2.0;
                    amp *= 0.5;
                }
                return val;
            }
            
            // Hash function for noise generation
            float hash(float3 p) { 
                return frac(sin(dot(p, float3(127.1, 311.7, 74.7))) * 43758.5453); 
            }
            
            // Fractal noise function for organic intensity variation
            float fractalNoise(float3 p) {
                float3 i = floor(p);
                float3 f = frac(p);
                
                // Smooth interpolation
                float3 u = f * f * (3.0 - 2.0 * f);
                
                // Generate random values for each corner
                float a = hash(i + float3(0.0, 0.0, 0.0));
                float b = hash(i + float3(1.0, 0.0, 0.0));
                float c = hash(i + float3(0.0, 1.0, 0.0));
                float d = hash(i + float3(1.0, 1.0, 0.0));
                
                // Bilinear interpolation
                return lerp(
                    lerp(a, b, u.x),
                    lerp(c, d, u.x),
                    u.y);
            }
            
            // Generate fractal noise with multiple octaves
            float fractalNoise3D(float3 p, int octaves, float persistence, float lacunarity) {
                float total = 0.0;
                float frequency = 1.0;
                float amplitude = 1.0;
                float maxValue = 0.0;
                
                for(int i = 0; i < octaves; i++) {
                    total += fractalNoise(p * frequency) * amplitude;
                    maxValue += amplitude;
                    amplitude *= persistence;
                    frequency *= lacunarity;
                }
                
                return total / maxValue;
            }
            
            // Smoothing function for audio data
            float SmoothAudio(float current, float previous, float smoothing) {
                return lerp(previous, current, 1.0 - exp(-_Time.y * 5.0 * (1.0 - smoothing)));
            }
            
            // Get smoothed audio data with blending between bands
            float GetSmoothedAudio(int band, float smoothing) {
                // Get current and adjacent bands for smoother response
                float current = AudioLinkData(ALPASS_AUDIOLINK + uint2(band, 0)).r;
                float prev = (band > 0) ? AudioLinkData(ALPASS_AUDIOLINK + uint2(band-1, 0)).r : current;
                float next = (band < 3) ? AudioLinkData(ALPASS_AUDIOLINK + uint2(band+1, 0)).r : current;
                
                // Blend current band with adjacent bands for smoother response
                float blended = current * 0.6 + prev * 0.2 + next * 0.2;
                
                // Apply smoothing
                float smooth = 0.0;
                smooth = SmoothAudio(blended, smooth, smoothing);
                
                // Apply gentle curve to make response more natural
                return smooth * (2.0 - smooth);
            }
            
            // RGB to HSV and back conversion functions
            float3 RGBtoHSV(float3 c) {
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
                float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));
                float d = q.x - min(q.w, q.y);
                float e = 1.0e-10;
                return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }

            float3 HSVtoRGB(float3 c) {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
                return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
            }
            
            // AudioLink properties
            float _AudioLinkEnabled;
            float _AudioLinkBand;
            float _AudioLinkStrength;
            float _AudioLinkSmoothing;
            float _AudioLinkMinValue;
            float _AudioLinkPulseAmount;
            float _AudioLinkPulseSpeed;
            float _ColorScrollSpeed;
            float _ColorScrollAmount;
            
            // AudioLink smoothing
            float _LastAudioValue = 0;
            
            // Animation controls
            float _AnimationSpeed;
            float _RotationAmount;
            float _PositionAmount;

            fixed4 _Color1, _Color2, _Color3;
            float _NebulaScale, _Intensity, _WarpStrength, _FlowSpeed, _PulseSpeed;
            fixed4 _StarColor;
            float _StarDensity, _StarBrightness, _TwinkleSpeed, _StarSize;
            fixed4 _ClusterColor1, _ClusterColor2;
            float _ClusterIntensity, _ClusterWidth, _ClusterLength;
            float _ScarRotationX, _ScarRotationY, _ScarRotationZ;
            float _ScarPositionX, _ScarPositionY, _ScarPositionZ;

            struct appdata { float4 vertex : POSITION; };
            struct v2f { float4 vertex : SV_POSITION; float3 dir : TEXCOORD0; };

            float noise(float3 p)
            {
                float3 i = floor(p);
                float3 f = frac(p);
                float3 u = f * f * (3.0 - 2.0 * f);
                return lerp(lerp(lerp(hash(i + float3(0,0,0)), hash(i + float3(1,0,0)), u.x),
                                 lerp(hash(i + float3(0,1,0)), hash(i + float3(1,1,0)), u.x), u.y),
                            lerp(lerp(hash(i + float3(0,0,1)), hash(i + float3(1,0,1)), u.x),
                                 lerp(hash(i + float3(0,1,1)), hash(i + float3(1,1,1)), u.x), u.y), u.z);
            }

            float3 hash3(float3 p)
            {
                p = float3(
                    dot(p, float3(127.1, 311.7, 74.7)),
                    dot(p, float3(269.5, 183.3, 246.1)),
                    dot(p, float3(113.5, 271.9, 124.6))
                );
                return frac(sin(p) * 43758.5453);
            }

            float cellular(float3 p)
            {
                float3 fp = frac(p);
                float3 ip = floor(p);
                float minDist = 1.0;
                for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                for (int z = -1; z <= 1; z++)
                {
                    float3 g = float3(x, y, z);
                    float3 o = hash3(ip + g);
                    float3 r = g - fp + o;
                    float dist = dot(r, r);
                    minDist = min(minDist, dist);
                }
                return 1.0 - sqrt(minDist);
            }

            float worleyFBM(float3 p)
            {
                float value = 0.0;
                float scale = 1.0;
                float weight = 0.5;
                for (int i = 0; i < 4; i++)
                {
                    value += weight * cellular(p * scale);
                    scale *= 2.0;
                    weight *= 0.5;
                }
                return value;
            }

            float fbm(float3 p)
            {
                float val = 0, amp = 0.5;
                for (int i = 0; i < 5; i++) {
                    val += amp * noise(p); p *= 2.0; amp *= 0.5;
                }
                return val;
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.dir = normalize(mul(unity_ObjectToWorld, v.vertex).xyz);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 dir = normalize(i.dir);
                float t = _Time.y * 0.05;
                float pulse = 1.0 + sin(t * _PulseSpeed * 10.0) * 0.1;
                float3 flow = float3(t * _FlowSpeed, t * _FlowSpeed * 0.7, 0);

                // Animated rotation and position for the scar
                float time = _Time.y * _AnimationSpeed;
                float rotationIntensity = _RotationAmount * 180.0; // Scale to degrees
                float positionIntensity = _PositionAmount * 0.5;   // Scale to reasonable position offset
                
                float animatedRotX = radians(sin(time * 0.2) * rotationIntensity);
                float animatedRotY = radians(cos(time * 0.13) * rotationIntensity);
                float animatedRotZ = radians(sin(time * 0.17) * rotationIntensity);

                // Rotation matrices with animation
                float3x3 rotX = float3x3(
                    1, 0, 0,
                    0, cos(animatedRotX), -sin(animatedRotX),
                    0, sin(animatedRotX), cos(animatedRotX)
                );
                float3x3 rotY = float3x3(
                    cos(animatedRotY), 0, sin(animatedRotY),
                    0, 1, 0,
                    -sin(animatedRotY), 0, cos(animatedRotY)
                );
                float3x3 rotZ = float3x3(
                    cos(animatedRotZ), -sin(animatedRotZ), 0,
                    sin(animatedRotZ), cos(animatedRotZ), 0,
                    0, 0, 1
                );
                
                // Animated offset
                float3 animatedOffset = float3(
                    _ScarPositionX + sin(time * 0.15) * positionIntensity,
                    _ScarPositionY + cos(time * 0.2) * (positionIntensity * 0.6),
                    _ScarPositionZ + sin(time * 0.12) * (positionIntensity * 0.8)
                );
                
                // Apply rotation and animated position offset
                float3 offsetDir = dir - animatedOffset;
                float3 rotatedDir = mul(rotZ, mul(rotY, mul(rotX, offsetDir)));
                
                // Calculate scar direction and perpendicular
                float3 clusterDir = normalize(float3(1.0, 0.0, 0.0)); // Pointing along X axis
                float3 perpDir = normalize(cross(clusterDir, float3(0.0, 1.0, 0.0)));
                float alongScar = dot(rotatedDir, clusterDir);
                float acrossScar = dot(rotatedDir, perpDir);

                float scarWidth = 0.15 * _ClusterWidth;
                float scarLength = 1.5 * _ClusterLength;
                float scarProgress = (alongScar * 0.5 + 0.5) * scarLength;
                float falloff = smoothstep(0.1, 0.9, 1.0 - abs(scarProgress - 0.5) * 2.0);
                float widthFalloff = 0.8 + 0.2 * sin(scarProgress * 3.14159);
                float scarBand = 1.0 - smoothstep(0.0, scarWidth * widthFalloff, abs(acrossScar));
                scarBand *= falloff;

                float scarMask = worleyFBM(dir * _NebulaScale * 3.0);
                scarMask *= scarBand;
                scarMask = pow(scarMask, 2.0);

                float nebulaNoise = fbm(dir * _NebulaScale + flow);
                // Audio processing is handled below after nebula color setup
                
                // Create base nebula color with hue shifting
                float3 nebulaColor = lerp(_Color1.rgb, _Color2.rgb, smoothstep(0.2, 0.6, nebulaNoise));
                nebulaColor = lerp(nebulaColor, _Color3.rgb, smoothstep(0.6, 1.0, nebulaNoise));
                
                // Apply smooth hue shifting over time
                float3 hsv = RGBtoHSV(nebulaColor);
                hsv.x = frac(hsv.x + _Time.y * _ColorScrollSpeed * 0.05); // Slow hue shift over time
                nebulaColor = HSVtoRGB(hsv);
                
                // Debug: Visualize audio data
                #if defined(UNITY_EDITOR) || defined(DEBUG_AUDIOLINK)
                if (_AudioLinkEnabled > 0.5 && (dir.y > 0.98 || dir.y < -0.98)) {
                    // Show audio level as a horizontal bar at the top/bottom of the screen
                    float audioLevel = GetAudioLinkData(0); // Direct bass band access
                    float barPos = (dir.x + 1.0) * 0.5; // Convert from -1..1 to 0..1
                    if (abs(dir.y) > 0.98 && barPos < audioLevel) {
                        return fixed4(1, 1, 1, 1); // White bar showing audio level
                    }
                }
                #endif
                
                // Apply fractal noise for organic intensity variation
                float fractalIntensity = 1.0;
                if (_AudioLinkEnabled > 0.5) {
                    // Get audio level from selected band
                    float audioLevel = AudioLinkData(ALPASS_AUDIOLINK + int(_AudioLinkBand)).r;
                    
                    // Generate fractal noise coordinates based on position and time
                    float3 noisePos = dir * 3.0 + float3(0, 0, _Time.y * _ColorScrollSpeed * 0.1);
                    
                    // Generate fractal noise (3 octaves for organic look)
                    float noise = fractalNoise3D(noisePos, 3, 0.5, 2.0);
                    
                    // Map noise to [0.5, 1.5] range and modulate with audio
                    float noiseMod = lerp(0.5, 1.5, noise);
                    fractalIntensity = lerp(1.0, noiseMod, audioLevel * _AudioLinkStrength);
                }
                // Calculate audio value with voronoi-based audio masking
                float audioValue = 1.0;
                if (_AudioLinkEnabled > 0.5) {
                    float rawAudio = AudioLinkData(ALPASS_AUDIOLINK + int(_AudioLinkBand)).r;
                    float pulse = 1.0 + (sin(_Time.y * _AudioLinkPulseSpeed * 0.1) * 0.5 + 0.5) * _AudioLinkPulseAmount;
                    float mask = fbmMask(normalize(dir));
                    audioValue = max(_AudioLinkMinValue, rawAudio * _AudioLinkStrength * pulse * mask);
                }
                
                // Apply fractal intensity to the nebula
                float3 nebula = nebulaColor * scarMask * _ClusterIntensity * _Intensity * audioValue * fractalIntensity;
                
                // Visualize audio for debugging (uncomment if needed)
                // if (length(dir - float3(0,0,1)) < 0.01) return fixed4(audioValue, audioValue, audioValue, 1.0);

                // Fully procedural Fibonacci sphere starfield: multi-star blend, halos, color ramp, clustering
                float3 d = normalize(dir);
                // Star density controls probability, not count. Use fixed maxStars.
                int maxStars = 512;
                int blendCount = 3; // Blend closest N stars per pixel
                float3 starColorRamp[7];
                starColorRamp[0] = float3(0.65, 0.8, 1.0); // Blue
                starColorRamp[1] = float3(0.8, 0.9, 1.0); // Blue-white
                starColorRamp[2] = float3(0.9, 0.95, 1.0); // White
                starColorRamp[3] = float3(1.0, 1.0, 0.9); // Yellow-white
                starColorRamp[4] = float3(1.0, 0.9, 0.7); // Yellow
                starColorRamp[5] = float3(1.0, 0.7, 0.5); // Orange
                starColorRamp[6] = float3(1.0, 0.5, 0.4); // Red
                float3 stars = float3(0,0,0);
                float3 blendDirs[3]; float blendDists[3]; int blendIdx[3];
                [unroll]
                for (int j = 0; j < blendCount; j++) { blendDists[j] = 100.0; blendIdx[j] = -1; }
                // Find closest N stars
                [loop]
                for (int i = 0; i < maxStars; i++) {
                    // Fibonacci sphere: procedural blue-noise direction
                    float phi = acos(1.0 - 2.0 * (i + 0.5) / maxStars);
                    float theta = UNITY_TWO_PI * frac(i * 0.61803398875);
                    // Subtle clustering (Milky Way band)
                    float clustering = smoothstep(0.25, 0.0, abs(phi - UNITY_PI/2.0 - 0.08 * sin(theta * 2.0 + 1.7)));
                    phi = lerp(phi, UNITY_PI/2.0, clustering * 0.3); // More subtle band
                    // Add random jitter to direction
                    float jitterA = (hash(i * 99.99) - 0.5) * 0.05;
                    float jitterB = (hash(i * 77.77) - 0.5) * 0.05;
                    phi += jitterA;
                    theta += jitterB;
                    float3 sd = float3(sin(phi) * cos(theta), cos(phi), sin(phi) * sin(theta));
                    // Density gating: only allow star if hash is below _StarDensity
                    float starActive = step(hash(i * 123.456), _StarDensity);
                    if (starActive < 0.5) continue;
                    // Find angular distance
                    float dist = acos(dot(d, sd));
                    // Insert into blend list if closer
                    for (int k = 0; k < blendCount; k++) {
                        if (dist < blendDists[k]) {
                            for (int m = blendCount-1; m > k; m--) { blendDists[m] = blendDists[m-1]; blendIdx[m] = blendIdx[m-1]; blendDirs[m] = blendDirs[m-1]; }
                            blendDists[k] = dist; blendIdx[k] = i; blendDirs[k] = sd;
                            break;
                        }
                    }
                }
                // Blend closest N stars
                for (int k = 0; k < blendCount; k++) {
                    if (blendIdx[k] < 0) continue;
                    float3 qd = blendDirs[k];
                    int idx = blendIdx[k];
                    float colorHash = hash(qd * 123.456 + idx);
                    float sizeHash = hash(qd * 789.123 + idx);
                    float twinkleHash = hash(qd * 456.789 + idx);
                    float horizonEffect = 1.0 - abs(qd.y);
                    float sizeControl = pow(_StarSize, 1.5); // finer control at low end
                    float baseStarSize = 0.003 * sizeControl;
                    float horizonStarSize = 0.01 * sizeControl;
                    float starSize = lerp(baseStarSize, horizonStarSize, horizonEffect * horizonEffect);
                    starSize *= lerp(0.8, 1.2, sizeHash);
                    float fade = smoothstep(-starSize, starSize, blendDists[k]);
                    // Halo: soft Gaussian-like
                    float halo = exp(-pow(blendDists[k] / (starSize * 2.5), 2.0));
                    float twinkle = 0.7 + 0.3 * sin(_Time.y * _TwinkleSpeed * 5.0 + twinkleHash * 100.0);
                    float rampIndex = colorHash * 6.0;
                    int rampIdxA = int(floor(rampIndex));
                    int rampIdxB = min(rampIdxA + 1, 6);
                    float rampLerp = frac(rampIndex);
                    float3 spectralTint = lerp(starColorRamp[rampIdxA], starColorRamp[rampIdxB], rampLerp);
                    spectralTint = lerp(spectralTint, float3(1,0.7,0.5), saturate((_StarSize-1.0)/2.0));
                    float starPresent = 1.0; // All procedural stars present
                    stars += _StarColor.rgb * spectralTint * ((1.0 - fade) + 0.7 * halo) * twinkle * _StarBrightness * starPresent * lerp(1.0, 0.8, horizonEffect * 0.5) / blendCount;
                }


                // Optional: add very slight bloom halo (comment out if not needed)
                // stars += starValue * 0.05 * _StarBrightness;

                // Add stars over nebula
                float3 finalColor = nebula + stars * (1.0 - saturate(length(nebula)));
                finalColor = max(finalColor, float3(0.0, 0.0, 0.0));

                return fixed4(saturate(finalColor), 1.0);
            }
            ENDCG
        }
    }
}