//********************************** Banshee Engine (www.banshee3d.com) **************************************************//
//**************** Copyright (c) 2016 Marko Pintera (marko.pintera@gmail.com). All rights reserved. **********************//
#pragma once

#include "BsCorePrerequisites.h"
#include "Reflection/BsRTTIType.h"
#include "Components/BsCAudioSource.h"
#include "RTTI/BsGameObjectRTTI.h"

namespace bs
{
	/** @cond RTTI */
	/** @addtogroup RTTI-Impl-Core
	 *  @{
	 */

	class BS_CORE_EXPORT CAudioSourceRTTI : public RTTIType<CAudioSource, Component, CAudioSourceRTTI>
	{
		BS_BEGIN_RTTI_MEMBERS
			BS_RTTI_MEMBER_REFLPTR(mInternal, 0)
			BS_RTTI_MEMBER_PLAIN(mPlayOnStart, 1)
		BS_END_RTTI_MEMBERS
	public:
		CAudioSourceRTTI()
			:mInitMembers(this)
		{ }

		const String& getRTTIName() override
		{
			static String name = "CAudioSource";
			return name;
		}

		UINT32 getRTTIId() override
		{
			return TID_CAudioSource;
		}

		SPtr<IReflectable> newRTTIObject() override
		{
			return GameObjectRTTI::createGameObject<CAudioSource>();
		}
	};

	/** @} */
	/** @endcond */
}