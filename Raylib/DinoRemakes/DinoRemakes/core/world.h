#pragma once

#include "core/entity.h"
#include "core/component.h"
#include "core/system.h"

#include <entt/entt.hpp>

#include <type_traits>
#include <memory>

namespace dino::ecs
{
	template <typename T>
	concept ISystem = std::is_base_of<System, std::decay_t<T>>::value;

	class World : public std::enable_shared_from_this<World>
	{
	public:
		static std::vector<std::shared_ptr<World>>& worlds();
		static std::shared_ptr<World> create();

	public:
		std::weak_ptr<entt::registry> registry() const;

		template <ISystem T>
		std::shared_ptr<World> add_system();

		void awake();
		void update(float delta_time);
		void draw(float delta_time);

	protected:
		World() = default;
		World(World const&) = delete;
		World(World&&) = delete;
		World& operator=(World const&) = delete;
		World& operator=(World&&) = delete;

	private:
		std::shared_ptr<entt::registry> _registry{ std::make_shared<entt::registry>() };
		std::vector<std::shared_ptr<System>> _systems;
	};

	template<ISystem T>
	inline std::shared_ptr<World> World::add_system()
	{
		std::shared_ptr<System> system = std::make_shared<T>();
		system->init(_registry);
		_systems.emplace_back(system);
		return shared_from_this();
	}
}

