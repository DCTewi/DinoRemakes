#include "core/world.h"

#include "components/transform.hpp"
#include "components/label.hpp"

#include "systems/label_renderer.hpp"

namespace dino::worlds
{
	using namespace ecs;

	static auto splash_screen = World::create()->add_system<systems::LabelRenderer>();

	static Entity splash = Entity{ splash_screen->registry() }
		.add_component(components::Label{ .text = "Hello RayLib", .font_size = 50 })
		.add_component(components::Transform{ .x = 800.f, .y = 490.f });
}
