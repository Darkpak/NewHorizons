Shader "Unlit/PortalShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //_NoiseTex ("Noise Texture", 2D) = "white" {}
        _PulseSpeed ("Pulse Speed", Float) = 2
        _PulseStrength ("Pulse Strength", Float) = 0.2
        _RotationSpeed ("Rotation Speed", Float) = 2
        _WhiteCol("Change White Chanel", Color) = (1, 1, 1, 1)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
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
            // sampler2D _NoiseTex;
            // float4 _NoiseTex_ST;
            float _PulseSpeed;
            float _PulseStrength;
            float _RotationSpeed;
            float4 _WhiteCol;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //o.uv = TRANSFORM_TEX(v.uv, _NoiseTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
               float pulse = 1.0 + sin(_Time.y * _PulseSpeed) * _PulseStrength;

                float2 uv = i.uv - 0.5;
                uv *= pulse;
                uv += 0.5;

                float angle = _Time.y * _RotationSpeed; 
                float cosA = cos(angle);
                float sinA = sin(angle);
                float2 rotatedUV;
                rotatedUV.x = uv.x * cosA - uv.y * sinA;
                rotatedUV.y = uv.x * sinA + uv.y * cosA;
                uv = rotatedUV;

             

                fixed4 mainTex = tex2D(_MainTex, uv);
                float epsilon = 0.25;
                if (all(abs(mainTex.rgb - 1.0) < epsilon))
                {
                    mainTex.rgb = _WhiteCol; // purple
                }
                return mainTex;
            }
            ENDCG
        }
    }
}
