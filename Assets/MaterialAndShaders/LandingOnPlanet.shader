// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Shaders 102/Simple Box Blur"
{
	Properties
	{
		_MainTex("MainTexture", 2D) = "white" {}
		_SpaceTex("SpaceTexture", 2D) = "white" {}
		_CutOffTex("CutOffTexture", 2D) = "white" {}
		_CutOffValue("cutOffValue", Range(0, 1)) = 0
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	float4 box(sampler2D tex, float2 uv, float4 size)
	{
		return tex2D(tex, uv);
	}





	sampler2D _MainTex;
	sampler2D _CutOffTex;
	sampler2D _SpaceTex;
	float4 _MainTex_TexelSize; 
	float _CutOffValue;

	float4 frag(v2f i) : SV_Target
	{
		fixed4 originalPixel = tex2D(_MainTex, i.uv);
		fixed4 spacePixel = tex2D(_SpaceTex, i.uv);
		fixed4 cutOffPixel = tex2D(_CutOffTex, i.uv);

		if (_CutOffValue > cutOffPixel.r)
			return spacePixel;
		else
			return originalPixel;
	}
		ENDCG
	}
	}
}