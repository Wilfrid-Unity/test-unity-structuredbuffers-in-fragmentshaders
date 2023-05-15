Shader "Unlit/render-unlit-from-read-structured-buffer"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma target 5.0
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            struct MyBufferData
            {
                float3 originalColor;
                float3 modifiedColor;
            };

            StructuredBuffer<MyBufferData> _MyStructuredBuffer : register(t1);

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                int index = 15 * int(15 * i.uv.x) + int(15 * i.uv.y);

                col.rgb = _MyStructuredBuffer[index].originalColor;

                col.b *= 0.2;

                return col;
            }
            ENDCG
        }
    }
}
