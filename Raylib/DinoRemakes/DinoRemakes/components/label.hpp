#pragma once

#include <raylib.h>

#include <string>

namespace dino::components
{
	struct Label
	{
		std::string text{};
		int font_size{ 20 };
		Color font_color{ BLACK };
	};
}
