Shader "Diffuse With Shadows"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            Tags {"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            // compile shader into multiple variants, with and without shadows
            // (we don't care about any lightmaps yet, so skip these variants)
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            // shadow helper functions and macros
            #include "AutoLight.cginc"
             float4 _MainTex_ST;
            // vertex shader input data
            struct appdata
            {
                float3 pos : POSITION;
                float3 uv1 : TEXCOORD1;
                float3 uv0 : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            struct v2f
            {
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                SHADOW_COORDS(1) // put shadows data into TEXCOORD1
                fixed3 diff : COLOR0;
                fixed3 ambient : COLOR1;
                float4 pos : SV_POSITION;
            };
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.pos);
                o.uv0 = v.uv1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                o.uv1 = v.uv0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                //half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                //half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                //o.diff = nl * _LightColor0.rgb;
                //o.ambient = ShadeSH9(half4(worldNormal,1));
                // compute shadows data
                TRANSFER_SHADOW(o)
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv1);
                // compute shadow attenuation (1.0 = fully lit, 0.0 = fully shadowed)
                fixed shadow = SHADOW_ATTENUATION(i);
                half4 bakedColorTex = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uv0.xy);
                fixed4 bakedColor;
                bakedColor.rgb = DecodeLightmap(bakedColorTex);
                // darken light's illumination with shadow, keep ambient intact
                fixed3 lighting = bakedColor.rgb;// + i.ambient;
                col.rgb *= lighting;
                return col;
            }
            ENDCG
        }

        // shadow casting support
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}