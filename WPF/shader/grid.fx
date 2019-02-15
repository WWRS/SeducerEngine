#define LINE_WIDTH 0.01
#define LINE_SPACE 15.0

float time : register(C0);
float resolutionRatio : register(C1);

float isInGrid(float t, float2 uv)
{
    float modded = fmod((uv.x + uv.y + t) / LINE_WIDTH, LINE_SPACE);
    modded = fmod(modded + LINE_SPACE, LINE_SPACE);
    return 1.0 - clamp(floor(modded), 0, 1);
}

float4 main(float2 uvIn : TEXCOORD) : COLOR
{
    float2 uv = float2(uvIn.x, uvIn.y / resolutionRatio);
    
    float4 col = float4(0, 0, 0, 0);
    
    float3 purple = float3(0.05, 0.0, 0.1);
    float isPurple = 0;
    isPurple += isInGrid(time * 0.05, uv);
    isPurple += isInGrid(3.14, float2(uv.x, -uv.y));
    isPurple = clamp(isPurple, 0, 1);
    col += float4(purple * isPurple, isPurple);
    
    float3 blue = float3(0.01, 0.01, 0.15);
    float isBlue = 0;
    isBlue += isInGrid(time * 0.08, uv);
    isBlue += isInGrid(time * 0.05, float2(uv.x, -uv.y));
    isBlue = clamp(isBlue, 0, 1);
    col += float4(blue * isBlue, isBlue);
    
    col /= max(sqrt(col.a), 1);
    return float4(col.xyz, 1);
}