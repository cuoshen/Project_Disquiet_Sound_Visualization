Shader "Sound_Visualization/SoundShader"
{
    Properties
    {
        _MainTex("Albedo", 3D) = "white" {}
        _GridSize("Total Grid Size",Vector) = (1,1,1)
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
            float3 _GridSize;

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

            fixed4 frag(v2f i) : SV_Target
            {
                // Normalize the world position regarding the grid
                // Some space to optimize, replace "/x" with "* (1/x)" approach
                float4 uvw = float4(i.worldPos.x / _GridSize.x, i.worldPos.y / _GridSize.y, i.worldPos.z / _GridSize.z, 1);
                // sample the sound texture
                fixed4 col = tex3D(_MainTex, uvw);
                return col;
            }
            ENDCG
        }
    }
}
