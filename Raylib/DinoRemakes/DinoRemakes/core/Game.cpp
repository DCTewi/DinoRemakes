#include "Game.h"

#include <print>

namespace dino
{
	struct Game::Private
	{
		std::string name{ "dino" };
	};

	Game::Game() noexcept
	{
		_d = std::make_shared<Private>();
	}

	int Game::run() noexcept
	{
		std::println("hello {}", _d->name);
		return 0;
	}
}
