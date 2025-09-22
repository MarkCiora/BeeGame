texture Texture0;

sampler2D TextureSampler = sampler_state
{
    Texture = <Texture0>;
    MinFilter = Linear;
    MagFilter = Linear;
    MipFilter = Linear;
    AddressU = Wrap;
    AddressV = Wrap;
};

struct PS_INPUT
{
    float4 Color    : COLOR0;
    float2 TexCoord : TEXCOORD0;
};



float4 PS_Main(PS_INPUT input) : COLOR0
{
    float2 uv = input.TexCoord;
    float4 tex_color = tex2D(TextureSampler, uv);

    float x = abs(uv.x - 0.5f) * 2;
    float y = abs(uv.y - 0.5f) * 2;
    
    float dist_to_edge = 0.8660254 - max(y, 0.5 * y + 0.8660254 * x);

    if (dist_to_edge < 0.05) return float4(0,0,0,0);

    return tex_color * input.Color;
}

technique SimpleTech
{
    pass P0
    {
        PixelShader = compile ps_3_0 PS_Main();
    }
}
