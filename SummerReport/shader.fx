float4x4 wvp:WVP;
float minimum : MIN = 0;
float maximum : MAX = 1;
float xScaling : XSC = 1;
float yScaling : YSC = 1;
int itr : IT = 100;
float epsilon = 1.0E-3;

struct VS_OUTPUT
{
	float4 svPosition : SV_Position;

	float4 Position : POSITION;
};

struct PS_OUTPUT
{
	float4 height:SV_Target0;

	float4 pos:SV_Target1;

	float4 grad:SV_Target2;
};

float func(float2 p)
{
	float x = p.x;
	float y = p.y;
	//return sin((x * 5)*(x * 5) + 5 * y)*cos((y * 5)*(y * 5) + 5 * x);
	return (exp(-10 * pow((x*x + y*y), 4)) - 0.5) * 2.0;
}

float dxfunc(float2 p)
{
	float x = p.x;
	float y = p.y;
	//return 50 * x*cos(25 * x*x + 5 * y)*cos(5 * x + 25 * y*y) - 5 * sin(25 * x*x + 5*y)*sin(5 * x + 25 * y*y);
	return -160 * exp(-10 * pow(x*x + y*y, 4))*x*pow(x*x + y*y, 3);
}

float dyfunc(float2 p)
{
	float x = p.x;
	float y = p.y;
	//return 5 * cos(25 * x*x + 5 * y)*cos(5 * x + 25 * y*y) - 50 * y*sin(25 * x*x + 5 * y)*sin(5 * x + 25 * y*y);
	return -160 * exp(-10 * pow(x*x + y*y, 4))*y*pow(x*x + y*y, 3);
}

float2 grad(float2 p)
{
	float2 gradient = float2(dxfunc(p), dyfunc(p));
	normalize(gradient);
	return gradient;
}

float2 nextPoint(float2 p)
{
	float fp = func(p);
	float fpg = func(p - grad(p));
	if (abs(fp - fpg) < epsilon)return p;
	float k = fp / (fp - fpg);
	return p - k*grad(p);
}

float4 toPosColor(float2 pos)
{
	return float4(pos.x / 2.0 + 0.5, 0, pos.y / 2.0 + 0.5, 1);
}
float3 Hue(float H)
{
	float R = abs(H * 6.0 - 3.0) - 1.0;
	float G = 2.0 - abs(H * 6.0 - 2.0);
	float B = 2.0 - abs(H * 6.0 - 4.0);
	return saturate(float3(R, G, B));
}

float4 HSVtoRGB(float3 HSV)
{
	return float4(((Hue(HSV.x) - 1.0) * HSV.y + 1.0) * HSV.z, 1.0);
}

float clampRange(float v, float minv, float maxv)
{
	float clamped = (v - minv) / (maxv - minv);
	return max(0, min(clamped, 1));
}

float adjustHue(float hue)
{
	float hueAddition = 0;
	float hueLength = 0.75;
	hue += hueAddition*hueLength;
	return (1-frac(hue))*hueLength;
}



float4 toHeightColor(float height)
{
	float dist = maximum - minimum;
	return HSVtoRGB(float3(adjustHue(clampRange(height,minimum,maximum)), 1,1 ));;
}

float4 toGradColor(float grad)
{

}

VS_OUTPUT vs(float4 pos:POSITION)
{
	VS_OUTPUT vo;
	vo.svPosition = mul(pos,wvp);
	vo.Position = pos;
	return vo;
}

PS_OUTPUT ps(VS_OUTPUT vo)
{
	float2 dp = vo.Position.xy*float2(xScaling, yScaling);
		float2 fp = dp;
	for (int i = 0; i < itr; i++)
	{
		dp = nextPoint(dp);
	}
	float g = grad(dp);

	float f = func(dp);
	PS_OUTPUT pso;
	pso.height = toHeightColor(f);
	pso.grad = toHeightColor(func(fp));
	if (abs(f) < epsilon){
		pso.grad=pso.height = 1.0.xxxx;
		pso.pos = toPosColor(dp);
	}
	else{
		pso.pos = toPosColor(dp);
	}
	return pso;
}

technique10 DefaultTechnique{
	pass DefaultPass{
		SetVertexShader(CompileShader(vs_5_0, vs()));
		SetPixelShader(CompileShader(ps_5_0, ps()));
	}
}
