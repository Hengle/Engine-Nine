/*********************************************************************NVMH3****
File:  $Id: //sw/devtools/FXComposer1.6/SDK/MEDIA/HLSL/goochy2.fx#1 $

Copyright NVIDIA Corporation 2002
TO THE MAXIMUM EXTENT PERMITTED BY APPLICABLE LAW, THIS SOFTWARE IS PROVIDED
*AS IS* AND NVIDIA AND ITS SUPPLIERS DISCLAIM ALL WARRANTIES, EITHER EXPRESS
OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, IMPLIED WARRANTIES OF MERCHANTABILITY
AND FITNESS FOR A PARTICULAR PURPOSE.  IN NO EVENT SHALL NVIDIA OR ITS SUPPLIERS
BE LIABLE FOR ANY SPECIAL, INCIDENTAL, INDIRECT, OR CONSEQUENTIAL DAMAGES
WHATSOEVER (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS,
BUSINESS INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR ANY OTHER PECUNIARY LOSS)
ARISING OUT OF THE USE OF OR INABILITY TO USE THIS SOFTWARE, EVEN IF NVIDIA HAS
BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.


Comments:
    Gooch-style "diffuse" with a glossy vertex-applied shading. The
		highlight is done in VERTEX shading --
		not as a texture.
    Glossiness is controlled not only by the usual power function, but also
        by applying a set of gloss controls that cause a sharp falloff across
		a specified range.
    The falloff will occur in the highlight range [glossBot-glossTop] and the
        amount of falloff is specified by "glossDrop." Setting "glossDrop"
		to 1.0 nullifies the effect.

******************************************************************************/

float Script : STANDARDSGLOBAL <
    string UIWidget = "none";
    string ScriptClass = "object";
    string ScriptOrder = "standard";
    string ScriptOutput = "color";
    string Script = "Technique=Technique?Untextured:Textured;";
> = 0.8;

/************* TWEAKABLES **************/

float4x4 WorldIT : WorldInverseTranspose < string UIWidget="None"; >;
float4x4 WorldViewProj : WorldViewProjection < string UIWidget="None"; >;
float4x4 World : World < string UIWidget="None"; >;
float4x4 ViewI : ViewInverse < string UIWidget="None"; >;

float4 LightPos : Position
<
	string Object = "PointLight";
	string UIName = "Lamp Position";
	string Space = "World";
> = {100.0f, 100.0f, 100.0f, 0.0f};

float4 LiteColor <
    string UIName =  "Bright Surface Color";
    string UIWidget = "Color";
> = {0.8f, 0.5f, 0.1f, 1.0f};

float4 DarkColor <
    string UIName =  "Dark Surface Color";
    string UIWidget = "Color";
> = {0.0f, 0.0f, 0.0f, 1.0f};

float4 WarmColor <
    string UIName =  "Gooch warm tone";
    string UIWidget = "Color";
> = {0.5f, 0.4f, 0.05f, 1.0f};

float4 CoolColor <
    string UIName =  "Gooch cool tone";
    string UIWidget = "Color";
> = {0.05f, 0.05f, 0.6f, 1.0f};

float4 SpecColor : Specular <
    string UIName =  "Hilight color";
    string UIWidget = "Color";
> = {0.7f, 0.7f, 1.0f, 1.0f};

float SpecExpon : SpecularPower <
    string UIWidget = "slider";
    float UIMin = 1.0;
    float UIMax = 128.0;
    float UIStep = 1.0;
    string UIName =  "Specular power";
> = 40.0;

float GlossTop <
    string UIWidget = "slider";
    float UIMin = 0.2;
    float UIMax = 1.0;
    float UIStep = 0.05;
    string UIName =  "Maximum for Gloss Dropoff";
> = 0.7;

float GlossBot
<
    string UIWidget = "slider";
    float UIMin = 0.05;
    float UIMax = 0.95;
    float UIStep = 0.05;
    string UIName =  "Minimum for Gloss Dropoff";
> = 0.5;

float GlossDrop
<
    string UIWidget = "slider";
    float UIMin = 0.0;
    float UIMax = 1.0;
    float UIStep = 0.05;
    string UIName =  "Strength of Glossy Dropoff";
> = 0.2;

////////////

texture colorTexture : DIFFUSE
<
    string ResourceName = "default_color.dds";
    string ResourceType = "2D";
>;

sampler2D colorTextureSampler = sampler_state
{
	Texture = <colorTexture>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = None;
};

/************* DATA STRUCTS **************/

/* data from application vertex buffer */
struct appdata {
    float3 Position	: POSITION;
    float4 UV		: TEXCOORD0;
    float4 Normal	: NORMAL;
};

/* data passed from vertex shader to pixel shader */
struct vertexOutput {
    float4 HPosition	: POSITION;
    float4 TexCoord0	: TEXCOORD0;
    float4 diffCol	: COLOR0;
    float4 specCol	: COLOR1;
};

/*********** vertex shader ******/

vertexOutput gooch2VS(appdata IN)
{
    vertexOutput OUT;
    float3 Nn = normalize(mul(IN.Normal, WorldIT).xyz);
    float4 Po = float4(IN.Position.xyz,1.0);
    //compute worldspace position
    float3 Pw = mul(Po, World).xyz;
    float3 Ln = normalize(LightPos - Pw);
    float mixer = 0.5 * (dot(Ln,Nn) + 1.0);
    float4 surfColor = lerp(DarkColor,LiteColor,mixer);
    float4 toneColor = lerp(CoolColor,WarmColor,mixer);
    float4 mixColor = surfColor + toneColor;
    mixColor.w = 1.0;
    OUT.diffCol = mixColor;
    OUT.TexCoord0 = IN.UV;
    float3 Vn = normalize(ViewI[3].xyz - Pw);
    float3 Hn = normalize(Vn + Ln);
    float hdn = pow(max(0,dot(Hn,Nn)),SpecExpon);
    hdn = hdn * (GlossDrop+smoothstep(GlossBot,GlossTop,hdn)*(1.0-GlossDrop));
    OUT.specCol = hdn * SpecColor;
    OUT.HPosition = mul(Po, WorldViewProj);
    return OUT;
}

/*************/

technique Untextured <
	string Script = "Pass=p0;";
> {
    pass p0  <
		string Script = "Draw=geometry;";
    > {		
		VertexShader = compile vs_1_1 gooch2VS();
		ZEnable = true;
		ZWriteEnable = true;
		CullMode = None;
		SpecularEnable = true;
		ColorArg1[ 0 ] = Diffuse;
		ColorOp[ 0 ]   = SelectArg1;
		ColorArg2[ 0 ] = Specular;
		AlphaArg1[ 0 ] = Diffuse;
		AlphaOp[ 0 ]   = SelectArg1;
    }
}

technique Textured <
	string Script = "Pass=p0;";
> {
    pass p0 <
	string Script = "Draw=geometry;";
> {		
		VertexShader = compile vs_1_1 gooch2VS();
		ZEnable = true;
		ZWriteEnable = true;
		CullMode = None;
        SpecularEnable = false;
        ColorArg1[ 0 ] = Texture;
        ColorOp[ 0 ]    = MultiplyAdd;
        ColorArg2[ 0 ] = Diffuse;
        ColorArg0[ 0 ] = Specular;
		Texture[0] = <colorTexture>;
		MinFilter[0] = Linear;
		MagFilter[0] = Linear;
		MipFilter[0] = None;
    }
}

/***************************** eof ***/