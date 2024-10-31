#include "core/game.h"

#include "core/configuration.h"
#include "core/world.h"

#include <raylib.h>

#include <chrono>

namespace dino
{
	using namespace ecs;
	using clock_t = std::chrono::steady_clock;
	using delta_time_t = std::chrono::milliseconds;

	class Game::Private
	{
		friend Game;

		clock_t::time_point last_update_tick = clock_t::now();
		delta_time_t delta_time{};
		float delta_time_seconds{};

		void update_time()
		{
			auto update_tick = clock_t::now();
			delta_time = std::chrono::duration_cast<delta_time_t>(update_tick - last_update_tick);
			last_update_tick = update_tick;
			delta_time_seconds = delta_time.count() / 1000.f;
		}

		void awake() const
		{
			for (auto& world : World::worlds())
			{
				world->awake();
			}
		}

		void update() const
		{
			for (auto& world : World::worlds())
			{
				world->update(delta_time_seconds);
			}
		}

		void draw() const
		{
			for (auto& world : World::worlds())
			{
				world->draw(delta_time_seconds);
			}
		}
	};

	Game::Game() noexcept
	{
		_d = std::make_shared<Private>();
	}

	int Game::run() noexcept
	{
		auto& config = configuration::get();

		InitWindow(config.screen_width, config.screen_height, "Dino Remakes");
		SetTargetFPS(config.target_fps);

		while (!WindowShouldClose())
		{
			_d->update_time();
			_d->update();

			BeginDrawing();
			{
				ClearBackground(RAYWHITE);

				_d->draw();
			}
			EndDrawing();
		}

		CloseWindow();

		return 0;
	}
}
