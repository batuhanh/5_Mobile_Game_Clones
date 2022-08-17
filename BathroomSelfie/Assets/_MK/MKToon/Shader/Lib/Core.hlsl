//////////////////////////////////////////////////////
// MK Toon Core						       			//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2021 All rights reserved.            //
//////////////////////////////////////////////////////

#ifndef MK_TOON_CORE
	#define MK_TOON_CORE

	#if defined(MK_URP)
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#if defined(LOD_FADE_CROSSFADE)
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
		#endif
	#elif defined(MK_LWRP)
		#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
	#else
		#include "UnityCG.cginc"
	#endif

	struct MKInputDataWrapper
	{
		float2 normalizedScreenSpaceUV;
		float3 positionWS;
	};

	struct MKFragmentOutput
	{
		half4 svTarget0 : SV_Target0;
		#ifdef _WRITE_RENDERING_LAYERS
			float4 svTarget1 : SV_Target1;
		#endif
	};

	#include "Config.hlsl"
	#include "Pipeline.hlsl"
	#include "Uniform.hlsl"
	#include "Common.hlsl"

#endif