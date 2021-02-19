Shader "Unlit/DistortedCardRecolor"
{
	Properties
	{
		[HideInInspector]
		_MainTex("Texture", 2D) = "white" {}
		_Dissolve("Dissolve amount", Range(-1,1)) = 1
		[NoScaleOffset]
		_DissolveTex("Dissolve Texture", 2D) = "white" {}
		[NoScaleOffset]
		_EdgeTex("Edge Texture", 2D) = "white" {}
		_Scale("Dissolve texture scale", Range(0,0.1)) = 0.1
		_Edge("Edge width Contrast", Range(-2,0)) = 0


		[Header(Distortion)]

		[NoScaleOffset]
		_MaskTex("Mask Texture", 2D) = "white" {}
		_Speed ("Texture speed", Range(0,5)) = 1
		_Scale2 ("Distorion texture scale", Range(0,0.1)) =0.1
		_Distort ("Distortion Range", Range (0,0.2)) = 0.1

	}
		SubShader
		{
			//Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{

				//Cull Off
				Blend SrcAlpha OneMinusSrcAlpha
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					fixed4 color : COLOR;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float2 uvD : TEXCOORD1;
					float2 uvM : TEXCOORD2;
					fixed4 color : COLOR;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				sampler2D  _DissolveTex;
				sampler2D  _MaskTex;
				sampler2D  _EdgeTex;
				fixed _Speed;
				fixed _Scale;
				fixed _Scale2;
				fixed _Dissolve;
				fixed _Edge;
				fixed _Distort;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.color = v.color;
					fixed2 wPos = mul(unity_ObjectToWorld, v.vertex).xy;
					o.uvD = wPos * _Scale;
					o.uvM = wPos * _Scale2 - float2(0, _Time.y * _Speed);
					//o.uvD = v.uv * _Scale;

					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);
					fixed4 uncol = (col.r + col.g + col.b) * 0.33;
					fixed2 diss = tex2D(_DissolveTex, i.uvD).rg;
					fixed uvHer = clamp(((1 - i.uv.y) * (1 - i.uv.x) + _Dissolve) * _Edge, 0, 1);
					fixed smoothStep = smoothstep(0 , 1 , (diss.r + uvHer) * uvHer);
					fixed4 edge = tex2D(_EdgeTex, fixed2(smoothStep,0));
					fixed4 base = lerp(uncol ,edge, clamp(smoothStep * 5,0,1));

					fixed2 distort = tex2D(_DissolveTex, i.uvM).rg;
					fixed4 colD = tex2D(_MainTex, i.uv + (distort - 0.5) * _Distort );
					//fin.a = 1;
					//fin.a = col.a;
					fixed mask = tex2D(_MaskTex, i.uv).r;
					fixed4 final = lerp(colD,col,mask);
					//fixed4 final = lerp(colD,col,col.a);
					fixed4 fin = lerp(base, final, smoothstep(0.65, 1, (diss.r + uvHer) * uvHer));
					fin.a = col.a;

					return fin;
				}
				ENDCG
			}
		}
}
