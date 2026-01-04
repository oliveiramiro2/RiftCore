Shader "Custom/FireblastEmissive"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}

        _Tint("Fire Color", Color) = (1, 0.4, 0.1, 1)
        _EmissionIntensity("Emission Intensity", Range(0, 5)) = 2
        _PulseSpeed("Pulse Speed", Range(0, 10)) = 4

        _DissolveStrength("Dissolve Strength", Range(0, 1)) = 0
        _EdgeIntensity("Edge Emission", Range(0, 5)) = 2
        _EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags { 
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
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
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;

            float4 _Tint;
            float4 _EdgeColor;

            float _EmissionIntensity;
            float _PulseSpeed;

            float _DissolveStrength;
            float _EdgeIntensity;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 baseCol = tex2D(_MainTex, i.uv);
                float noise = tex2D(_NoiseTex, i.uv * 50).r;

                float cutoff = _DissolveStrength;
                float dissolveMask = step(cutoff, noise);

                float edgeMask = smoothstep(cutoff - 0.5, cutoff, noise);

                baseCol.rgb *= dissolveMask;

                float pulse = (sin(_Time.y * _PulseSpeed) * 1 + 1);

                float3 emission = (_Tint.rgb * _EmissionIntensity * pulse);

                emission += _EdgeColor.rgb * edgeMask * _EdgeIntensity;

                float3 finalColor = baseCol.rgb + emission;

                return float4(finalColor, baseCol.a * dissolveMask);
            }

            ENDHLSL
        }
    }
}
