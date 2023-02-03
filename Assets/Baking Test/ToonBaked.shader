Shader "CreatorLynx/ToonBaked"
{
	Properties
	{
		//main block
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}	
		//ambient
		[HDR]
		_AmbientColor("Ambient Color", Color) = (0.4, 0.4, 0.4, 0.4)
		//specular
		[HDR]
		_SpecularColor("Specular Color", Color) = (0.9, 0.9, 0.9, 0.9)
		_Glossiness("Glossiness", Float) = 32
		//rim
		[HDR]
		_RimColor("Rim Color", Color) = (1, 1, 1, 1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.716
		_RimThroshold("Rim Threshold", Range(0, 1)) = 0.1

		_ShadowTreshold("Shadow Threshold", Range(0, 1)) = 0
		_HalfShadowTreshold("HalfShadow Threshold", Range(0, 1)) = 0.3

		//shadow baking
		_GIAlbedoColor ("Color Albedo (GI)", Color)=(1,1,1,1)
        _GIAlbedoTex ("Albedo (GI)",2D)="white"{}
	}
	SubShader
	{
		// ------------------------------------------------------------------
    // Extracts information for lightmapping, GI (emission, albedo, ...)
    // This pass is not used during regular rendering.
        Pass
        {
            Name "META"
            Tags {"LightMode"="Meta"}
            Cull Off
            CGPROGRAM

            #include"UnityStandardMeta.cginc"

            sampler2D _GIAlbedoTex;
            fixed4 _GIAlbedoColor;
			float4 _AmbientColor;
            float4 frag_meta2 (v2f_meta i): SV_Target
            {
                // We're interested in diffuse & specular colors
                // and surface roughness to produce final albedo.

               
				//float3 normal = normalize(i.worldNormal);
				//float NdotL = dot(normal, _WorldSpaceLightPos0);
				float shadow = SHADOW_ATTENUATION(i);
				float lightIntensity = 1;
				//if(NdotL > 0)
				//{
				//	lightIntensity = smoothstep(_ShadowTreshold, _ShadowTreshold+0.01, NdotL) * 0.7 + 0.3;
				//}
				///else
				//{
				//	lightIntensity = smoothstep(-_HalfShadowTreshold, -_HalfShadowTreshold + 0.01, NdotL) * 0.3 ;
				//}
				//lightIntensity *= shadow; //smoothstep(0, 0.01, shadow);
				lightIntensity *= shadow;

				float4 light = lightIntensity * _LightColor0;
				

				float4 sample = tex2D(_MainTex, i.uv);

				FragmentCommonData data = UNITY_SETUP_BRDF_INPUT (i.uv);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT(UnityMetaInput, o);
                fixed4 c = _Color * sample * (light + _AmbientColor);
                o.Albedo = fixed3(c.rgb);
                //o.Emission = Emission(i.uv.xy);
                return UnityMetaFragment(o);
            }

            #pragma vertex vert_meta
            #pragma fragment frag_meta2
            #pragma shader_feature _EMISSION
            #pragma shader_feature _METALLICGLOSSMAP
            #pragma shader_feature ___ _DETAIL_MULX2
            ENDCG
        }
		Pass
		{
			Tags
			{
				"LightMode" = "ForwardBase"
				"PassFlags" = "OnlyDirectional"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#pragma target 3.0
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv0 : TEXCOORD2;
				float3 worldNormal : NORMAL;
				float3 viewDir : TEXCOORD1;
				SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv0 = v.uv.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);
				TRANSFER_SHADOW(o);
				
				return o;
			}
			
			float4 _Color;
			float4 _AmbientColor;
			//SPECULAR
			float4 _SpecularColor;
			float _Glossiness;
			//rim
			float4 _RimColor;
			float _RimAmount;
			float _RimThroshold;

			//shadows
			float _ShadowTreshold;
			float _HalfShadowTreshold;

			float4 frag (v2f i) : SV_Target
			{
				//light calculating
				float3 normal = normalize(i.worldNormal);
				float NdotL = dot(normal, _WorldSpaceLightPos0);
				float shadow = SHADOW_ATTENUATION(i);
				float lightIntensity;
				if(NdotL > 0)
				{
					lightIntensity = smoothstep(_ShadowTreshold, _ShadowTreshold+0.01, NdotL) * 0.7 + 0.3;
				}
				else
				{
					lightIntensity = smoothstep(-_HalfShadowTreshold, -_HalfShadowTreshold + 0.01, NdotL) * 0.3 ;
				}
				lightIntensity *= shadow; //smoothstep(0, 0.01, shadow);

				float4 light = lightIntensity * _LightColor0;
				//SPECULAR calculating
				float3 viewDir = normalize(i.viewDir);
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);
				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;
				//rim calculating
				float rimDot = 1 - dot(viewDir, normal);
				rimDot = rimDot * pow(smoothstep(0, 1, NdotL), _RimThroshold);
				float rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimDot);
				float4 rim = rimIntensity * _RimColor;

				half4 bakedColorTex = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uv0.xy);
                fixed4 bakedColor;
                bakedColor.rgb = DecodeLightmap(bakedColorTex);

				float4 sample = tex2D(_MainTex, i.uv);
				return _Color * sample * (light + _AmbientColor + specular + rim) * bakedColor;
			}
			ENDCG
		}
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}