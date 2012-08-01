
float3 ProjectionParams;
float3 FocalParams;

sampler TextureSampler : register(s0);
sampler BlurSampler : register(s1);
sampler DepthSampler : register(s2);

float4 PS(float2 texCoord : TEXCOORD0) : COLOR0
{  
    float4 scene = tex2D(TextureSampler, texCoord);
    float4 blur = tex2D(BlurSampler, texCoord);
    float4 depth = tex2D(DepthSampler, texCoord);

    float z = (depth.r - ProjectionParams.z) / (depth.r * ProjectionParams.x - ProjectionParams.y);
    float amount = saturate(smoothstep(0, 1, (abs(z - FocalParams.x) - FocalParams.y) / FocalParams.z));
    
    return lerp(scene, blur, amount);
}


Technique Default
{
    Pass
    {
        PixelShader	 = compile ps_2_0 PS();
    }
}