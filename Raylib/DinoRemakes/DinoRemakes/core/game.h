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
		class Private;
		std::shared_ptr<Private> _d{ nullptr };
	};
}
