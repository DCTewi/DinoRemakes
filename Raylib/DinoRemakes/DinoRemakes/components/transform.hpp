#pragma once

#include "core/component.h"

namespace dino::components
{
	struct Transform
	{
		float x{};
		float y{};
	};
}

namespace dino::ecs
{
	template <>
	inline bool is_unique<components::Transform>() { return true; }
}