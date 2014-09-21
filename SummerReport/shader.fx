float4x4 wvp:WVP;
float minimum : MIN = 0;
float maximum : MAX = 1;
float xScaling : XSC = 1;
float yScaling : YSC = 1;
int itr : IT = 100;
float epsilon = 1.0E-6;

struct VS_OUTPUT
{
	float4 svPosition : SV_Position;

	float4 Position : POSITION;
};

struct PS_OUTPUT
{
	float4 height:SV_Target0;

	float4 pos:SV_Target1;
};

float func(float2 p)
{
	float x = p.x;
	float y = p.y;
	return sin((x * 5)*(x * 5) + 5 * y)*cos((y * 5)*(y * 5) + 5 * x);
}

float dxfunc(float2 p)
{
	float x = p.x;
	float y = p.y;
	return 50 * x*cos(25 * x*x + 5 * y)*cos(5 * x + 25 * y*y) - 5 * sin(25 * x*x + 5*y)*sin(5 * x + 25 * y*y);
}

float dyfunc(float2 p)
{
	float x = p.x;
	float y = p.y;
	return 5 * cos(25 * x*x + 5 * y)*cos(5 * x + 25 * y*y) - 50 * y*sin(25 * x*x + 5 * y)*sin(5 * x + 25 * y*y);
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



VS_OUTPUT vs(float4 pos:POSITION)
{
	VS_OUTPUT vo;
	vo.svPosition = mul(pos,wvp);
	vo.Position = pos;
	return vo;
}

PS_OUTPUT ps(VS_OUTPUT vo)
{
	float2 dp = vo.Position.xy*float2(xScaling,yScaling);
	for (int i = 0; i < itr; i++)
	{
		dp = nextPoint(dp);
	}
	float f = func(dp);
	PS_OUTPUT pso;
	pso.height=float4((f-minimum)*(maximum-minimum), f>maximum?1:0, f<minimum?1:0, 1);
	if (abs(f) < epsilon){
		pso.height = 1.0.xxxx;
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
