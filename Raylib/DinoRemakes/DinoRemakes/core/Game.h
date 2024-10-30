#pragma once

#include <memory>

namespace dino
{
	class Game
	{
	public:
		Game() noexcept;

	public:
		int run() noexcept;

	private:
		struct Private;
		std::shared_ptr<Private> _d{ nullptr };
	};
}
