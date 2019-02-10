Shader "Camera/LandingOnPlanet"{

Properties{
}

	SubShader{
	
		Pass{
	
		CGPROGRAM
	
		#pragma vertex vertexFunc
		#pragma fragment fragmentFunc
		#include "UnityCG.cginc"
	
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
	
	
		fixed4 fragmentFunc(v2f i) : COLOR{
			return half4(1.0, 0.0, 0.0, 1.0);
			//return c;
		}
	
	
		ENDCG
		}
	}
}