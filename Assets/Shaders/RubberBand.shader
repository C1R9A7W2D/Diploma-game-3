Shader "Custom/RubberBand"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _StretchAmount ("Stretch Amount", Float) = 0.5
        _ContactPoint ("Contact Point", Vector) = (0.5, 0.5, 0, 0)
        _StretchRadius ("Stretch Radius", Float) = 0.3
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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _StretchAmount;
            float4 _ContactPoint;
            float _StretchRadius;

            v2f vert (appdata v)
            {
                v2f o;
                
                // Вычисляем расстояние от вершины до центра натяжения
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float2 centerPos = _ContactPoint.xy;
                float dist = distance(worldPos.xy, centerPos);
                
                // Создаём falloff (плавное затухание эффекта)
                float falloff = smoothstep(_StretchRadius, 0, dist);
                
                // Вычисляем направление от центра к вершине
                float2 direction = normalize(worldPos.xy - centerPos);
                
                // Применяем деформацию
                float deformation = _StretchAmount * falloff;
                v.vertex.xy += direction * deformation * 0.1;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = mul((float3x3)unity_ObjectToWorld, v.normal);
                
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                return col;
            }
            ENDCG
        }
    }
}
