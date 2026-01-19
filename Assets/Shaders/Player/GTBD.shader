Shader "Custom/GhostTrailBloomDistort"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _TintColor("Tint Color (HDR)", Color) = (1,1,1,1)
        _BrightnessBoost("Brightness Boost", Float) = 2.0
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _DistortionAmount("Distortion Amount", Range(0,1)) = 0.05
        _DissolveAmount("Dissolve Amount", Range(0,1)) = 0.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" "CanUseSpriteAtlas"="True" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;

            float4 _TintColor;
            float _BrightnessBoost;
            float _DistortionAmount;
            float _DissolveAmount;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // Texture sample
                float4 mainColor = tex2D(_MainTex, i.uv);

                // Noise sample
                float noise = tex2D(_NoiseTex, i.uv * 2).r;

                // DISTORT UVs
                float2 distort = (noise - 0.5) * _DistortionAmount;
                float4 distortedColor = tex2D(_MainTex, i.uv + distort);

                // Choose distorted sprite
                float4 sprite = lerp(mainColor, distortedColor, 0.5);

                // DISSOLVE
                float dissolveMask = step(_DissolveAmount, noise);
                sprite.a *= dissolveMask;

                // BLOOM through HDR emission
                float4 finalColor = sprite * _TintColor * _BrightnessBoost;

                // preserve alpha tint
                finalColor.a = sprite.a * _TintColor.a;

                return finalColor;
            }
            ENDHLSL
        }
    }
}
