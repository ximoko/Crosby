Shader "Custom/Mobile Diffuse Outline Soften Edge" {
Properties {
	[Header(Texture Maps)]
    _MainTex ("Base (RGB)", 2D) = "white" {}

	[Header(Outline Parameters)]
	_OutlineWidth ("Outline Width", Range(0,1)) = 0
	_OutlineColor ("Outline Color", Color) = (1,1,1,1)
}

SubShader {
    Tags { "RenderType" = "Opaque" }
    Tags { "Queue" = "Transparent" }
    LOD 150

	Pass{
		ZWrite Off
		Cull Off

		CGPROGRAM
		#include "UnityCG.cginc"

		#pragma vertex vert
		#pragma fragment frag

		struct appdata
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
		};

		struct v2f
		{
			float4 pos : POSITION;
			float3 normal : NORMAL;
		};

		float _OutlineWidth;
		float4 _OutlineColor;

		v2f vert(appdata v)
		{
			//v.vertex.xyz += v.normal * _OutlineWidth;

			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);

			float3 normal = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
			float2 offset = TransformViewToProjection(normal.xy);

			o.pos.xy += offset * o.pos.z * _OutlineWidth;

			return o;
		}

		half4 frag(v2f i) : COLOR
		{
			return _OutlineColor;
		}

		ENDCG
	}

	CGPROGRAM
	#pragma surface surf Lambert noforwardadd

	sampler2D _MainTex;

	struct Input {
		float2 uv_MainTex;
	};

	void surf (Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
}

Fallback "Mobile/VertexLit"
}
