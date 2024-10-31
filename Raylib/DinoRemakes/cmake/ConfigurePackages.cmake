# 依赖导入
#

find_package(EnTT CONFIG REQUIRED)

find_package(raylib CONFIG REQUIRED)

set(nlohmann-json_IMPLICIT_CONVERSIONS OFF)
find_package(nlohmann_json CONFIG REQUIRED)
