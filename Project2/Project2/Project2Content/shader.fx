sampler2D input : register(s0);
float frequency : register(C0);
//Game -- Going to add a point light (as a flashlight) for final version.
float4 PixelShaderFunction(float2 uv: TEXCOORD0) : COLOR0
{
	
	float4 color = tex2D( input , uv.xy); 
	color.b = color.b/2;
	color.g = color.g/2;
	color.r = color.r/2;
	return color;
    
}
//Start Screen
float4 PixelShaderFunction2(float2 uv : TEXCOORD0) : COLOR0
{

	float distfromcenter=distance(float2(0.4f, 0.6f), uv );
	float4 rColor = lerp(float4 (0,0,0,1),float4(0.8,0,0,1), saturate(distfromcenter));
	rColor *= 0.55;
	return rColor;
	
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
	pass Pass2
	{
		PixelShader = compile ps_2_0 PixelShaderFunction2();
	}
}
