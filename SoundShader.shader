Shader "Sound_Visualization/SoundShader"
{
    Properties
    {
        _MainTex("Albedo", 3D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma target 4.0  
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler3D _MainTex;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 worldPos: TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex3D(_MainTex, i.worldPos);
                return col;
            }
            ENDCG
        }
    }
}
