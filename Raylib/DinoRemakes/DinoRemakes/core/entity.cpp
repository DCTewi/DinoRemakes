#include "core/entity.h"

namespace dino::ecs
{
	Entity::Entity(std::weak_ptr<entt::registry> registry) : _registry(registry)
	{
		if (auto reg = _registry.lock())
		{
			_id = reg->create();
		}
	}

}
