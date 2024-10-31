#include "core/world.h"

namespace dino::ecs
{
	static std::vector<std::shared_ptr<World>> _worlds{};

	std::vector<std::shared_ptr<World>>& World::worlds()
	{
		return _worlds;
	}

	struct concrete_world : public World {};

	std::shared_ptr<World> World::create()
	{
		auto world = std::make_shared<concrete_world>();
		return _worlds.emplace_back(world);
	}

	std::weak_ptr<entt::registry> World::registry() const
	{
		return _registry;
	}

	void World::awake()
	{
		for (auto& system : _systems)
		{
			system->awake();
		}
	}

	void World::update(float delta_time)
	{
		for (auto& system : _systems)
		{
			system->update(delta_time);
		}
	}

	void World::draw(float delta_time)
	{
		for (auto& system : _systems)
		{
			system->draw(delta_time);
		}
	}
}
