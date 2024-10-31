#include "core/configuration.h"

#include <nlohmann/json.hpp>

#include <filesystem>
#include <fstream>
#include <mutex>
#include <optional>

using json = nlohmann::json;

namespace fs = std::filesystem;

constexpr char CONFIGURATION_PATH[] = "config.json";
constexpr char KEY_SCREEN_HEIGHT[] = "screen_height";
constexpr char KEY_SCREEN_WIDTH[] = "screen_width";
constexpr char KEY_TARGET_FPS[] = "fps";

namespace dino::configuration
{
	static std::optional<config_t> _config_instance{};

	static std::mutex _mutex{};
	static std::filesystem::path _config_path{ CONFIGURATION_PATH };

	static void to_json(json& j, const config_t& config)
	{
		j = json
		{
			{ KEY_SCREEN_HEIGHT, config.screen_height },
			{ KEY_SCREEN_WIDTH, config.screen_width },
			{ KEY_TARGET_FPS, config.target_fps },
		};
	}

	static void from_json(const json& j, config_t& config)
	{
		j.at(KEY_SCREEN_HEIGHT).get_to(config.screen_height);
		j.at(KEY_SCREEN_WIDTH).get_to(config.screen_width);
		j.at(KEY_TARGET_FPS).get_to(config.target_fps);
	}

	static void force_recreate()
	{
		_config_instance = config_t();

		if (fs::exists(_config_path))
		{
			fs::remove_all(_config_path);
		}

		std::ofstream f{ _config_path };
		if (f.is_open())
		{
			json j = _config_instance.value();
			f << j.dump(4);
		}
	}

	config_t& get()
	{
		if (!_config_instance.has_value())
		{
			update();
		}

		return _config_instance.value();
	}

	void update()
	{
		if (!fs::exists(_config_path) || !fs::is_regular_file(_config_path))
		{
			force_recreate();
		}

		try
		{
			std::ifstream f{ _config_path };
			_config_instance = json::parse(f).template get<config_t>();
		}
		catch (...)
		{
			force_recreate();
		}
	}

	void save()
	{
		if (!_config_instance.has_value())
		{
			update();
		}

		json j = _config_instance.value();
		std::ofstream f{ _config_path };

		if (f.is_open())
		{
			f << j.dump(4);
		}
	}
}

