Shader "Custom/PlasmaShock2D"
{
    Properties
    {
        _Color("HDR Color", Color) = (1,1,1,1)
        _Emission("Emission", Range(0,10)) = 2

        _Strength("Distortion Strength", Range(0,0.2)) = 0.05
        _Radius("Max Radius", Range(0,1)) = 0.4
        _Soft("Softness", Range(0,1)) = 0.3
        _ShockPush("Shockwave Push", Range(0,0.3)) = 0.1

        _NoiseSpeed("Noise Speed", Float) = 2.0
        _NoiseAmount("Noise Amount", Float) = 0.25

        _TimeScale("Time Scale", Float) = 1.0
        _Lifetime("Lifetime", Float) = 1.0

        _MainTex("Sprite", 2D) = "white" {}

        // cracks controlles
        _CrackIntensity("Crack Intensity", Range(0,2)) = 1.0
        _CrackSharpness("Crack Sharpness", Range(0,8)) = 4.0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float4 pos : SV_POSITION; float2 uv : TEXCOORD0; float2 screenUV : TEXCOORD1; };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;

            float4 _Color;
            float _Emission;

            float _Strength;
            float _Radius;
            float _Soft;
            float _ShockPush;

            float _NoiseSpeed;
            float _NoiseAmount;

            float _TimeScale;
            float _Lifetime;

            // Crack var's
            float _CrackIntensity;
            float _CrackSharpness;

            float noise(float2 p)
            {
                return sin(p.x*12 + _Time.y*_NoiseSpeed) *
                       sin(p.y*14 + _Time.y*_NoiseSpeed*1.3);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                float4 ndc = o.pos / o.pos.w;
                o.screenUV = ndc.xy * 0.5 + 0.5;
                return o;
            }

            // Procedural crack pattern (energy fissure)
            float crackNoise(float2 uv)
            {
                float n = sin(uv.x * 40 + _Time.y * 6) * sin(uv.y * 50 - _Time.y * 4);
                n = abs(n);
                return pow(n, _CrackSharpness);
            }

            float4 frag(v2f i) : SV_Target
            {
                // Time progression normalized 0 → 1
                float localTime = fmod(_Time.y * _TimeScale, _Lifetime);
                float t = saturate(localTime / _Lifetime);

                // Used for fade-out of the entire effect
                float lifeFade = 1 - t;

                // Auto-expanding radius
                float radius = lerp(_Radius * 0.2, _Radius, t);

                float2 center = float2(0.5, 0.5);
                float dist = distance(i.screenUV, center);

                // Shockwave ring intensity
                float ring = smoothstep(radius, radius - _Soft, dist);

                float n = noise(i.screenUV * 3.0) * _NoiseAmount * lifeFade;

                // Distortion of background (subtle air shockwave)
                float2 offset = normalize(i.screenUV - center) * (ring + n) * _Strength * lifeFade;

                // Sprite push displacement (the real shockwave effect)
                float2 pushedUV = i.uv +
                    normalize(i.screenUV - float2(0.5,0.5)) * ring * _ShockPush * t;

                // Sample sprite after displacement
                float4 col = _MainTex.Sample(sampler_MainTex, pushedUV);

                // Crack mask (trincas)
                float cracks = crackNoise(i.uv * 3.0) * _CrackIntensity * t;

                // Faz as trincas ficarem mais fortes próximo ao centro
                cracks *= 1.0 - dist;

                // Trincas brilham com HDR
                col.rgb += cracks * _Color.rgb * _Emission * 0.5;

                // Alpha das trincas (integram com o efeito final)
                col.a += cracks * 0.4;

                // HDR emission
                col.rgb *= _Color.rgb * _Emission;

                // Fade alpha with ring and lifetime
                col.a *= ring * lifeFade;

                return col;
            }
            ENDHLSL
        }
    }
}