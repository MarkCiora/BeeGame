texture Texture0;

sampler2D TextureSampler = sampler_state
{
    Texture = <Texture0>;
    MinFilter = Point;
    MagFilter = Point;
    MipFilter = Point;
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
    float4 tex_color = tex2D(TextureSampler, uv) * input.Color;

    if (tex_color[3] <= 0.01) {   
        return float4(0,0,0,0);
    }
    else {
        float4 highlight_color = float4(0.3, 0.3, 0.3, 1.0);
        float a_highlight = 0.5;

        float4 shifted_color = highlight_color * a_highlight + 
                               (1 - a_highlight) * tex_color;

        shifted_color[3] *= 0.5;

        return shifted_color;
    }
}

technique SimpleTech
{
    pass P0
    {
        PixelShader = compile ps_3_0 PS_Main();
    }
}
