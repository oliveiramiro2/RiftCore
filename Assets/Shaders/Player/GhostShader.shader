Shader "Custom/GhostAfterimage"
{
    Properties
    {
        _MainTex ("Sprite", 2D) = "white" {}
        _Tint ("Tint Color", Color) = (1,1,1,1)
        _Brightness ("Brightness", Range(0,3)) = 1
        _Desaturate ("Desaturate", Range(0,1)) = 0.5
        _Fade ("Fade", Range(0,1)) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Tint;
            float _Brightness;
            float _Desaturate;
            float _Fade;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (Varyings i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);

                // desaturação
                float gray = dot(col.rgb, float3(0.3,0.59,0.11));
                col.rgb = lerp(col.rgb, gray.xxx, _Desaturate);

                // brilho
                col.rgb *= _Brightness;

                // fade externo via script
                col.a *= _Fade;

                // tint
                col *= _Tint;

                return col;
            }
            ENDHLSL
        }
    }
}
