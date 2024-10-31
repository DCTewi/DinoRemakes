#pragma once

#include "core/component.h"

#include <entt/entt.hpp>

namespace dino::ecs
{
	class Entity
	{
	public:
		Entity(std::weak_ptr<entt::registry> registry);

		template <typename component_t>
		Entity& add_component(const component_t& component);

	protected:
		entt::entity _id{};
		std::weak_ptr<entt::registry> _registry{};
	};

	template<typename component_t>
	inline Entity& Entity::add_component(const component_t& component)
	{
		if (auto reg = _registry.lock(); reg->valid(_id))
		{
			if (is_unique<component_t>())
			{
				reg->emplace_or_replace<component_t>(_id, component);
			}
			else
			{
				reg->emplace<component_t>(_id, component);
			}
		}
		return *this;
	}
}
