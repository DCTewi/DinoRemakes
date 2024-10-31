#pragma once

#include <memory>

namespace dino::configuration
{
	struct config_t
	{
		int screen_width = 1920;
		int screen_height = 1080;

		int target_fps = 60;
	};

	[[nodiscard]] config_t& get();
	void update();
	void save();
}
