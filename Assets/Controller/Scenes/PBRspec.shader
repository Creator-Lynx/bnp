Shader "ed_shaders/PBRspec"
{
    Properties
    {
        _MaskTex ("Mask texture", 2D) = "black" {}
        _MainTex ("Main texture", 2D) = "red" {}
        _MainTex1 ("Main texture1", 2D) = "white" {}
        _MainTex2 ("Main texture2", 2D) = "black" {}

        _EmissionMaskTex ("Emission texture", 2D) = "white" {}
        _EmissionApperance ("Emission apperance", Range(0, 1)) = 1

        _BumpMap("Bump map", 2D) = "bump" {}
        _BumpMap1("Bump map 1", 2D) = "bump" {}

        _Shiness ("Shiness", Range(0, 1)) = 0.07
        _Shiness1 ("Shiness1", Range(0, 1)) = 0.07
        _Shiness2 ("Shiness2", Range(0, 1)) = 0.07

        _Specularity ("Specularity", Range(0, 1)) = 1
        _Specularity1 ("Specularity1", Range(0, 1)) = 1
        _Specularity2 ("Specularity2", Range(0, 1)) = 1

        _SpecColor1 ("Specular color1", Color) = (1, 1, 1, 1)
        _SpecColor2 ("Specular color2", Color) = (1, 1, 1, 1)
        _SpecColor3 ("Specular color3", Color) = (1, 1, 1, 1)

    }

    SubShader
    {
        CGPROGRAM
        #pragma surface surf StandardSpecular
        
        sampler2D _MaskTex, _MainTex, _MainTex1, _MainTex2, _EmissionMaskTex;
        
        half _EmissionApperance;
        
        sampler2D _BumpMap, _BumpMap1;

        half _Shiness, _Shiness1, _Shiness2;

        fixed _Specularity, _Specularity1, _Specularity2;

        fixed3 _SpecColor1, _SpecColor2, _SpecColor3;


        struct Input
        {
            half2 uv_MaskTex;
            half2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutputStandardSpecular o)
        {
            fixed3 masks = tex2D(_MaskTex, IN.uv_MaskTex);
            fixed3 clr = tex2D(_MainTex, IN.uv_MainTex) * masks.r;
            clr += tex2D(_MainTex1, IN.uv_MainTex) * masks.g;
            clr += tex2D(_MainTex2, IN.uv_MainTex) * masks.b;
            o.Albedo = clr;

            fixed3 emTex = tex2D(_EmissionMaskTex, IN.uv_MaskTex);
            //half appearMask = emTex.r;
            half3 appearMask = smoothstep(_EmissionApperance  * 1.2- 0.2, _EmissionApperance * 1.2, emTex);
            //o.Emission = smoothstep(0.5, 1.0, IN.uv_MaskTex.y);
            o.Emission = appearMask;


            o.Normal = UnpackNormal(tex2D(_BumpMap1, IN.uv_MainTex));

            o.Specular = (_Specularity * _SpecColor1 * masks.r + 
            _Specularity1 * _SpecColor2 * masks.g + 
            _Specularity2 * _SpecColor3 * masks.b)
             * clr;

            o.Smoothness = _Shiness * masks.r + _Shiness1 * masks.g +_Shiness2 * masks.b;
            

        }

        ENDCG
    }

    Fallback "Diffuse"
}