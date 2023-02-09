Shader "screen space"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // note: no SV_POSITION in this struct
            struct v2f {
                float2 uv : TEXCOORD0;
            };

            v2f vert (
                float4 vertex : POSITION, // vertex position input
                float2 uv : TEXCOORD0, // texture coordinate input
                out float4 outpos : SV_POSITION // clip space position output
                )
            {
                v2f o;
                o.uv = uv;
                outpos = UnityObjectToClipPos(vertex);
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
            {
                // screenPos.xy will contain pixel integer coordinates.
                // use them to implement a checkerboard pattern that skips rendering
                // 4x4 blocks of pixels

                // checker value will be negative for 4x4 blocks of pixels
                // in a checkerboard pattern
                screenPos.xy = floor(screenPos.xy * 0.25) * 0.5;
                float checker = -frac(screenPos.r + screenPos.g);
                checker = 0;
                if(frac(screenPos.x) == 0 && frac(screenPos.y) == 0) checker = -1;
                else checker = 0;
                 

                // clip HLSL instruction stops rendering a pixel if value is negative
                //clip(checker);

                // for pixels that were kept, read the texture and output it
                fixed4 c = 0;//tex2D (_MainTex, i.uv);
                if(checker < 0) c = 0.5;
                return c;
            }
            ENDCG
        }
    }
}