Shader "Custom/LightBeam"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Intensity ("Glow Intensity", Float) = 2.0
        _SoftEdge ("Soft Edge", Float) = 2.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend SrcAlpha One
        ZWrite Off
        Cull Off
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _Color;
            float _Intensity;
            float _SoftEdge;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv * 2.0 - 1.0; // Centered UVs
                float dist = length(uv);
                float falloff = saturate(1.0 - pow(dist, _SoftEdge));
                float alpha = falloff * _Color.a;

                return _Color * alpha * _Intensity;
            }
            ENDCG
        }
    }
}
