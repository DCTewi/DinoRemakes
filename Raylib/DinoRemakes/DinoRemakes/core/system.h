#pragma once

#include <entt/entt.hpp>

namespace dino::ecs
{
	class System
	{
	public:
		virtual void init(std::weak_ptr<entt::registry> registry) { _registry = registry; }
		virtual void awake() {}
		virtual void update([[maybe_unused]]float delta_time) {}
		virtual void draw([[maybe_unused]]float delta_time) {}

	protected:
		std::weak_ptr<entt::registry> _registry{};
	};
}
