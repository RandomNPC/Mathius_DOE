Shader "Hidden/Nature/Tree Creator Bark Rendertex Snow" {
Properties {
	_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
	_BumpSpecMap ("Normalmap (GA) Spec (R)", 2D) = "bump" {}
	_TranslucencyMap ("Trans (RGB) Gloss(A)", 2D) = "white" {}
	
	// These are here only to provide default values
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Scale ("Scale", Vector) = (1,1,1,1)
	_SquashAmount ("Squash", Float) = 1
}

SubShader {  
	Fog { Mode Off }	
	Pass {
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

struct v2f {
	float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
	float3 color : TEXCOORD1;
	float2 params[3]: TEXCOORD2;
	
	float3 mywn : COLOR;
};

float3 _TerrainTreeLightDirections[4];
float4 _TerrainTreeLightColors[4];

v2f vert (appdata_full v) {
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	o.uv = v.texcoord.xy;
	float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
	
	for (int j = 0; j < 3; j++)
	{
		float3 lightDir = _TerrainTreeLightDirections[j];
		half nl = dot (v.normal, lightDir);
		o.params[j].r = nl;
		half3 h = normalize (lightDir + viewDir);
		float nh = max (0, dot (v.normal, h));
		o.params[j].g = nh;
	}
	
	o.color = v.color.a;
	// add worldnormal to color
	float3 worldNormal = normalize(mul((float3x3)_Object2World, v.normal));
    o.mywn.rgb = worldNormal.xyz; // y - world up vector
	return o;
}

sampler2D _MainTex;
sampler2D _BumpSpecMap;
sampler2D _TranslucencyMap;
fixed4 _SpecColor;

// this texture has to be send via script = CustomTerrainScriptAtsV3Snow.cs
sampler2D _SnowTexture;

//snow
float _SnowAmount;
float _SnowStartHeight;

fixed4 frag (v2f i) : COLOR
{
	//fixed3 albedo = tex2D (_MainTex, i.uv).rgb * i.color;
	fixed3 col = tex2D (_MainTex, i.uv) * i.color.rgb;
	
	// get snow texture
	half3 snowtex = tex2D( _SnowTexture, i.uv).rgb;
	
	// worldNormal is stored in i.mywn
	// (1-col.b) = take the blue channel to get some kind of heightmap...
	// float snowAmount = _SnowAmount * (i.mywn.y) * (1-col.g) * 1.425;
	
	// lerp = allows snow even on orthogonal surfaces
	// float snowAmount = lerp(_SnowAmount * (i.mywn.y), 1, _SnowAmount) * (1-col.g);
	// lerp = allows snow even on orthogonal surfaces // worldNormal is stored in i.mywn
	float snowAmount = lerp(_SnowAmount * i.mywn.y, 1, _SnowAmount) * (1-col.b)*.55;
		
	// sharpen snow mask
	snowAmount = clamp(pow(snowAmount,6)*256, 0, 1);
	
	fixed3 albedo = (col.rgb * (1-snowAmount) * i.color + snowtex.rgb*snowAmount);
	half gloss = tex2D (_TranslucencyMap, i.uv).a;
	half specular = tex2D (_BumpSpecMap, i.uv).r * 128.0;
	half3 light = UNITY_LIGHTMODEL_AMBIENT * albedo;
	for (int j = 0; j < 3; j++)
	{
		half3 lightColor = _TerrainTreeLightColors[j].rgb;
		half nl = i.params[j].r;
		light += albedo * lightColor * nl;
		float nh = i.params[j].g;
		float spec = pow (nh, specular) * gloss;
		light += lightColor * _SpecColor.rgb * spec;
	}
	fixed4 c;
	c.rgb = light * 2.0;
	c.a = 1.0;
	return c;
}
ENDCG
	}
}
FallBack Off
}
