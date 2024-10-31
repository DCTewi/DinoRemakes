#pragma once

#include "core/system.h"
#include "components/label.hpp"
#include "components/transform.hpp"

#include <entt/entt.hpp>
#include <raylib.h>

#include <memory>

namespace dino::systems
{
    class LabelRenderer : public ecs::System
    {
    public:
		virtual void draw(float) override
		{
			using namespace components;

			if (auto reg = _registry.lock())
			{
				auto view = reg->view<const Transform, const Label>();

				for (auto [entity, transform, label] : view.each())
				{
					DrawText(label.text.c_str(), (int)transform.x, (int)transform.y, label.font_size, label.font_color);
				}
			}
		}
    };
}
