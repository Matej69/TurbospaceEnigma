Shader "Sprites/RealSpriteColorShader"{

Properties{
	_MainTex("_Texture", 2D) = "white" {}
	_HitColor("_HitColor", Color) = (1, 1, 1, 1)
	_HitEffectActive ("_HitEffectActive", Int) = 0 
}

	SubShader{
		Cull Off
	
		Blend One OneMinusSrcAlpha
	
		Pass{
	
		CGPROGRAM
	
		#pragma vertex vertexFunc
		#pragma fragment fragmentFunc
		#include "UnityCG.cginc"
	
		sampler2D _MainTex;
	
		struct v2f {
			float4 pos : SV_POSITION;
			half2 uv : TEXCOORD0;
		};
	
		v2f vertexFunc(appdata_base v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord;
			return o;
		}
	
		fixed4 _HitColor;
		int _HitEffectActive;
		float4 _MainTex_TexelSize;
	
		fixed4 fragmentFunc(v2f i) : COLOR{
			half4 c = tex2D(_MainTex, i.uv);
			c.rgb *= c.a;
	
			half4 inCol = _HitColor;
			int hit = _HitEffectActive;
	
			// Do not apply color to whole box but just sprite pixels that are not transparent
			if (c.a != 0) {
				// Color whole sprite if hit = 1
				if (hit == 1)
					c = inCol;
			}
			
			return c;
		}
	
	
		ENDCG
		}
	}
}